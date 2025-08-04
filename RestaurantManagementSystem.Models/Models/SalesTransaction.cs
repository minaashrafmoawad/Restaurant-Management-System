using RestaurantManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Models
{
    public class SalesTransaction:BaseEntity
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; } // total amount of order
        public PaymentMethod PaymentMethod { get; set; }
        public string? PaymentReference { get; set; }
        public Order? Order { get; set; }
    }
}
