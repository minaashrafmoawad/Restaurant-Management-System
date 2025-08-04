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
    public class TableReservationRepository : GenericRepository<TableReservation>, ITableReservationRepository
    {
        public TableReservationRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<TableReservation>> GetReservationsByDateAsync(DateTime date)
        {
            return await _dbSet
                .Include(x => x.Table)
                .Where(x => x.Date.Date == date.Date && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<TableReservation>> GetReservationsByCustomerAsync(Guid customerId)
        {
            return await _dbSet
                .Include(x => x.Table)
                .Where(x => x.CustomerId == customerId && !x.IsDeleted)
                .OrderByDescending(x => x.Date)
            .ToListAsync();
        }

        public async Task<bool> IsTableAvailableAsync(Guid tableId, DateTime date, TimeSpan time)
        {
            var existingReservation = await _dbSet
                .FirstOrDefaultAsync(x => x.TableId == tableId &&
                                    x.Date.Date == date.Date &&
                                    Math.Abs((x.Time - time).TotalMinutes) < 120 && // 2-hour buffer
                                    !x.IsDeleted);

            return existingReservation == null;
        }
    }
}
