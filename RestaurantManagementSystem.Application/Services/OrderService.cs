using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Repository_Contracts;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Enums;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public OrderService(IOrderRepository orderRepository, IMenuItemRepository menuItemRepository)
        {
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = new Order
            {
                //Id = Guid.NewGuid(),
                CustomerId = orderDto.CustomerId,
                Type = orderDto.Type,
                Status = OrderStatus.Pending,
                SpecialInstructions = orderDto.SpecialInstructions,
                DeliveryAddress = orderDto.DeliveryAddress,
                PickupTime = orderDto.PickupTime,
                CreatedDate = DateTime.UtcNow
            };

            // Business Logic: Validate and create order items
            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var itemDto in orderDto.OrderItems)
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(itemDto.MenuItemId);

                // Business Logic: Unavailable items cannot be added to orders
                if (menuItem == null || !menuItem.IsAvailable)
                    throw new ArgumentException($"Menu item {itemDto.MenuItemId} is not available");

                var orderItem = new OrderItem
                {
                    //Id = Guid.NewGuid(),
                    MenuItemId = itemDto.MenuItemId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = menuItem.Price ?? 0,
                    SpecialInstructions = itemDto.SpecialInstructions,
                    CreatedDate = DateTime.UtcNow
                };

                orderItems.Add(orderItem);
                totalAmount += orderItem.UnitPrice * orderItem.Quantity;

                // Business Logic: Track daily order count per menu item in DataRepository
                await _menuItemRepository.UpdateDailyOrderCountAsync(menuItem.Id, itemDto.Quantity);

                // Business Logic: Daily availability - Items with >50 orders become unavailable until midnight
                if (menuItem.DailyOrderCount + itemDto.Quantity > 50)
                {
                    menuItem.IsAvailable = false;
                    await _menuItemRepository.UpdateAsync(menuItem);
                }
            }

            // Business Logic: Calculate total with tax and discounts
            totalAmount = await ApplyDiscountsAndTaxAsync(totalAmount, orderDto.Type);
            order.TotalAmount = totalAmount;
            order.OrderItems = orderItems;

            // Business Logic: Set estimated delivery time
            if (orderDto.Type == OrderType.Delivery)
            {
                if (string.IsNullOrEmpty(orderDto.DeliveryAddress))
                    throw new ArgumentException("Delivery address is required for delivery orders");

                // Max preparation time of items + 30 minutes
                order.EstimatedDeliveryTime = DateTime.UtcNow.AddMinutes(GetPreparationTime(orderItems) + 30);
            }

            return await _orderRepository.AddAsync(order);
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _orderRepository.GetOrderWithItemsAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _orderRepository.GetOrdersByStatusAsync(status);
        }

        public async Task<Order> UpdateOrderStatusAsync(Guid orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new ArgumentException("Order not found");

            // Business Logic: Cannot cancel Ready or Delivered orders
            if (order.Status == OrderStatus.Ready || order.Status == OrderStatus.Completed || order.Status== OrderStatus.OutForDelivery)
            {
                if (status == OrderStatus.Cancelled)
                    throw new InvalidOperationException("Cannot cancel Ready , Delivered, or OutForDelivery orders");
            }

            order.Status = status;
            order.UpdatedDate = DateTime.UtcNow;

            if (status == OrderStatus.Completed)
                order.ActualDeliveryTime = DateTime.UtcNow;

            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<Order> CancelOrderAsync(Guid orderId)
        {
            return await UpdateOrderStatusAsync(orderId, OrderStatus.Cancelled);
        }

        public async Task<decimal> CalculateOrderTotalAsync(List<OrderItemDto> orderItems)
        {
            decimal total = 0;

            foreach (var item in orderItems)
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                if (menuItem != null)
                {
                    total += (menuItem.Price ?? 0) * item.Quantity;
                }
            }

            return total;
        }

        public async Task<Order> ProcessOrderAsync(Guid orderId)
        {
            // Business Logic: Automatic progression - Pending → Preparing (after 5 min), Preparing → Ready (after preparation time)
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;

            if (order.Status == OrderStatus.Pending &&
                DateTime.UtcNow > order.CreatedDate.AddMinutes(5))
            {
                await UpdateOrderStatusAsync(orderId, OrderStatus.Preparing);
            }

            return order;
        }

        private async Task<decimal> ApplyDiscountsAndTaxAsync(decimal amount, OrderType orderType)
        {
            decimal finalAmount = amount;

            // Business Logic: Happy hour - 20% off from 3-5 PM
            var currentHour = DateTime.Now.Hour;
            if (currentHour >= 15 && currentHour < 17)
            {
                finalAmount *= 0.8m; // 20% discount
            }

            // Business Logic: Bulk discount - 10% off orders over $100
            if (amount > 100)
            {
                finalAmount *= 0.9m; // 10% discount
            }

            // Business Logic: Add 8.5% tax
            finalAmount *= 1.085m;

            return Math.Round(finalAmount, 2);
        }

        private int GetPreparationTime(List<OrderItem> orderItems)
        {
            // Simple logic: 5 minutes per item type, minimum 15 minutes
            return Math.Max(15, orderItems.Count * 5);
        }
    }
}
