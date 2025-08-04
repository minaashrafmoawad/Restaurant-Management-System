using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Models
{
    public class TableReservation:BaseEntity
    {
        [ForeignKey(nameof(Table))]
        public Guid TableId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
 
        public Table? Table { get; set; }

    }
}
