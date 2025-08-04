using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Application.Repository_Contracts;
using RestaurantManagementSystem.Domain.Enums;
using RestaurantManagementSystem.Domain.Models;
using RestaurantManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _dbSet
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.MenuItem)
                .Where(x => x.Status == status && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(Guid customerId)
        {
            return await _dbSet
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.MenuItem)
                .Where(x => x.CustomerId == customerId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<Order> GetOrderWithItemsAsync(Guid orderId)
        {
            return await _dbSet
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.MenuItem)
                .FirstOrDefaultAsync(x => x.Id == orderId && !x.IsDeleted);
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.MenuItem)
                .Where(x => x.CreatedDate >= startDate && x.CreatedDate < endDate && !x.IsDeleted)
                .ToListAsync();
        }
    }
}
