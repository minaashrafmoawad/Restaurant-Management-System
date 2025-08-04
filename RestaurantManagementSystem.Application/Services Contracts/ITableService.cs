using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services_Contracts
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table> GetTableByIdAsync(Guid id);
        Task<Table> CreateTableAsync(Table table);
        Task<Table> UpdateTableAsync(Table table);
        Task DeleteTableAsync(Guid id);
        Task<IEnumerable<Table>> GetAvailableTablesAsync();
        Task<Table> GetTableByNumberAsync(int number);
        Task UpdateTableAvailabilityAsync(Guid tableId, bool isAvailable);
    }
}
