using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Enums;
using RestaurantManagementSystem.Domain.Models;

namespace Restaurant_Management_System.Controllers
{
     
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("daily-report/{date}")]
        public async Task<ActionResult<SalesReportDto>> GetDailySalesReport(DateTime date)
        {
            try
            {
                var report = await _analyticsService.GenerateDailySalesReportAsync(date);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("popular-items")]
        public async Task<ActionResult<Dictionary<string, int>>> GetPopularItems(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var popularItems = await _analyticsService.GetPopularItemsAsync(startDate, endDate);
                return Ok(popularItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("total-sales")]
        public async Task<ActionResult<decimal>> GetTotalSales(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var totalSales = await _analyticsService.GetTotalSalesAsync(startDate, endDate);
                return Ok(new { totalSales });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("sales-by-hour/{date}")]
        public async Task<ActionResult<Dictionary<int, decimal>>> GetSalesByHour(DateTime date)
        {
            try
            {
                var salesByHour = await _analyticsService.GetSalesByHourAsync(date);
                return Ok(salesByHour);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}


