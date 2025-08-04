using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Models
{
    public class Table:BaseEntity
    {
        public int Number {  get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }=true;
    }
}
