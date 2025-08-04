using RestaurantManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services_Contracts
{
    public interface IAnalyticsService
    {
        Task<SalesReportDto> GenerateDailySalesReportAsync(DateTime date);
        Task<Dictionary<string, int>> GetPopularItemsAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate);
        Task<Dictionary<int, decimal>> GetSalesByHourAsync(DateTime date);
    }
}
