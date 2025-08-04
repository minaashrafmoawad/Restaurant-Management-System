using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Application.Repository_Contracts;
using RestaurantManagementSystem.Domain.Models;
using RestaurantManagementSystem.Infrastructure.Data;
using RestaurantManagementSystem.Infrastructure.Repositories;

namespace Restaurant_Management_System
{
    public class SalesTransactionRepository : GenericRepository<SalesTransaction>, ISalesTransactionRepository
    {
        public SalesTransactionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<SalesTransaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(x => x.Order)
                .Where(x => x.CreatedDate >= startDate && x.CreatedDate < endDate && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(x => x.CreatedDate >= startDate && x.CreatedDate < endDate && !x.IsDeleted)
                .SumAsync(x => x.Amount);
        }
    }
}
