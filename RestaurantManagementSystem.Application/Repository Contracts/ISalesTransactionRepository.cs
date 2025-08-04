using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Repository_Contracts
{
    public interface ISalesTransactionRepository : IGenericRepository<SalesTransaction>
    {
        Task<IEnumerable<SalesTransaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate);
    }
}
