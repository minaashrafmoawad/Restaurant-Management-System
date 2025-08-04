using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity; // For UserManager, RoleManager, IdentityRole
using Microsoft.EntityFrameworkCore;   // For DbContext, UseSqlServer
using RestaurantManagementSystem.Application.BackgroundServices;
using RestaurantManagementSystem.Application.Repository_Contracts;
using RestaurantManagementSystem.Application.Services;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Models; // For AppUser
using RestaurantManagementSystem.Infrastructure.Data;
using RestaurantManagementSystem.Infrastructure.Repositories; // For AppDbContext and SeedData

namespace Restaurant_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ➤ 1. Configure DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ➤ 2. Register Notyf
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopRight;
            });

            // ➤ 3. Register Repositories
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<ITableReservationRepository, TableReservationRepository>();
            builder.Services.AddScoped<ISalesTransactionRepository, SalesTransactionRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();


            // ➤ 4. Register Services
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ITableReservationService, TableReservationService>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IOrderProcessingService, OrderProcessingService>();
            builder.Services.AddScoped<IInventoryService, InventoryService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ITableService, TableService>();

            // ➤ 5. MVC and API Support
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();

            // ➤ 6. CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // ➤ 7. Configure Middleware
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthorization();

            // ➤ 8. Routes
            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=MenuView}/{action=Index}/{id?}");

            // ➤ 9. Ensure DB Created
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
            }

            app.Run();
        }


            //public static async Task Main(string[] args) // Changed to async Task Main
            //{



            //    ///-----------------------
            //    //var builder = WebApplication.CreateBuilder(args);

            //    //// Add services to the container.
            //    //builder.Services.AddControllersWithViews();

            //    //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
            //    //                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            //    //// Configure AppDbContext
            //    //builder.Services.AddDbContext<AppDbContext>(options =>
            //    //    options.UseSqlServer(connectionString));

            //    //// --- Configure ASP.NET Core Identity Services ---
            //    //builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            //    //{
            //    //    // Identity password options
            //    //    options.Password.RequireDigit = true;
            //    //    options.Password.RequireLowercase = true;
            //    //    options.Password.RequireUppercase = true;
            //    //    options.Password.RequireNonAlphanumeric = true;
            //    //    options.Password.RequiredLength = 8;
            //    //    options.User.RequireUniqueEmail = true; // Ensure emails are unique
            //    //    // Add any other Identity options as needed (e.g., lockout, signin)
            //    //})
            //    //.AddEntityFrameworkStores<AppDbContext>() // Tell Identity to use AppDbContext for storage
            //    //.AddDefaultTokenProviders(); // Adds token providers for things like password resets, email confirmations

            //    //var app = builder.Build();

            //    //// --- Database Seeding during application startup ---
            //    //// This ensures that the database is populated with initial data and users/roles
            //    //// when the application starts up.
            //    //using (var scope = app.Services.CreateScope())
            //    //{
            //    //    var services = scope.ServiceProvider;

            //    //        var context = services.GetRequiredService<AppDbContext>();
            //    //        var userManager = services.GetRequiredService<UserManager<AppUser>>();
            //    //        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();


            //    //        await SeedData.Initialize(context, userManager, roleManager);

            //    //}
            //    //// --- End Database Seeding ---


            //    //// Configure the HTTP request pipeline.
            //    //if (!app.Environment.IsDevelopment())
            //    //{
            //    //    app.UseExceptionHandler("/Home/Error");
            //    //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    //    app.UseHsts();
            //    //}

            //    //app.UseHttpsRedirection();
            //    //app.UseStaticFiles(); // Serves wwwroot content (CSS, JS, images)

            //    //app.UseRouting();

            //    //// --- Authentication and Authorization Middleware ---
            //    //// These must be placed AFTER app.UseRouting() and BEFORE app.MapControllerRoute()
            //    //app.UseAuthentication(); // Enables authentication features (e.g., login, logout)
            //    //app.UseAuthorization();  // Enables authorization checks (e.g., [Authorize] attribute)

            //    //app.MapControllerRoute(
            //    //    name: "default",
            //    //    pattern: "{controller=Home}/{action=Index}/{id?}");

            //    //app.Run();

            //}

        }
    }