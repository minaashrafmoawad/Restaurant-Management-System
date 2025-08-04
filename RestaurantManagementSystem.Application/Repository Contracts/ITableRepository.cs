using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Repository_Contracts
{
    public interface ITableRepository : IGenericRepository<Table>
    {
        Task<IEnumerable<Table>> GetAvailableTablesAsync();
        Task<Table> GetTableByNumberAsync(int number);
    }
}
