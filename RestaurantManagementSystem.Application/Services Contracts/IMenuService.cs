using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services_Contracts
{
    public interface IMenuService
    {
 

        Task<IEnumerable<Category>> GetCategoriesWithItemsAsync();
        Task<IEnumerable<MenuItem>> GetAvailableMenuItemsAsync();
        Task<MenuItem> GetMenuItemByIdAsync(Guid id);
        Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem);
        Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(Guid id);
        Task UpdateItemAvailabilityAsync(Guid menuItemId, bool isAvailable);
        Task ResetDailyAvailabilityAsync();
    }
}
