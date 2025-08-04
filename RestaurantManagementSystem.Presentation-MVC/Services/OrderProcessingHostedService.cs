using RestaurantManagementSystem.Application.BackgroundServices;

namespace Restaurant_Management_System.Services
{
    public class OrderProcessingHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OrderProcessingHostedService> _logger;

        public OrderProcessingHostedService(IServiceProvider serviceProvider, ILogger<OrderProcessingHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var orderProcessingService = scope.ServiceProvider.GetRequiredService<IOrderProcessingService>();
                    var inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryService>();

                    await orderProcessingService.ProcessPendingOrdersAsync();
                    await orderProcessingService.UpdateOrderStatusesAsync();
                    await orderProcessingService.SendCustomerNotificationsAsync();

                    // Reset daily inventory at midnight
                    if (DateTime.Now.Hour == 0 && DateTime.Now.Minute < 5)
                    {
                        await inventoryService.ResetDailyInventoryAsync();
                    }

                    _logger.LogInformation("Order processing completed at {Time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during order processing");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Run every 5 minutes
            }
        }
    }
}
