using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Domain.Enums;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services_Contracts
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderDto orderDto);
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<Order> UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
        Task<Order> CancelOrderAsync(Guid orderId);
        Task<decimal> CalculateOrderTotalAsync(List<OrderItemDto> orderItems);
        Task<Order> ProcessOrderAsync(Guid orderId);
    }
}
