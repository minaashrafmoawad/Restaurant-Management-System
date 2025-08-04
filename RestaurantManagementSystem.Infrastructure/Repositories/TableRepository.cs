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
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        public TableRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync()
        {
            return await _dbSet
                .Where(x => x.IsAvailable && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<Table> GetTableByNumberAsync(int number)
        {
            return await _dbSet
                  .FirstOrDefaultAsync(x => x.Number == number);
        }
    }
}
