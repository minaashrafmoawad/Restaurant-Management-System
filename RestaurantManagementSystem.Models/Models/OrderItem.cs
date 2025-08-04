using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Models
{
    public class OrderItem:BaseEntity
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        [ForeignKey(nameof(MenuItem))]
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? SpecialInstructions { get; set; }

        public Order? Order { get; set; }
        public MenuItem? MenuItem { get; set; }

    }
}
