using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Repository_Contracts
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem>
    {
        Task<IEnumerable<MenuItem>> GetAvailableItemsAsync();
        Task<IEnumerable<MenuItem>> GetItemsByCategoryAsync(Guid categoryId);
        Task UpdateDailyOrderCountAsync(Guid menuItemId, int count);
        Task ResetDailyCountsAsync();
    }
}
