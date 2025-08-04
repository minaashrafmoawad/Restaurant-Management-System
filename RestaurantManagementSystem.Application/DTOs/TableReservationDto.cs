using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.DTOs
{
    public class TableReservationDto
    {
        //public required string UserPhoneNumber { get; set; }
       public Guid CustomerId {  get; set; }
        public int TableNumber { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
