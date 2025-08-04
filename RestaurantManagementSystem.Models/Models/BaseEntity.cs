using RestaurantManagementSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Models
{
    public abstract class BaseEntity:IAuditable
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }= false;
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
    }
}
