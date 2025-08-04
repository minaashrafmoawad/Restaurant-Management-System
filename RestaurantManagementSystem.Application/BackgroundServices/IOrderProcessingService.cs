using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.BackgroundServices
{
    public interface IOrderProcessingService
    {
        Task ProcessPendingOrdersAsync();
        Task UpdateOrderStatusesAsync();
        Task SendCustomerNotificationsAsync();
    }
}
