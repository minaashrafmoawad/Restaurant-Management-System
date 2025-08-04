using RestaurantManagementSystem.Application.Services_Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.BackgroundServices
{
    public class InventoryService : IInventoryService
    {
        private readonly IMenuService _menuService;

        public InventoryService(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task ResetDailyInventoryAsync()
        {
            // Business Logic: Reset availability at midnight
            await _menuService.ResetDailyAvailabilityAsync();
        }

        public async Task UpdateItemAvailabilityAsync()
        {
            // This would typically check inventory levels and update availability
            // For now, it's handled in the order creation process
            await Task.CompletedTask;
        }
    }
}
