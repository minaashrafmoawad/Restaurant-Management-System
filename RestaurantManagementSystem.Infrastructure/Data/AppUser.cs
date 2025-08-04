using Microsoft.AspNetCore.Identity;
using RestaurantManagementSystem.Domain.Interfaces;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Infrastructure.Data
{
    public class AppUser : IdentityUser<Guid>,IAuditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Default to UTC now
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true; // User is active by default
        public bool IsDeleted { get; set; } = false; 
        public DateTime? LastLoginDate { get; set; }


        public decimal? Salary { get; set; } // for employees
        public DateTime? HiringDate { get; set; }// for employees
        

        public ICollection<Order>? Orders { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
    }
}
