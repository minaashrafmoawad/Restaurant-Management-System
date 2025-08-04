using RestaurantManagementSystem.Application.BackgroundServices;
using RestaurantManagementSystem.Application.Repository_Contracts;
using RestaurantManagementSystem.Application.Services;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Infrastructure.Repositories;

namespace Restaurant_Management_System.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<ITableReservationRepository, TableReservationRepository>();
            services.AddScoped<ISalesTransactionRepository, SalesTransactionRepository>();

            return services;
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ITableReservationService, TableReservationService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<IOrderProcessingService, OrderProcessingService>();
            services.AddScoped<IInventoryService, InventoryService>();

            return services;
        }
    }
}
