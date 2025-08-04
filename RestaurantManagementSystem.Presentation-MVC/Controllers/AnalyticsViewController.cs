using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.Services_Contracts;

namespace Restaurant_Management_System.Controllers
{
    public class AnalyticsViewController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsViewController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);
            var weekAgo = today.AddDays(-7);

            var todayReport = await _analyticsService.GenerateDailySalesReportAsync(today);
            var yesterdayReport = await _analyticsService.GenerateDailySalesReportAsync(yesterday);
            var weeklyTotal = await _analyticsService.GetTotalSalesAsync(weekAgo, today);
            var popularItems = await _analyticsService.GetPopularItemsAsync(weekAgo, today);

            ViewBag.TodayReport = todayReport;
            ViewBag.YesterdayReport = yesterdayReport;
            ViewBag.WeeklyTotal = weeklyTotal;
            ViewBag.PopularItems = popularItems;

            return View();
        }

        public async Task<IActionResult> SalesReport(DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? DateTime.Today.AddDays(-30);
            var end = endDate ?? DateTime.Today;

            var totalSales = await _analyticsService.GetTotalSalesAsync(start, end);
            var popularItems = await _analyticsService.GetPopularItemsAsync(start, end);

            ViewBag.StartDate = start;
            ViewBag.EndDate = end;
            ViewBag.TotalSales = totalSales;
            ViewBag.PopularItems = popularItems;

            return View();
        }
    }
}
