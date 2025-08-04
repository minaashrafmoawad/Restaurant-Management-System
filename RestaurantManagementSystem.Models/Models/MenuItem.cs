using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Models
{
    public class MenuItem:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageURL { get; set; }
        public decimal? Price { get; set; }
        
        public bool IsAvailable { get; set; }
        public int DailyOrderCount { get; set; }
        [ForeignKey(nameof(Category))]
        [Required]
        public Guid CategoryId { get; set; }

        public Category? Category { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
