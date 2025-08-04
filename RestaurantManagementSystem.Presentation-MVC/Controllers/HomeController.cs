using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Make sure this is included for ILogger
using Restaurant_Management_System.Models; // Assuming ErrorViewModel is here
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Models; // For AppUser
using RestaurantManagementSystem.Infrastructure.Data; // For AppDbContext

namespace Restaurant_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IMenuService menuService, IAnalyticsService analyticsService)
        {
            _menuService = menuService;
            _analyticsService = analyticsService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var todayReport = await _analyticsService.GenerateDailySalesReportAsync(DateTime.Today);
                ViewBag.TodaySales = todayReport.TotalSales;
                ViewBag.TodayOrders = todayReport.TotalOrders;
                ViewBag.PopularItems = todayReport.PopularItems;

                var categories = await _menuService.GetCategoriesWithItemsAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<Category>());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }

   
}