using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Repository_Contracts;
using RestaurantManagementSystem.Application.Services_Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ISalesTransactionRepository _transactionRepository;
        private readonly IOrderRepository _orderRepository;

        public AnalyticsService(ISalesTransactionRepository transactionRepository, IOrderRepository orderRepository)
        {
            _transactionRepository = transactionRepository;
            _orderRepository = orderRepository;
        }

        public async Task<SalesReportDto> GenerateDailySalesReportAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);
            var orders = await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);

            var totalSales = transactions.Sum(t => t.Amount);
            var totalOrders = orders.Count();
            var averageOrderValue = totalOrders > 0 ? totalSales / totalOrders : 0;

            var popularItems = await GetPopularItemsAsync(startDate, endDate);

            return new SalesReportDto
            {
                Date = date,
                TotalSales = totalSales,
                TotalOrders = totalOrders,
                AverageOrderValue = averageOrderValue,
                PopularItems = popularItems
            };
        }

        public async Task<Dictionary<string, int>> GetPopularItemsAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);

            var itemCounts = new Dictionary<string, int>();
            foreach (var order in orders)
            {
                foreach (var item in order.OrderItems)
                {
                    var menuItemName = item.MenuItem?.Name ?? "Unknown Item";
                    if (itemCounts.ContainsKey(menuItemName))
                        itemCounts[menuItemName] += item.Quantity;
                    else
                        itemCounts[menuItemName] = item.Quantity;
                }
            }

            return itemCounts.OrderByDescending(x => x.Value)
                           .Take(10)
                           .ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            return await _transactionRepository.GetTotalSalesAsync(startDate, endDate);
        }

        public async Task<Dictionary<int, decimal>> GetSalesByHourAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);

            var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);

            return transactions
                .GroupBy(t => t.CreatedDate.Hour)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
        }
    }
}

