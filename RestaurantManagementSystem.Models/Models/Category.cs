using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Models
{
    public class Category:BaseEntity
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MenuItem>? MenuItems { get; set; }
    }
}
