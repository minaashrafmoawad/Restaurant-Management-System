using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Repository_Contracts
{
    public interface ITableReservationRepository : IGenericRepository<TableReservation>
    {
        Task<IEnumerable<TableReservation>> GetReservationsByDateAsync(DateTime date);
        Task<IEnumerable<TableReservation>> GetReservationsByCustomerAsync(Guid customerId);
        Task<bool> IsTableAvailableAsync(Guid tableId, DateTime date, TimeSpan time);
    }
}
