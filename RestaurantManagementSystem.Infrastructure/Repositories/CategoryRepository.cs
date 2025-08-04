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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }





        public async Task<IEnumerable<Category>> GetCategoriesWithAvailableMenuItemsAsync()
        {
            return await _dbSet
                .Include(x => x.MenuItems.Where(m => m.IsAvailable && !m.IsDeleted)) // Eager load MenuItems and filter them
                .Where(x => !x.IsDeleted && x.MenuItems.Any(m => m.IsAvailable && !m.IsDeleted)) // Filter categories: not deleted, AND has at least one available, non-deleted menu item
                .ToListAsync();
        }
    }
}
