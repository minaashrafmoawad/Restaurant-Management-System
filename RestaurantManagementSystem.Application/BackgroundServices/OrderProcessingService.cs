using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Enums;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.BackgroundServices
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;

        public OrderProcessingService(IOrderService orderService, IMenuService menuService)
        {
            _orderService = orderService;
            _menuService = menuService;
        }

        public async Task ProcessPendingOrdersAsync()
        {
            // Business Logic: Automatic progression - Pending → Preparing (after 5 min)
            var pendingOrders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Pending);

            foreach (var order in pendingOrders)
            {
                if (DateTime.UtcNow > order.CreatedDate.AddMinutes(5))
                {
                    await _orderService.UpdateOrderStatusAsync(order.Id, OrderStatus.Preparing);
                }
            }
        }

        public async Task UpdateOrderStatusesAsync()
        {
            // Business Logic: Preparing → Ready (after preparation time)
            var preparingOrders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Preparing);

            foreach (var order in preparingOrders)
            {
                var preparationTime = GetEstimatedPreparationTime(order);
                if (DateTime.UtcNow > order.UpdatedDate?.AddMinutes(preparationTime))
                {
                    await _orderService.UpdateOrderStatusAsync(order.Id, OrderStatus.Ready);
                }
            }
        }

        public async Task SendCustomerNotificationsAsync()
        {
            // Business Logic: Customer notifications (simulated via ViewBag messages)
            var readyOrders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Ready);

            foreach (var order in readyOrders)
            {
                // Simulate sending notification
                Console.WriteLine($"Notification: Order {order.Id} is ready for {order.Type}");
            }
        }

        private int GetEstimatedPreparationTime(Order order)
        {
            // Simple estimation: 5 minutes per item, minimum 15 minutes
            return Math.Max(15, order.OrderItems?.Count * 5 ?? 15);
        }
    }

}
