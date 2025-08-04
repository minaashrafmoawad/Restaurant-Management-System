using RestaurantManagementSystem.Application.Repository_Contracts;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MenuService(IMenuItemRepository menuItemRepository, ICategoryRepository categoryRepository)
        {
            _menuItemRepository = menuItemRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithItemsAsync()
        {
            return await _categoryRepository.GetCategoriesWithAvailableMenuItemsAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetAvailableMenuItemsAsync()
        {
            var items = await _menuItemRepository.GetAvailableItemsAsync();

            // Business Logic: Hide categories with no active items
            return items.Where(item => item.IsAvailable && !item.IsDeleted);
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(Guid id)
        {
            return await _menuItemRepository.GetByIdAsync(id);
        }

        public async Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem)
        {
            // Business Logic: Validate price is positive
            if (menuItem.Price <= 0)
                throw new ArgumentException("Price must be positive");

            return await _menuItemRepository.AddAsync(menuItem);
        }

        public async Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem)
        {
            // Business Logic: Validate price is positive
            if (menuItem.Price <= 0)
                throw new ArgumentException("Price must be positive");

            menuItem.UpdatedDate = DateTime.UtcNow;
            return await _menuItemRepository.UpdateAsync(menuItem);
        }

        public async Task DeleteMenuItemAsync(Guid id)
        {
            await _menuItemRepository.DeleteAsync(id);
        }

        public async Task UpdateItemAvailabilityAsync(Guid menuItemId, bool isAvailable)
        {
            var item = await _menuItemRepository.GetByIdAsync(menuItemId);
            if (item != null)
            {
                item.IsAvailable = isAvailable;
                item.UpdatedDate = DateTime.UtcNow;
                await _menuItemRepository.UpdateAsync(item);
            }
        }

        public async Task ResetDailyAvailabilityAsync()
        {
            // Business Logic: Reset availability at midnight (simulate with static flag or timestamp check)
            await _menuItemRepository.ResetDailyCountsAsync();
        }
    }
}
