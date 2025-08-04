using RestaurantManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.DTOs
{
    public class CreateOrderDto
    {
        public Guid? CustomerId { get; set; }
        public OrderType Type { get; set; }
        public string? SpecialInstructions { get; set; }
        public string? DeliveryAddress { get; set; }
        public DateTime? PickupTime { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}
