using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Application.Repository_Contracts;
using RestaurantManagementSystem.Domain.Models;
using RestaurantManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Infrastructure.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<MenuItem>> GetAvailableItemsAsync()
        {
            return await _dbSet
                .Include(x => x.Category)
                .Where(x => x.IsAvailable && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetItemsByCategoryAsync(Guid categoryId)
        {
            return await _dbSet
                .Include(x => x.Category)
                .Where(x => x.CategoryId == categoryId && x.IsAvailable && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task UpdateDailyOrderCountAsync(Guid menuItemId, int count)
        {
            var item = await GetByIdAsync(menuItemId);
            if (item != null)
            {
                item.DailyOrderCount += count;
                await UpdateAsync(item);
            }
        }

        public async Task ResetDailyCountsAsync()
        {
            var items = await _dbSet.Where(x => !x.IsDeleted).ToListAsync();
            foreach (var item in items)
            {
                item.DailyOrderCount = 0;
                item.IsAvailable = true;
            }
            await _context.SaveChangesAsync();
        }
    }
}
