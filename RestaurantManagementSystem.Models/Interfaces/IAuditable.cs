using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Domain.Interfaces
{
    public interface IAuditable
    {
        Guid Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        bool IsDeleted { get; set; }
        Guid? CreatedById { get; set; }
        Guid? UpdatedById { get; set; }

    }
    
}
