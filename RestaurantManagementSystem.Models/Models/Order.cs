using RestaurantManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Models
{
    public class Order:BaseEntity
    {

        // Foreign Key to AppUser (Customer) => this relation configured in AppUser Configration
        // in the Infrastructure layer respecting Onion arch.
        public Guid? CustomerId { get; set; }

        // Foreign Key to SalesTransaction 
        public int? SalesTransactionId { get; set; }

        // Order details
        public OrderType Type { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }
        public string? SpecialInstructions { get; set; }

        // Time-related fields
        public DateTime? EstimatedDeliveryTime { get; set; }
        public DateTime? ActualDeliveryTime { get; set; }
        public DateTime? PickupTime { get; set; } // For takeout orders

        
        public string? DeliveryAddress { get; set; } // For delivery orders only

        // Collection navigation property (one-to-many)
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        
    }
}
