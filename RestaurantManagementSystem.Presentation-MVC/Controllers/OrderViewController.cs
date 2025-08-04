using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Enums;

namespace Restaurant_Management_System.Controllers
{
    public class OrderViewController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;

        public OrderViewController(IOrderService orderService, IMenuService menuService)
        {
            _orderService = orderService;
            _menuService = menuService;
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _menuService.GetCategoriesWithItemsAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto orderDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = await _orderService.CreateOrderAsync(orderDto);
                    TempData["Success"] = "Order created successfully!";
                    return RedirectToAction(nameof(Details), new { id = order.Id });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var categories = await _menuService.GetCategoriesWithItemsAsync();
            ViewBag.Categories = categories;
            return View(orderDto);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        public async Task<IActionResult> Kitchen()
        {
            var preparingOrders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Preparing);
            var pendingOrders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Pending);

            ViewBag.PreparingOrders = preparingOrders;
            ViewBag.PendingOrders = pendingOrders;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid orderId, OrderStatus status)
        {
            try
            {
                await _orderService.UpdateOrderStatusAsync(orderId, status);
                TempData["Success"] = "Order status updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Kitchen));
        }
    }
}
