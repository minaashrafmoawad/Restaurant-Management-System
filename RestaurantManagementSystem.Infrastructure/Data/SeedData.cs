//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore; // Required for DbContext and Async operations
//using RestaurantManagementSystem.Domain.Models; // Your domain models
//using RestaurantManagementSystem.Domain.Enums; // Your enums
//using RestaurantManagementSystem.Infrastructure.Data; // Namespace for your AppDbContext

//public static class SeedData
//{
//    /// <summary>
//    /// Initializes the database with sample data if it's empty.
//    /// This method is typically called during application startup (e.g., in Program.cs).
//    /// </summary>
//    /// <param name="context">The DbContext instance to seed data into.</param>
//    public static async Task Initialize(AppDbContext context) // Changed to AppDbContext
//    {

//        //----------------------------------------------------------------------------------------------------------------
//         #region
//        // Ensure the database is created if it doesn't exist.
//        // In a production environment, you might use context.Database.Migrate() instead.
//        await context.Database.EnsureCreatedAsync();

//        // Check if any categories already exist. If so, the database has likely been seeded.
//        if (context.Categories.Any())
//        {
//            Console.WriteLine("Database already contains data. Skipping seeding.");
//            return; // DB has been seeded
//        }

//        Console.WriteLine("Seeding database with initial data...");
//        var random = new Random();

//        // --- 1. Seed Categories (5 categories as requested) ---
//        var categories = new List<Category>
//        {
//            // Id and CreatedDate will be handled by DbContext configuration and UpdateAuditFields
//            new Category { Name = "Appetizers", Description = "Delicious starters to begin your meal." },
//            new Category { Name = "Main Courses", Description = "Hearty and fulfilling main dishes for a complete experience." },
//            new Category { Name = "Desserts", Description = "Sweet treats to end your dining experience." },
//            new Category { Name = "Beverages", Description = "Refreshing drinks to complement your meal." },
//            new Category { Name = "Breakfast", Description = "Perfect morning delights to start your day." }
//        };
//        await context.Categories.AddRangeAsync(categories);
//        await context.SaveChangesAsync(); // Save to get the generated IDs for categories

//        // Retrieve the generated IDs after saving changes, as they are needed for related entities.
//        categories = await context.Categories.OrderBy(c => c.Name).ToListAsync(); // Order to ensure consistent indexing

//        Console.WriteLine("Seeded Categories.");

//        // --- 2. Seed Menu Items (7 per category, 35 total) ---
//        var menuItems = new List<MenuItem>();

//        // Appetizers (Category 1)
//        menuItems.Add(new MenuItem { Name = "Spring Rolls", Description = "Crispy vegetable spring rolls with sweet chili dip.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Spring+Rolls", Price = 7.50M, IsAvailable = true, DailyOrderCount = 10, CategoryId = categories[0].Id });
//        menuItems.Add(new MenuItem { Name = "Garlic Bread", Description = "Toasted baguette slices with fragrant garlic butter and herbs.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Garlic+Bread", Price = 5.00M, IsAvailable = true, DailyOrderCount = 15, CategoryId = categories[0].Id });
//        menuItems.Add(new MenuItem { Name = "Crispy Calamari", Description = "Golden fried calamari rings served with zesty tartar sauce.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Calamari", Price = 12.00M, IsAvailable = true, DailyOrderCount = 5, CategoryId = categories[0].Id });
//        menuItems.Add(new MenuItem { Name = "Buffalo Wings", Description = "Spicy chicken wings tossed in buffalo sauce, served with blue cheese dip.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Buffalo+Wings", Price = 10.00M, IsAvailable = true, DailyOrderCount = 20, CategoryId = categories[0].Id });
//        menuItems.Add(new MenuItem { Name = "Mozzarella Sticks", Description = "Melted mozzarella cheese sticks, breaded and fried to perfection. (Currently Unavailable)", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Mozzarella+Sticks", Price = 8.00M, IsAvailable = false, DailyOrderCount = 0, CategoryId = categories[0].Id }); // Unavailable item
//        menuItems.Add(new MenuItem { Name = "Bruschetta", Description = "Toasted bread topped with fresh diced tomatoes, garlic, and basil.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Bruschetta", Price = 6.50M, IsAvailable = true, DailyOrderCount = 12, CategoryId = categories[0].Id });
//        menuItems.Add(new MenuItem { Name = "Loaded Nachos", Description = "Crispy tortilla chips smothered in cheese, jalapenos, and salsa.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Loaded+Nachos", Price = 13.50M, IsAvailable = true, DailyOrderCount = 9, CategoryId = categories[0].Id });

//        // Main Courses (Category 2)
//        menuItems.Add(new MenuItem { Name = "Grilled Salmon", Description = "Pan-seared salmon fillet served with asparagus and lemon-dill sauce.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Grilled+Salmon", Price = 22.00M, IsAvailable = true, DailyOrderCount = 8, CategoryId = categories[1].Id });
//        menuItems.Add(new MenuItem { Name = "Ribeye Steak", Description = "Juicy ribeye steak cooked to your preference, with roasted potatoes.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Ribeye+Steak", Price = 28.00M, IsAvailable = true, DailyOrderCount = 10, CategoryId = categories[1].Id });
//        menuItems.Add(new MenuItem { Name = "Fettuccine Alfredo", Description = "Creamy fettuccine pasta with Parmesan cheese and a rich Alfredo sauce.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Fettuccine+Alfredo", Price = 18.00M, IsAvailable = true, DailyOrderCount = 12, CategoryId = categories[1].Id });
//        menuItems.Add(new MenuItem { Name = "Chicken Tikka Masala", Description = "Tender chicken pieces in a rich, creamy tomato sauce, served with naan.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Chicken+Tikka", Price = 19.50M, IsAvailable = true, DailyOrderCount = 7, CategoryId = categories[1].Id });
//        menuItems.Add(new MenuItem { Name = "Vegetable Lasagna", Description = "Layers of pasta, fresh vegetables, ricotta, and mozzarella cheese.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Veg+Lasagna", Price = 16.00M, IsAvailable = true, DailyOrderCount = 6, CategoryId = categories[1].Id });
//        menuItems.Add(new MenuItem { Name = "Classic Cheeseburger", Description = "Grilled beef patty with cheddar cheese, lettuce, tomato, and pickles.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Cheeseburger", Price = 15.00M, IsAvailable = false, DailyOrderCount = 0, CategoryId = categories[1].Id }); // Unavailable
//        menuItems.Add(new MenuItem { Name = "Fish and Chips", Description = "Crispy battered cod served with thick-cut fries and malt vinegar.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Fish+and+Chips", Price = 17.00M, IsAvailable = true, DailyOrderCount = 9, CategoryId = categories[1].Id });

//        // Desserts (Category 3)
//        menuItems.Add(new MenuItem { Name = "Chocolate Lava Cake", Description = "Warm chocolate cake with a gooey molten center, served with vanilla ice cream.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Lava+Cake", Price = 9.00M, IsAvailable = true, DailyOrderCount = 12, CategoryId = categories[2].Id });
//        menuItems.Add(new MenuItem { Name = "New York Cheesecake", Description = "Creamy, classic New York style cheesecake with berry compote.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Cheesecake", Price = 8.50M, IsAvailable = true, DailyOrderCount = 10, CategoryId = categories[2].Id });
//        menuItems.Add(new MenuItem { Name = "Tiramisu", Description = "Traditional Italian coffee-flavored dessert with ladyfingers and mascarpone.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Tiramisu", Price = 9.50M, IsAvailable = true, DailyOrderCount = 7, CategoryId = categories[2].Id });
//        menuItems.Add(new MenuItem { Name = "Seasonal Fruit Platter", Description = "A refreshing selection of fresh seasonal fruits.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Fruit+Platter", Price = 7.00M, IsAvailable = true, DailyOrderCount = 15, CategoryId = categories[2].Id });
//        menuItems.Add(new MenuItem { Name = "Assorted Ice Cream", Description = "Two scoops of your choice: vanilla, chocolate, or strawberry.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Ice+Cream", Price = 6.00M, IsAvailable = true, DailyOrderCount = 20, CategoryId = categories[2].Id });
//        menuItems.Add(new MenuItem { Name = "Crème brûlée", Description = "Rich vanilla bean custard base topped with a layer of hardened caramelized sugar.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Creme+Brulee", Price = 10.00M, IsAvailable = true, DailyOrderCount = 5, CategoryId = categories[2].Id });
//        menuItems.Add(new MenuItem { Name = "Apple Pie", Description = "Warm apple pie with a scoop of vanilla ice cream. (Currently Unavailable)", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Apple+Pie", Price = 8.00M, IsAvailable = false, DailyOrderCount = 0, CategoryId = categories[2].Id }); // Unavailable

//        // Beverages (Category 4)
//        menuItems.Add(new MenuItem { Name = "Coca-Cola", Description = "Classic refreshing Coca-Cola.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Coca-Cola", Price = 3.00M, IsAvailable = true, DailyOrderCount = 30, CategoryId = categories[3].Id });
//        menuItems.Add(new MenuItem { Name = "Fresh Orange Juice", Description = "Freshly squeezed orange juice, rich in Vitamin C.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Orange+Juice", Price = 4.50M, IsAvailable = true, DailyOrderCount = 25, CategoryId = categories[3].Id });
//        menuItems.Add(new MenuItem { Name = "Espresso", Description = "Strong, concentrated coffee shot.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Espresso", Price = 4.00M, IsAvailable = true, DailyOrderCount = 40, CategoryId = categories[3].Id });
//        menuItems.Add(new MenuItem { Name = "Green Tea", Description = "Calming green tea, served hot or iced.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Green+Tea", Price = 3.50M, IsAvailable = true, DailyOrderCount = 28, CategoryId = categories[3].Id });
//        menuItems.Add(new MenuItem { Name = "Sparkling Water", Description = "Crisp, bubbly sparkling water.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Sparkling+Water", Price = 2.50M, IsAvailable = true, DailyOrderCount = 50, CategoryId = categories[3].Id });
//        menuItems.Add(new MenuItem { Name = "Berry Smoothie", Description = "Blended fresh berries with yogurt and a hint of honey.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Berry+Smoothie", Price = 6.00M, IsAvailable = true, DailyOrderCount = 18, CategoryId = categories[3].Id });
//        menuItems.Add(new MenuItem { Name = "Iced Latte", Description = "Chilled espresso with milk and ice.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Iced+Latte", Price = 5.50M, IsAvailable = true, DailyOrderCount = 15, CategoryId = categories[3].Id });

//        // Breakfast (Category 5)
//        menuItems.Add(new MenuItem { Name = "Fluffy Pancakes", Description = "Stack of golden pancakes served with maple syrup and butter.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Pancakes", Price = 11.00M, IsAvailable = true, DailyOrderCount = 10, CategoryId = categories[4].Id });
//        menuItems.Add(new MenuItem { Name = "Classic Omellette", Description = "Three-egg omelette with your choice of cheese, ham, or vegetables.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Omellette", Price = 13.00M, IsAvailable = true, DailyOrderCount = 8, CategoryId = categories[4].Id });
//        menuItems.Add(new MenuItem { Name = "Breakfast Burrito", Description = "Warm tortilla filled with scrambled eggs, sausage, cheese, and salsa.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Breakfast+Burrito", Price = 12.50M, IsAvailable = true, DailyOrderCount = 7, CategoryId = categories[4].Id });
//        menuItems.Add(new MenuItem { Name = "Belgian Waffles", Description = "Crispy Belgian waffles topped with fresh berries and whipped cream.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Waffles", Price = 10.50M, IsAvailable = true, DailyOrderCount = 9, CategoryId = categories[4].Id });
//        menuItems.Add(new MenuItem { Name = "Avocado Toast", Description = "Toasted sourdough with smashed avocado, poached egg, and everything bagel seasoning.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Avocado+Toast", Price = 14.00M, IsAvailable = true, DailyOrderCount = 6, CategoryId = categories[4].Id });
//        menuItems.Add(new MenuItem { Name = "French Toast", Description = "Thick-cut bread dipped in cinnamon egg batter, grilled, and dusted with powdered sugar.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=French+Toast", Price = 11.50M, IsAvailable = true, DailyOrderCount = 5, CategoryId = categories[4].Id });
//        menuItems.Add(new MenuItem { Name = "Yogurt Parfait", Description = "Layers of Greek yogurt, granola, and fresh mixed berries.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Yogurt+Parfait", Price = 9.00M, IsAvailable = true, DailyOrderCount = 11, CategoryId = categories[4].Id });

//        await context.MenuItems.AddRangeAsync(menuItems);
//        await context.SaveChangesAsync();
//        Console.WriteLine("Seeded MenuItems.");

//        // --- 3. Seed Tables (10 tables) ---
//        var tables = new List<Table>
//        {
//            // Id and CreatedDate will be handled by DbContext configuration and UpdateAuditFields
//            new Table { Number = 1, Capacity = 2, IsAvailable = true },
//            new Table { Number = 2, Capacity = 4, IsAvailable = true },
//            new Table { Number = 3, Capacity = 4, IsAvailable = false }, // Reserved
//            new Table { Number = 4, Capacity = 6, IsAvailable = true },
//            new Table { Number = 5, Capacity = 8, IsAvailable = true },
//            new Table { Number = 6, Capacity = 2, IsAvailable = true },
//            new Table { Number = 7, Capacity = 4, IsAvailable = true },
//            new Table { Number = 8, Capacity = 4, IsAvailable = true },
//            new Table { Number = 9, Capacity = 6, IsAvailable = true },
//            new Table { Number = 10, Capacity = 2, IsAvailable = true }
//        };
//        await context.Tables.AddRangeAsync(tables);
//        await context.SaveChangesAsync(); // Save to get the generated IDs for tables

//        // Retrieve the generated IDs after saving changes.
//        tables = await context.Tables.OrderBy(t => t.Number).ToListAsync(); // Order to ensure consistent indexing
//        Console.WriteLine("Seeded Tables.");
//        #endregion
//        //---------------------------------------------------------------------------------------------------------------------------------------

//        // --- 4. Seed Customers (dummy GUIDs) ---
//        // In a real application, these would come from your Identity system (AppUser table).
//        // For seeding purposes here, we'll generate dummy GUIDs.
//        //var customerIds = new List<Guid>
//        //{
//        //    Guid.NewGuid(), // Customer 1
//        //    Guid.NewGuid(), // Customer 2
//        //    Guid.NewGuid(), // Customer 3
//        //    Guid.NewGuid(), // Customer 4
//        //    Guid.NewGuid()  // Customer 5
//        //};
//        //Console.WriteLine("Generated Customer GUIDs (not stored as entities in this seed).");


//        //// --- 5. Seed Orders (~15 orders) ---
//        //var orders = new List<Order>();

//        //// Order 1: DineIn, Happy Hour (3-5 PM), total < $100
//        //var order1 = new Order
//        //{
//        //    CustomerId = customerIds[0],
//        //    Type = OrderType.DineIn,
//        //    Status = OrderStatus.Completed,
//        //    SpecialInstructions = "No onions",
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(15).AddMinutes(30), // 3:30 PM
//        //    ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(15).AddMinutes(45) // 15 mins later
//        //};
//        //orders.Add(order1);

//        //// Order 2: Delivery, over $100 (qualifies for 10% bulk discount)
//        //var order2 = new Order
//        //{
//        //    CustomerId = customerIds[1],
//        //    Type = OrderType.Delivery,
//        //    Status = OrderStatus.OutForDelivery,
//        //    DeliveryAddress = "123 Main St, Anytown, USA",
//        //    SpecialInstructions = "Ring doorbell once.",
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(18).AddMinutes(0),
//        //    EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(18).AddMinutes(20) // 20 mins
//        //};
//        //orders.Add(order2);

//        //// Order 3: Takeout, Pending
//        //var order3 = new Order
//        //{
//        //    CustomerId = customerIds[2],
//        //    Type = OrderType.Takeout,
//        //    Status = OrderStatus.Pending,
//        //    SpecialInstructions = "",
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(11).AddMinutes(45),
//        //    PickupTime = DateTime.UtcNow.Date.AddHours(12).AddMinutes(15) // 30 mins
//        //};
//        //orders.Add(order3);

//        //// Order 4: DineIn, Preparing
//        //var order4 = new Order
//        //{
//        //    CustomerId = customerIds[0],
//        //    Type = OrderType.DineIn,
//        //    Status = OrderStatus.Preparing,
//        //    SpecialInstructions = "Extra spicy",
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(19).AddMinutes(0),
//        //    EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(19).AddMinutes(25) // 25 mins
//        //};
//        //orders.Add(order4);

//        //// Order 5: Delivery, Completed
//        //var order5 = new Order
//        //{
//        //    CustomerId = customerIds[3],
//        //    Type = OrderType.Delivery,
//        //    Status = OrderStatus.Completed,
//        //    DeliveryAddress = "456 Oak Ave, Othercity, USA",
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(13).AddMinutes(10),
//        //    EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(13).AddMinutes(35),
//        //    ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(13).AddMinutes(30)
//        //};
//        //orders.Add(order5);

//        //// Order 6: Takeout, ReadyForPickup
//        //var order6 = new Order
//        //{
//        //    CustomerId = customerIds[4],
//        //    Type = OrderType.Takeout,
//        //    Status = OrderStatus.ReadyForPickup,
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(9).AddMinutes(0),
//        //    PickupTime = DateTime.UtcNow.Date.AddHours(9).AddMinutes(20)
//        //};
//        //orders.Add(order6);

//        //// Order 7: DineIn, Pending (Breakfast)
//        //var order7 = new Order
//        //{
//        //    CustomerId = customerIds[0],
//        //    Type = OrderType.DineIn,
//        //    Status = OrderStatus.Pending,
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(8).AddMinutes(10)
//        //};
//        //orders.Add(order7);

//        //// Order 8: Delivery, Pending, Happy Hour (3-5 PM), over $100 (bulk discount applies AFTER Happy Hour discount)
//        //var order8 = new Order
//        //{
//        //    CustomerId = customerIds[1],
//        //    Type = OrderType.Delivery,
//        //    Status = OrderStatus.Pending,
//        //    DeliveryAddress = "789 Pine Rd, Smallville, USA",
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(16).AddMinutes(0), // 4:00 PM
//        //    EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(16).AddMinutes(25)
//        //};
//        //orders.Add(order8);

//        //// Order 9: DineIn, Completed, small order
//        //var order9 = new Order
//        //{
//        //    CustomerId = customerIds[2],
//        //    Type = OrderType.DineIn,
//        //    Status = OrderStatus.Completed,
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(20).AddMinutes(15),
//        //    ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(20).AddMinutes(30)
//        //};
//        //orders.Add(order9);

//        //// Order 10: Takeout, Cancelled
//        //var order10 = new Order
//        //{
//        //    CustomerId = customerIds[3],
//        //    Type = OrderType.Takeout,
//        //    Status = OrderStatus.Cancelled,
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(17).AddMinutes(0)
//        //};
//        //orders.Add(order10);

//        //// Add more diverse orders to reach ~20
//        //// Order 11: DineIn, Multiple Items
//        //var order11 = new Order
//        //{
//        //    CustomerId = customerIds[0],
//        //    Type = OrderType.DineIn,
//        //    Status = OrderStatus.Completed,
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(14).AddMinutes(0),
//        //    ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(14).AddMinutes(20)
//        //};
//        //orders.Add(order11);

//        //// Order 12: Delivery, Preparing
//        //var order12 = new Order
//        //{
//        //    CustomerId = customerIds[1],
//        //    Type = OrderType.Delivery,
//        //    Status = OrderStatus.Preparing,
//        //    DeliveryAddress = "101 River St, Lakeside, USA",
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(19).AddMinutes(30),
//        //    EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(19).AddMinutes(55)
//        //};
//        //orders.Add(order12);

//        //// Order 13: Takeout, Completed
//        //var order13 = new Order
//        //{
//        //    CustomerId = customerIds[2],
//        //    Type = OrderType.Takeout,
//        //    Status = OrderStatus.Completed,
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(12).AddMinutes(0),
//        //    PickupTime = DateTime.UtcNow.Date.AddHours(12).AddMinutes(30)
//        //};
//        //orders.Add(order13);

//        //// Order 14: DineIn, Pending (Large group order)
//        //var order14 = new Order
//        //{
//        //    CustomerId = customerIds[3],
//        //    Type = OrderType.DineIn,
//        //    Status = OrderStatus.Pending,
//        //    SpecialInstructions = "Separate checks please",
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(18).AddMinutes(45),
//        //};
//        //orders.Add(order14);

//        //// Order 15: Delivery, Completed, Happy Hour
//        //var order15 = new Order
//        //{
//        //    CustomerId = customerIds[4],
//        //    Type = OrderType.Delivery,
//        //    Status = OrderStatus.Completed,
//        //    DeliveryAddress = "777 Valley St, Hilltop, USA",
//        //    CreatedDate = DateTime.UtcNow.Date.AddHours(16).AddMinutes(40),
//        //    EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(17).AddMinutes(0),
//        //    ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(16).AddMinutes(58)
//        //};
//        //orders.Add(order15);


//        //// --- IMPORTANT: Save Orders first to populate their IDs ---
//        //await context.Orders.AddRangeAsync(orders);
//        //await context.SaveChangesAsync();
//        //Console.WriteLine("Partially seeded Orders to get generated IDs.");

//        //// Re-fetch orders to ensure IDs are loaded, or if using navigation properties, they'd be updated
//        //// For simplicity and to ensure correct IDs, we'll just refer to the 'orders' list which
//        //// now has its IDs populated after SaveChangesAsync().

//        //var orderItems = new List<OrderItem>();
//        //// Helper to add an order item and calculate subtotal
//        //decimal AddOrderItem(Order currentOrder, MenuItem menuItem, int quantity)
//        //{
//        //    var item = new OrderItem
//        //    {
//        //        OrderId = currentOrder.Id, // Use the generated OrderId
//        //        MenuItemId = menuItem.Id,
//        //        Quantity = quantity,
//        //        UnitPrice = menuItem.Price ?? 0, // Handle nullable Price
//        //        SpecialInstructions = "",
//        //        // CreatedDate will be handled by DbContext's UpdateAuditFields
//        //    };
//        //    orderItems.Add(item);
//        //    return item.Quantity * item.UnitPrice;
//        //}

//        //// Now, add OrderItems using the IDs from the already saved Orders
//        //// Note: The `orders` list now contains the `Id` values populated by the database
//        //// after the previous `context.SaveChangesAsync()`.

//        //// Populate Order 1's items
//        //decimal subtotal1 = 0;
//        //subtotal1 += AddOrderItem(order1, menuItems.First(m => m.Name == "Garlic Bread"), 1);
//        //subtotal1 += AddOrderItem(order1, menuItems.First(m => m.Name == "Fettuccine Alfredo"), 1);
//        //subtotal1 += AddOrderItem(order1, menuItems.First(m => m.Name == "Coca-Cola"), 2);
//        //order1.TotalAmount = subtotal1 * 0.80M; // 20% Happy Hour discount

//        //// Populate Order 2's items
//        //decimal subtotal2 = 0;
//        //subtotal2 += AddOrderItem(order2, menuItems.First(m => m.Name == "Ribeye Steak"), 2);
//        //subtotal2 += AddOrderItem(order2, menuItems.First(m => m.Name == "Grilled Salmon"), 1);
//        //subtotal2 += AddOrderItem(order2, menuItems.First(m => m.Name == "Chocolate Lava Cake"), 3);
//        //subtotal2 += AddOrderItem(order2, menuItems.First(m => m.Name == "Fresh Orange Juice"), 4);
//        //order2.TotalAmount = subtotal2;
//        //if (order2.TotalAmount > 100) order2.TotalAmount *= 0.90M; // 10% bulk discount

//        //// Populate Order 3's items
//        //decimal subtotal3 = 0;
//        //subtotal3 += AddOrderItem(order3, menuItems.First(m => m.Name == "Buffalo Wings"), 1);
//        //subtotal3 += AddOrderItem(order3, menuItems.First(m => m.Name == "Espresso"), 1);
//        //order3.TotalAmount = subtotal3;

//        //// Populate Order 4's items
//        //decimal subtotal4 = 0;
//        //subtotal4 += AddOrderItem(order4, menuItems.First(m => m.Name == "Chicken Tikka Masala"), 2);
//        //subtotal4 += AddOrderItem(order4, menuItems.First(m => m.Name == "Sparkling Water"), 2);
//        //order4.TotalAmount = subtotal4;

//        //// Populate Order 5's items
//        //decimal subtotal5 = 0;
//        //subtotal5 += AddOrderItem(order5, menuItems.First(m => m.Name == "Vegetable Lasagna"), 1);
//        //subtotal5 += AddOrderItem(order5, menuItems.First(m => m.Name == "New York Cheesecake"), 1);
//        //order5.TotalAmount = subtotal5;

//        //// Populate Order 6's items
//        //decimal subtotal6 = 0;
//        //subtotal6 += AddOrderItem(order6, menuItems.First(m => m.Name == "Fluffy Pancakes"), 1);
//        //subtotal6 += AddOrderItem(order6, menuItems.First(m => m.Name == "Espresso"), 1);
//        //order6.TotalAmount = subtotal6;

//        //// Populate Order 7's items
//        //decimal subtotal7 = 0;
//        //subtotal7 += AddOrderItem(order7, menuItems.First(m => m.Name == "Classic Omellette"), 1);
//        //subtotal7 += AddOrderItem(order7, menuItems.First(m => m.Name == "Fresh Orange Juice"), 1);
//        //order7.TotalAmount = subtotal7;

//        //// Populate Order 8's items
//        //decimal subtotal8 = 0;
//        //subtotal8 += AddOrderItem(order8, menuItems.First(m => m.Name == "Ribeye Steak"), 3);
//        //subtotal8 += AddOrderItem(order8, menuItems.First(m => m.Name == "Spring Rolls"), 4);
//        //subtotal8 += AddOrderItem(order8, menuItems.First(m => m.Name == "Tiramisu"), 2);
//        //order8.TotalAmount = subtotal8;
//        //order8.TotalAmount *= 0.80M; // Happy Hour 20% discount
//        //if (order8.TotalAmount > 100) order8.TotalAmount *= 0.90M; // Bulk 10% discount

//        //// Populate Order 9's items
//        //decimal subtotal9 = 0;
//        //subtotal9 += AddOrderItem(order9, menuItems.First(m => m.Name == "Assorted Ice Cream"), 1);
//        //subtotal9 += AddOrderItem(order9, menuItems.First(m => m.Name == "Green Tea"), 1);
//        //order9.TotalAmount = subtotal9;

//        //// Populate Order 10's items
//        //decimal subtotal10 = 0;
//        //subtotal10 += AddOrderItem(order10, menuItems.First(m => m.Name == "Crispy Calamari"), 1);
//        //order10.TotalAmount = subtotal10;

//        //// Populate Order 11's items
//        //decimal subtotal11 = 0;
//        //subtotal11 += AddOrderItem(order11, menuItems.First(m => m.Name == "Loaded Nachos"), 1);
//        //subtotal11 += AddOrderItem(order11, menuItems.First(m => m.Name == "Chicken Tikka Masala"), 1);
//        //subtotal11 += AddOrderItem(order11, menuItems.First(m => m.Name == "Berry Smoothie"), 1);
//        //order11.TotalAmount = subtotal11;

//        //// Populate Order 12's items
//        //decimal subtotal12 = 0;
//        //subtotal12 += AddOrderItem(order12, menuItems.First(m => m.Name == "Fish and Chips"), 2);
//        //subtotal12 += AddOrderItem(order12, menuItems.First(m => m.Name == "Assorted Ice Cream"), 1);
//        //order12.TotalAmount = subtotal12;

//        //// Populate Order 13's items
//        //decimal subtotal13 = 0;
//        //subtotal13 += AddOrderItem(order13, menuItems.First(m => m.Name == "Breakfast Burrito"), 1);
//        //subtotal13 += AddOrderItem(order13, menuItems.First(m => m.Name == "Iced Latte"), 1);
//        //order13.TotalAmount = subtotal13;

//        //// Populate Order 14's items
//        //decimal subtotal14 = 0;
//        //subtotal14 += AddOrderItem(order14, menuItems.First(m => m.Name == "Ribeye Steak"), 4);
//        //subtotal14 += AddOrderItem(order14, menuItems.First(m => m.Name == "Fettuccine Alfredo"), 2);
//        //subtotal14 += AddOrderItem(order14, menuItems.First(m => m.Name == "Crispy Calamari"), 2);
//        //subtotal14 += AddOrderItem(order14, menuItems.First(m => m.Name == "Coca-Cola"), 4);
//        //order14.TotalAmount = subtotal14;
//        //if (order14.TotalAmount > 100) order14.TotalAmount *= 0.90M; // Bulk discount

//        //// Populate Order 15's items
//        //decimal subtotal15 = 0;
//        //subtotal15 += AddOrderItem(order15, menuItems.First(m => m.Name == "Bruschetta"), 1);
//        //subtotal15 += AddOrderItem(order15, menuItems.First(m => m.Name == "Vegetable Lasagna"), 1);
//        //subtotal15 += AddOrderItem(order15, menuItems.First(m => m.Name == "Crème brûlée"), 1);
//        //order15.TotalAmount = subtotal15 * 0.80M; // Happy Hour discount


//        //// Update the TotalAmount for each order that was calculated
//        //context.Orders.UpdateRange(orders); // Mark orders as modified to save TotalAmount updates
//        //await context.OrderItems.AddRangeAsync(orderItems); // Add all order items
//        //await context.SaveChangesAsync();
//        //Console.WriteLine("Seeded OrderItems and updated Order TotalAmounts.");


//        //// --- 6. Seed Sales Transactions (for completed orders) ---
//        //var salesTransactions = new List<SalesTransaction>();

//        //// Only add transactions for completed orders
//        //foreach (var order in orders.Where(o => o.Status == OrderStatus.Completed))
//        //{
//        //    salesTransactions.Add(new SalesTransaction
//        //    {
//        //        OrderId = order.Id,
//        //        Amount = order.TotalAmount,
//        //        PaymentMethod = (PaymentMethod)random.Next(0, 3), // Randomly pick Cash, CreditCard, or OnlinePayment
//        //        PaymentReference = $"TRANS-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
//        //        // CreatedDate will be handled by DbContext's UpdateAuditFields
//        //    });
//        //}

//        //await context.SalesTransactions.AddRangeAsync(salesTransactions);
//        //await context.SaveChangesAsync();
//        //Console.WriteLine("Seeded SalesTransactions.");


//        //// --- 7. Seed Table Reservations ---
//        //var tableReservations = new List<TableReservation>
//        //{
//        //    // TableId and CreatedDate will be handled by DbContext configuration and UpdateAuditFields
//        //    new TableReservation { TableId = tables.First(t => t.Number == 3).Id, CustomerId = customerIds[0], Date = DateTime.Today.AddDays(1), Time = new TimeSpan(19, 0, 0) }, // Tomorrow 7 PM
//        //    new TableReservation { TableId = tables.First(t => t.Number == 2).Id, CustomerId = customerIds[1], Date = DateTime.Today.AddDays(2), Time = new TimeSpan(12, 30, 0) }, // Day after tomorrow 12:30 PM
//        //    new TableReservation { TableId = tables.First(t => t.Number == 5).Id, CustomerId = customerIds[4], Date = DateTime.Today.Date, Time = new TimeSpan(21, 0, 0) }, // Tonight 9 PM
//        //    new TableReservation { TableId = tables.First(t => t.Number == 4).Id, CustomerId = customerIds[2], Date = DateTime.Today.AddDays(3), Time = new TimeSpan(18, 0, 0) } // 3 days from now 6 PM
//        //};
//        //await context.TableReservations.AddRangeAsync(tableReservations);
//        //await context.SaveChangesAsync();
//        //Console.WriteLine("Seeded TableReservations.");

//        Console.WriteLine("Database seeding complete!");
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Required for DbContext and Async operations
using RestaurantManagementSystem.Domain.Models; // Your domain models
using RestaurantManagementSystem.Domain.Enums; // Your enums
using RestaurantManagementSystem.Infrastructure.Data; // Namespace for your AppDbContext
using Microsoft.AspNetCore.Identity; // For UserManager and RoleManager

public static class SeedData
{
    /// <summary>
    /// Initializes the database with sample data, including Identity users and roles, if it's empty.
    /// This method is typically called during application startup (e.g., in Program.cs).
    /// </summary>
    /// <param name="context">The AppDbContext instance to seed data into.</param>
    /// <param name="userManager">The UserManager for AppUser.</param>
    /// <param name="roleManager">The RoleManager for IdentityRole<Guid>.</param>
 
    public static async Task Initialize(
        AppDbContext context,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        // Ensure the database is created if it doesn't exist.
        // In a production environment, you might use context.Database.Migrate() instead.
        //await context.Database.EnsureCreatedAsync();

        // Check if any users already exist. If so, the database has likely been seeded.
        if (userManager.Users.Any())
        {
            Console.WriteLine("Database already contains user data. Skipping seeding.");
            // If users exist, it's likely other core data also exists.
            // You might add a check for categories as well if user seeding isn't the only indicator.
            return;
        }

        Console.WriteLine("Seeding database with initial data...");
        var random = new Random();

        // --- 0. Seed Roles ---
        Console.WriteLine("Seeding Roles...");
        string[] roleNames = { "Admin", "Waiter", "Chef", "Customer" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                Console.WriteLine($"  Created Role: {roleName}");
            }
        }
        Console.WriteLine("Roles seeded.");

        // --- 1. Seed Users (AppUser) ---
        Console.WriteLine("Seeding Users...");
        var seededCustomers = new List<AppUser>(); // To store actual customer AppUser objects

        // Admin User
        var adminUser = new AppUser
        {
            UserName = "admin@restaurant.com",
            Email = "admin@restaurant.com",
            FirstName = "Super",
            LastName = "Admin",
            Address = "100 Admin Way, Qena",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            IsActive = true,
            LastLoginDate = DateTime.UtcNow // Current time in Qena, Egypt
        };
        // CreatedDate and Id will be handled by AppDbContext and Identity framework
        if (await userManager.FindByEmailAsync(adminUser.Email) == null)
        {
            var result = await userManager.CreateAsync(adminUser, "AdminP@ss1!"); // Strong password
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine($"  Created Admin user: {adminUser.Email}");
            }
            else
            {
                Console.WriteLine($"  Error creating Admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        // Waiter Users
        var waiter1 = new AppUser
        {
            UserName = "waiter1@restaurant.com",
            Email = "waiter1@restaurant.com",
            FirstName = "Alice",
            LastName = "Smith",
            Address = "201 Elm St, Qena",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            IsActive = true,
            Salary = 3000.00M,
            HiringDate = new DateTime(2023, 1, 15),
            LastLoginDate = DateTime.UtcNow // Current time in Qena, Egypt
        };
        if (await userManager.FindByEmailAsync(waiter1.Email) == null)
        {
            var result = await userManager.CreateAsync(waiter1, "WaiterP@ss1!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(waiter1, "Waiter");
                Console.WriteLine($"  Created Waiter user: {waiter1.Email}");
            }
        }

        var waiter2 = new AppUser
        {
            UserName = "waiter2@restaurant.com",
            Email = "waiter2@restaurant.com",
            FirstName = "Bob",
            LastName = "Johnson",
            Address = "202 Oak Ave, Qena",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            IsActive = true,
            Salary = 3100.00M,
            HiringDate = new DateTime(2024, 6, 1),
            LastLoginDate = DateTime.UtcNow // Current time in Qena, Egypt
        };
        if (await userManager.FindByEmailAsync(waiter2.Email) == null)
        {
            var result = await userManager.CreateAsync(waiter2, "WaiterP@ss2!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(waiter2, "Waiter");
                Console.WriteLine($"  Created Waiter user: {waiter2.Email}");
            }
        }

        // Chef User
        var chef1 = new AppUser
        {
            UserName = "chef1@restaurant.com",
            Email = "chef1@restaurant.com",
            FirstName = "Charlie",
            LastName = "Brown",
            Address = "301 Pine Ln, Qena",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            IsActive = true,
            Salary = 4500.00M,
            HiringDate = new DateTime(2022, 9, 1),
            LastLoginDate = DateTime.UtcNow // Current time in Qena, Egypt
        };
        if (await userManager.FindByEmailAsync(chef1.Email) == null)
        {
            var result = await userManager.CreateAsync(chef1, "ChefP@ss1!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(chef1, "Chef");
                Console.WriteLine($"  Created Chef user: {chef1.Email}");
            }
        }

        // Customer Users (5 customers)
        var customerData = new (string FirstName, string LastName, string Email, string Address)[]
        {
            ("David", "Miller", "customer1@example.com", "701 Cedar Blvd, Qena"),
            ("Eve", "Davis", "customer2@example.com", "702 Birch St, Qena"),
            ("Frank", "Wilson", "customer3@example.com", "703 Willow Ln, Qena"),
            ("Grace", "Taylor", "customer4@example.com", "704 Maple Dr, Qena"),
            ("Henry", "Moore", "customer5@example.com", "705 Spruce Ct, Qena")
        };

        foreach (var data in customerData)
        {
            var customer = new AppUser
            {
                UserName = data.Email,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Address = data.Address,
                EmailConfirmed = true,
                IsActive = true,
                LastLoginDate = DateTime.UtcNow // Current time in Qena, Egypt
            };

            if (await userManager.FindByEmailAsync(customer.Email) == null)
            {
                var result = await userManager.CreateAsync(customer, "CustomerP@ss1!"); // All customers share a simple password for seeding
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customer, "Customer");
                    seededCustomers.Add(customer);
                    Console.WriteLine($"  Created Customer user: {customer.Email}");
                }
                else
                {
                    Console.WriteLine($"  Error creating Customer user {customer.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                // If user already exists, retrieve it to ensure the 'seededCustomers' list has actual AppUser objects with their IDs
                seededCustomers.Add(await userManager.FindByEmailAsync(customer.Email));
                Console.WriteLine($"  Customer user {customer.Email} already exists, added to list.");
            }
        }

        // Update the customerIds list to use the actual GUIDs of the seeded customers
        var customerIds = seededCustomers.Select(c => c.Id).ToList();
        Console.WriteLine("Users seeded successfully.");

        // --- Rest of your existing seeding logic for Categories, MenuItems, Tables, Orders, SalesTransactions, TableReservations ---
        // Ensure that the following blocks are present and use 'context' as before.
        // The important part is that 'customerIds' list is now populated with actual AppUser GUIDs.

        // --- 2. Seed Categories ---
        var categories = new List<Category>
        {
            new Category { Name = "Appetizers", Description = "Delicious starters to begin your meal." },
            new Category { Name = "Main Courses", Description = "Hearty and fulfilling main dishes for a complete experience." },
            new Category { Name = "Desserts", Description = "Sweet treats to end your dining experience." },
            new Category { Name = "Beverages", Description = "Refreshing drinks to complement your meal." },
            new Category { Name = "Breakfast", Description = "Perfect morning delights to start your day." }
        };
        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
        categories = await context.Categories.OrderBy(c => c.Name).ToListAsync();
        Console.WriteLine("Seeded Categories.");

        // --- 3. Seed Menu Items ---
        var menuItems = new List<MenuItem>();
        menuItems.Add(new MenuItem { Name = "Spring Rolls", Description = "Crispy vegetable spring rolls with sweet chili dip.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Spring+Rolls", Price = 7.50M, IsAvailable = true, DailyOrderCount = 10, CategoryId = categories.First(c => c.Name == "Appetizers").Id });
        menuItems.Add(new MenuItem { Name = "Garlic Bread", Description = "Toasted baguette slices with fragrant garlic butter and herbs.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Garlic+Bread", Price = 5.00M, IsAvailable = true, DailyOrderCount = 15, CategoryId = categories.First(c => c.Name == "Appetizers").Id });
        menuItems.Add(new MenuItem { Name = "Crispy Calamari", Description = "Golden fried calamari rings served with zesty tartar sauce.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Calamari", Price = 12.00M, IsAvailable = true, DailyOrderCount = 5, CategoryId = categories.First(c => c.Name == "Appetizers").Id });
        menuItems.Add(new MenuItem { Name = "Buffalo Wings", Description = "Spicy chicken wings tossed in buffalo sauce, served with blue cheese dip.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Buffalo+Wings", Price = 10.00M, IsAvailable = true, DailyOrderCount = 20, CategoryId = categories.First(c => c.Name == "Appetizers").Id });
        menuItems.Add(new MenuItem { Name = "Mozzarella Sticks", Description = "Melted mozzarella cheese sticks, breaded and fried to perfection. (Currently Unavailable)", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Mozzarella+Sticks", Price = 8.00M, IsAvailable = false, DailyOrderCount = 0, CategoryId = categories.First(c => c.Name == "Appetizers").Id });
        menuItems.Add(new MenuItem { Name = "Bruschetta", Description = "Toasted bread topped with fresh diced tomatoes, garlic, and basil.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Bruschetta", Price = 6.50M, IsAvailable = true, DailyOrderCount = 12, CategoryId = categories.First(c => c.Name == "Appetizers").Id });
        menuItems.Add(new MenuItem { Name = "Loaded Nachos", Description = "Crispy tortilla chips smothered in cheese, jalapenos, and salsa.", ImageURL = "https://placehold.co/200x150/FF6347/FFFFFF?text=Loaded+Nachos", Price = 13.50M, IsAvailable = true, DailyOrderCount = 9, CategoryId = categories.First(c => c.Name == "Appetizers").Id });

        menuItems.Add(new MenuItem { Name = "Grilled Salmon", Description = "Pan-seared salmon fillet served with asparagus and lemon-dill sauce.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Grilled+Salmon", Price = 22.00M, IsAvailable = true, DailyOrderCount = 8, CategoryId = categories.First(c => c.Name == "Main Courses").Id });
        menuItems.Add(new MenuItem { Name = "Ribeye Steak", Description = "Juicy ribeye steak cooked to your preference, with roasted potatoes.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Ribeye+Steak", Price = 28.00M, IsAvailable = true, DailyOrderCount = 10, CategoryId = categories.First(c => c.Name == "Main Courses").Id });
        menuItems.Add(new MenuItem { Name = "Fettuccine Alfredo", Description = "Creamy fettuccine pasta with Parmesan cheese and a rich Alfredo sauce.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Fettuccine+Alfredo", Price = 18.00M, IsAvailable = true, DailyOrderCount = 12, CategoryId = categories.First(c => c.Name == "Main Courses").Id });
        menuItems.Add(new MenuItem { Name = "Chicken Tikka Masala", Description = "Tender chicken pieces in a rich, creamy tomato sauce, served with naan.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Chicken+Tikka", Price = 19.50M, IsAvailable = true, DailyOrderCount = 7, CategoryId = categories.First(c => c.Name == "Main Courses").Id });
        menuItems.Add(new MenuItem { Name = "Vegetable Lasagna", Description = "Layers of pasta, fresh vegetables, ricotta, and mozzarella cheese.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Veg+Lasagna", Price = 16.00M, IsAvailable = true, DailyOrderCount = 6, CategoryId = categories.First(c => c.Name == "Main Courses").Id });
        menuItems.Add(new MenuItem { Name = "Classic Cheeseburger", Description = "Grilled beef patty with cheddar cheese, lettuce, tomato, and pickles.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Cheeseburger", Price = 15.00M, IsAvailable = false, DailyOrderCount = 0, CategoryId = categories.First(c => c.Name == "Main Courses").Id });
        menuItems.Add(new MenuItem { Name = "Fish and Chips", Description = "Crispy battered cod served with thick-cut fries and malt vinegar.", ImageURL = "https://placehold.co/200x150/4682B4/FFFFFF?text=Fish+and+Chips", Price = 17.00M, IsAvailable = true, DailyOrderCount = 9, CategoryId = categories.First(c => c.Name == "Main Courses").Id });

        menuItems.Add(new MenuItem { Name = "Chocolate Lava Cake", Description = "Warm chocolate cake with a gooey molten center, served with vanilla ice cream.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Lava+Cake", Price = 9.00M, IsAvailable = true, DailyOrderCount = 12, CategoryId = categories.First(c => c.Name == "Desserts").Id });
        menuItems.Add(new MenuItem { Name = "New York Cheesecake", Description = "Creamy, classic New York style cheesecake with berry compote.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Cheesecake", Price = 8.50M, IsAvailable = true, DailyOrderCount = 10, CategoryId = categories.First(c => c.Name == "Desserts").Id });
        menuItems.Add(new MenuItem { Name = "Tiramisu", Description = "Traditional Italian coffee-flavored dessert with ladyfingers and mascarpone.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Tiramisu", Price = 9.50M, IsAvailable = true, DailyOrderCount = 7, CategoryId = categories.First(c => c.Name == "Desserts").Id });
        menuItems.Add(new MenuItem { Name = "Seasonal Fruit Platter", Description = "A refreshing selection of fresh seasonal fruits.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Fruit+Platter", Price = 7.00M, IsAvailable = true, DailyOrderCount = 15, CategoryId = categories.First(c => c.Name == "Desserts").Id });
        menuItems.Add(new MenuItem { Name = "Assorted Ice Cream", Description = "Two scoops of your choice: vanilla, chocolate, or strawberry.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Ice+Cream", Price = 6.00M, IsAvailable = true, DailyOrderCount = 20, CategoryId = categories.First(c => c.Name == "Desserts").Id });
        menuItems.Add(new MenuItem { Name = "Crème brûlée", Description = "Rich vanilla bean custard base topped with a layer of hardened caramelized sugar.", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Creme+Brulee", Price = 10.00M, IsAvailable = true, DailyOrderCount = 5, CategoryId = categories.First(c => c.Name == "Desserts").Id });
        menuItems.Add(new MenuItem { Name = "Apple Pie", Description = "Warm apple pie with a scoop of vanilla ice cream. (Currently Unavailable)", ImageURL = "https://placehold.co/200x150/DAA520/FFFFFF?text=Apple+Pie", Price = 8.00M, IsAvailable = false, DailyOrderCount = 0, CategoryId = categories.First(c => c.Name == "Desserts").Id });

        menuItems.Add(new MenuItem { Name = "Coca-Cola", Description = "Classic refreshing Coca-Cola.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Coca-Cola", Price = 3.00M, IsAvailable = true, DailyOrderCount = 30, CategoryId = categories.First(c => c.Name == "Beverages").Id });
        menuItems.Add(new MenuItem { Name = "Fresh Orange Juice", Description = "Freshly squeezed orange juice, rich in Vitamin C.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Orange+Juice", Price = 4.50M, IsAvailable = true, DailyOrderCount = 25, CategoryId = categories.First(c => c.Name == "Beverages").Id });
        menuItems.Add(new MenuItem { Name = "Espresso", Description = "Strong, concentrated coffee shot.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Espresso", Price = 4.00M, IsAvailable = true, DailyOrderCount = 40, CategoryId = categories.First(c => c.Name == "Beverages").Id });
        menuItems.Add(new MenuItem { Name = "Green Tea", Description = "Calming green tea, served hot or iced.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Green+Tea", Price = 3.50M, IsAvailable = true, DailyOrderCount = 28, CategoryId = categories.First(c => c.Name == "Beverages").Id });
        menuItems.Add(new MenuItem { Name = "Sparkling Water", Description = "Crisp, bubbly sparkling water.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Sparkling+Water", Price = 2.50M, IsAvailable = true, DailyOrderCount = 50, CategoryId = categories.First(c => c.Name == "Beverages").Id });
        menuItems.Add(new MenuItem { Name = "Berry Smoothie", Description = "Blended fresh berries with yogurt and a hint of honey.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Berry+Smoothie", Price = 6.00M, IsAvailable = true, DailyOrderCount = 18, CategoryId = categories.First(c => c.Name == "Beverages").Id });
        menuItems.Add(new MenuItem { Name = "Iced Latte", Description = "Chilled espresso with milk and ice.", ImageURL = "https://placehold.co/200x150/2E8B57/FFFFFF?text=Iced+Latte", Price = 5.50M, IsAvailable = true, DailyOrderCount = 15, CategoryId = categories.First(c => c.Name == "Beverages").Id });

        menuItems.Add(new MenuItem { Name = "Fluffy Pancakes", Description = "Stack of golden pancakes served with maple syrup and butter.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Pancakes", Price = 11.00M, IsAvailable = true, DailyOrderCount = 10, CategoryId = categories.First(c => c.Name == "Breakfast").Id });
        menuItems.Add(new MenuItem { Name = "Classic Omellette", Description = "Three-egg omelette with your choice of cheese, ham, or vegetables.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Omellette", Price = 13.00M, IsAvailable = true, DailyOrderCount = 8, CategoryId = categories.First(c => c.Name == "Breakfast").Id });
        menuItems.Add(new MenuItem { Name = "Breakfast Burrito", Description = "Warm tortilla filled with scrambled eggs, sausage, cheese, and salsa.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Breakfast+Burrito", Price = 12.50M, IsAvailable = true, DailyOrderCount = 7, CategoryId = categories.First(c => c.Name == "Breakfast").Id });
        menuItems.Add(new MenuItem { Name = "Belgian Waffles", Description = "Crispy Belgian waffles topped with fresh berries and whipped cream.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Waffles", Price = 10.50M, IsAvailable = true, DailyOrderCount = 9, CategoryId = categories.First(c => c.Name == "Breakfast").Id });
        menuItems.Add(new MenuItem { Name = "Avocado Toast", Description = "Toasted sourdough with smashed avocado, poached egg, and everything bagel seasoning.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Avocado+Toast", Price = 14.00M, IsAvailable = true, DailyOrderCount = 6, CategoryId = categories.First(c => c.Name == "Breakfast").Id });
        menuItems.Add(new MenuItem { Name = "French Toast", Description = "Thick-cut bread dipped in cinnamon egg batter, grilled, and dusted with powdered sugar.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=French+Toast", Price = 11.50M, IsAvailable = true, DailyOrderCount = 5, CategoryId = categories.First(c => c.Name == "Breakfast").Id });
        menuItems.Add(new MenuItem { Name = "Yogurt Parfait", Description = "Layers of Greek yogurt, granola, and fresh mixed berries.", ImageURL = "https://placehold.co/200x150/9932CC/FFFFFF?text=Yogurt+Parfait", Price = 9.00M, IsAvailable = true, DailyOrderCount = 11, CategoryId = categories.First(c => c.Name == "Breakfast").Id });

        await context.MenuItems.AddRangeAsync(menuItems);
        await context.SaveChangesAsync();
        Console.WriteLine("Seeded MenuItems.");

        // --- 4. Seed Tables ---
        var tables = new List<Table>
        {
            new Table { Number = 1, Capacity = 2, IsAvailable = true },
            new Table { Number = 2, Capacity = 4, IsAvailable = true },
            new Table { Number = 3, Capacity = 4, IsAvailable = false },
            new Table { Number = 4, Capacity = 6, IsAvailable = true },
            new Table { Number = 5, Capacity = 8, IsAvailable = true },
            new Table { Number = 6, Capacity = 2, IsAvailable = true },
            new Table { Number = 7, Capacity = 4, IsAvailable = true },
            new Table { Number = 8, Capacity = 4, IsAvailable = true },
            new Table { Number = 9, Capacity = 6, IsAvailable = true },
            new Table { Number = 10, Capacity = 2, IsAvailable = true }
        };
        await context.Tables.AddRangeAsync(tables);
        await context.SaveChangesAsync();
        tables = await context.Tables.OrderBy(t => t.Number).ToListAsync();
        Console.WriteLine("Seeded Tables.");


        // --- 5. Seed Orders (~15 orders) ---
        var orders = new List<Order>();

        // Order 1: DineIn, Happy Hour (3-5 PM), total < $100
        var order1 = new Order
        {
            CustomerId = customerIds[0],
            Type = OrderType.DineIn,
            Status = OrderStatus.Completed,
            SpecialInstructions = "No onions",
            CreatedDate = DateTime.UtcNow.Date.AddHours(15).AddMinutes(30), // 3:30 PM
            ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(15).AddMinutes(45) // 15 mins later
        };
        orders.Add(order1);

        // Order 2: Delivery, over $100 (qualifies for 10% bulk discount)
        var order2 = new Order
        {
            CustomerId = customerIds[1],
            Type = OrderType.Delivery,
            Status = OrderStatus.OutForDelivery,
            DeliveryAddress = "123 Main St, Anytown, USA",
            SpecialInstructions = "Ring doorbell once.",
            CreatedDate = DateTime.UtcNow.Date.AddHours(18).AddMinutes(0),
            EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(18).AddMinutes(20)
        };
        orders.Add(order2);

        // Order 3: Takeout, Pending
        var order3 = new Order
        {
            CustomerId = customerIds[2],
            Type = OrderType.Takeout,
            Status = OrderStatus.Pending,
            SpecialInstructions = "",
            CreatedDate = DateTime.UtcNow.Date.AddHours(11).AddMinutes(45),
            PickupTime = DateTime.UtcNow.Date.AddHours(12).AddMinutes(30) // 30 mins
        };
        orders.Add(order3);

        // Order 4: DineIn, Preparing
        var order4 = new Order
        {
            CustomerId = customerIds[0],
            Type = OrderType.DineIn,
            Status = OrderStatus.Preparing,
            SpecialInstructions = "Extra spicy",
            CreatedDate = DateTime.UtcNow.Date.AddHours(19).AddMinutes(0),
            EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(19).AddMinutes(25)
        };
        orders.Add(order4);

        // Order 5: Delivery, Completed
        var order5 = new Order
        {
            CustomerId = customerIds[3],
            Type = OrderType.Delivery,
            Status = OrderStatus.Completed,
            DeliveryAddress = "456 Oak Ave, Othercity, USA",
            CreatedDate = DateTime.UtcNow.Date.AddHours(13).AddMinutes(10),
            EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(13).AddMinutes(35),
            ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(13).AddMinutes(30)
        };
        orders.Add(order5);

        // Order 6: Takeout, ReadyForPickup
        var order6 = new Order
        {
            CustomerId = customerIds[4],
            Type = OrderType.Takeout,
            Status = OrderStatus.Ready,
            CreatedDate = DateTime.UtcNow.Date.AddHours(9).AddMinutes(0),
            PickupTime = DateTime.UtcNow.Date.AddHours(9).AddMinutes(20)
        };
        orders.Add(order6);

        // Order 7: DineIn, Pending (Breakfast)
        var order7 = new Order
        {
            CustomerId = customerIds[0],
            Type = OrderType.DineIn,
            Status = OrderStatus.Pending,
            CreatedDate = DateTime.UtcNow.Date.AddHours(8).AddMinutes(10)
        };
        orders.Add(order7);

        // Order 8: Delivery, Pending, Happy Hour (3-5 PM), over $100
        var order8 = new Order
        {
            CustomerId = customerIds[1],
            Type = OrderType.Delivery,
            Status = OrderStatus.Pending,
            DeliveryAddress = "789 Pine Rd, Smallville, USA",
            CreatedDate = DateTime.UtcNow.Date.AddHours(16).AddMinutes(0),
            EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(16).AddMinutes(25)
        };
        orders.Add(order8);

        // Order 9: DineIn, Completed, small order
        var order9 = new Order
        {
            CustomerId = customerIds[2],
            Type = OrderType.DineIn,
            Status = OrderStatus.Completed,
            CreatedDate = DateTime.UtcNow.Date.AddHours(20).AddMinutes(15),
            ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(20).AddMinutes(30)
        };
        orders.Add(order9);

        // Order 10: Takeout, Cancelled
        var order10 = new Order
        {
            CustomerId = customerIds[3],
            Type = OrderType.Takeout,
            Status = OrderStatus.Cancelled,
            CreatedDate = DateTime.UtcNow.Date.AddHours(17).AddMinutes(0)
        };
        orders.Add(order10);

        // Order 11: DineIn, Multiple Items
        var order11 = new Order
        {
            CustomerId = customerIds[0],
            Type = OrderType.DineIn,
            Status = OrderStatus.Completed,
            CreatedDate = DateTime.UtcNow.Date.AddHours(14).AddMinutes(0),
            ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(14).AddMinutes(20)
        };
        orders.Add(order11);

        // Order 12: Delivery, Preparing
        var order12 = new Order
        {
            CustomerId = customerIds[1],
            Type = OrderType.Delivery,
            Status = OrderStatus.Preparing,
            DeliveryAddress = "101 River St, Lakeside, USA",
            CreatedDate = DateTime.UtcNow.Date.AddHours(19).AddMinutes(30),
            EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(19).AddMinutes(55)
        };
        orders.Add(order12);

        // Order 13: Takeout, Completed
        var order13 = new Order
        {
            CustomerId = customerIds[2],
            Type = OrderType.Takeout,
            Status = OrderStatus.Completed,
            CreatedDate = DateTime.UtcNow.Date.AddHours(12).AddMinutes(0),
            PickupTime = DateTime.UtcNow.Date.AddHours(12).AddMinutes(30)
        };
        orders.Add(order13);

        // Order 14: DineIn, Pending (Large group order)
        var order14 = new Order
        {
            CustomerId = customerIds[3],
            Type = OrderType.DineIn,
            Status = OrderStatus.Pending,
            SpecialInstructions = "Separate checks please",
            CreatedDate = DateTime.UtcNow.Date.AddHours(18).AddMinutes(45),
        };
        orders.Add(order14);

        // Order 15: Delivery, Completed, Happy Hour
        var order15 = new Order
        {
            CustomerId = customerIds[4],
            Type = OrderType.Delivery,
            Status = OrderStatus.Completed,
            DeliveryAddress = "777 Valley St, Hilltop, USA",
            CreatedDate = DateTime.UtcNow.Date.AddHours(16).AddMinutes(40),
            EstimatedDeliveryTime = DateTime.UtcNow.Date.AddHours(17).AddMinutes(0),
            ActualDeliveryTime = DateTime.UtcNow.Date.AddHours(16).AddMinutes(58)
        };
        orders.Add(order15);


        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();
        Console.WriteLine("Partially seeded Orders to get generated IDs.");

        var orderItems = new List<OrderItem>();
        decimal AddOrderItem(Order currentOrder, MenuItem menuItem, int quantity)
        {
            var item = new OrderItem
            {
                OrderId = currentOrder.Id, // Use the generated OrderId
                MenuItemId = menuItem.Id,
                Quantity = quantity,
                UnitPrice = menuItem.Price ?? 0, // Handle nullable Price
                SpecialInstructions = "",
                // CreatedDate will be handled by DbContext's UpdateAuditFields
            };
            orderItems.Add(item);
            return item.Quantity * item.UnitPrice;
        }

        // Populate Order 1's items and calculate TotalAmount
        decimal subtotal1 = 0;
        subtotal1 += AddOrderItem(order1, menuItems.First(m => m.Name == "Garlic Bread"), 1);
        subtotal1 += AddOrderItem(order1, menuItems.First(m => m.Name == "Fettuccine Alfredo"), 1);
        subtotal1 += AddOrderItem(order1, menuItems.First(m => m.Name == "Coca-Cola"), 2);
        order1.TotalAmount = subtotal1 * 0.80M;

        // Populate Order 2's items
        decimal subtotal2 = 0;
        subtotal2 += AddOrderItem(order2, menuItems.First(m => m.Name == "Ribeye Steak"), 2);
        subtotal2 += AddOrderItem(order2, menuItems.First(m => m.Name == "Grilled Salmon"), 1);
        subtotal2 += AddOrderItem(order2, menuItems.First(m => m.Name == "Chocolate Lava Cake"), 3);
        subtotal2 += AddOrderItem(order2, menuItems.First(m => m.Name == "Fresh Orange Juice"), 4);
        order2.TotalAmount = subtotal2;
        if (order2.TotalAmount > 100) order2.TotalAmount *= 0.90M;

        // Populate Order 3's items
        decimal subtotal3 = 0;
        subtotal3 += AddOrderItem(order3, menuItems.First(m => m.Name == "Buffalo Wings"), 1);
        subtotal3 += AddOrderItem(order3, menuItems.First(m => m.Name == "Espresso"), 1);
        order3.TotalAmount = subtotal3;

        // Populate Order 4's items
        decimal subtotal4 = 0;
        subtotal4 += AddOrderItem(order4, menuItems.First(m => m.Name == "Chicken Tikka Masala"), 2);
        subtotal4 += AddOrderItem(order4, menuItems.First(m => m.Name == "Sparkling Water"), 2);
        order4.TotalAmount = subtotal4;

        // Populate Order 5's items
        decimal subtotal5 = 0;
        subtotal5 += AddOrderItem(order5, menuItems.First(m => m.Name == "Vegetable Lasagna"), 1);
        subtotal5 += AddOrderItem(order5, menuItems.First(m => m.Name == "New York Cheesecake"), 1);
        order5.TotalAmount = subtotal5;

        // Populate Order 6's items
        decimal subtotal6 = 0;
        subtotal6 += AddOrderItem(order6, menuItems.First(m => m.Name == "Fluffy Pancakes"), 1);
        subtotal6 += AddOrderItem(order6, menuItems.First(m => m.Name == "Espresso"), 1);
        order6.TotalAmount = subtotal6;

        // Populate Order 7's items
        decimal subtotal7 = 0;
        subtotal7 += AddOrderItem(order7, menuItems.First(m => m.Name == "Classic Omellette"), 1);
        subtotal7 += AddOrderItem(order7, menuItems.First(m => m.Name == "Fresh Orange Juice"), 1);
        order7.TotalAmount = subtotal7;

        // Populate Order 8's items
        decimal subtotal8 = 0;
        subtotal8 += AddOrderItem(order8, menuItems.First(m => m.Name == "Ribeye Steak"), 3);
        subtotal8 += AddOrderItem(order8, menuItems.First(m => m.Name == "Spring Rolls"), 4);
        subtotal8 += AddOrderItem(order8, menuItems.First(m => m.Name == "Tiramisu"), 2);
        order8.TotalAmount = subtotal8;
        order8.TotalAmount *= 0.80M;
        if (order8.TotalAmount > 100) order8.TotalAmount *= 0.90M;

        // Populate Order 9's items
        decimal subtotal9 = 0;
        subtotal9 += AddOrderItem(order9, menuItems.First(m => m.Name == "Assorted Ice Cream"), 1);
        subtotal9 += AddOrderItem(order9, menuItems.First(m => m.Name == "Green Tea"), 1);
        order9.TotalAmount = subtotal9;

        // Populate Order 10's items
        decimal subtotal10 = 0;
        subtotal10 += AddOrderItem(order10, menuItems.First(m => m.Name == "Crispy Calamari"), 1);
        order10.TotalAmount = subtotal10;

        // Populate Order 11's items
        decimal subtotal11 = 0;
        subtotal11 += AddOrderItem(order11, menuItems.First(m => m.Name == "Loaded Nachos"), 1);
        subtotal11 += AddOrderItem(order11, menuItems.First(m => m.Name == "Chicken Tikka Masala"), 1);
        subtotal11 += AddOrderItem(order11, menuItems.First(m => m.Name == "Berry Smoothie"), 1);
        order11.TotalAmount = subtotal11;

        // Populate Order 12's items
        decimal subtotal12 = 0;
        subtotal12 += AddOrderItem(order12, menuItems.First(m => m.Name == "Fish and Chips"), 2);
        subtotal12 += AddOrderItem(order12, menuItems.First(m => m.Name == "Assorted Ice Cream"), 1);
        order12.TotalAmount = subtotal12;

        // Populate Order 13's items
        decimal subtotal13 = 0;
        subtotal13 += AddOrderItem(order13, menuItems.First(m => m.Name == "Breakfast Burrito"), 1);
        subtotal13 += AddOrderItem(order13, menuItems.First(m => m.Name == "Iced Latte"), 1);
        order13.TotalAmount = subtotal13;

        // Populate Order 14's items
        decimal subtotal14 = 0;
        subtotal14 += AddOrderItem(order14, menuItems.First(m => m.Name == "Ribeye Steak"), 4);
        subtotal14 += AddOrderItem(order14, menuItems.First(m => m.Name == "Fettuccine Alfredo"), 2);
        subtotal14 += AddOrderItem(order14, menuItems.First(m => m.Name == "Crispy Calamari"), 2);
        subtotal14 += AddOrderItem(order14, menuItems.First(m => m.Name == "Coca-Cola"), 4);
        order14.TotalAmount = subtotal14;
        if (order14.TotalAmount > 100) order14.TotalAmount *= 0.90M;

        // Populate Order 15's items
        decimal subtotal15 = 0;
        subtotal15 += AddOrderItem(order15, menuItems.First(m => m.Name == "Bruschetta"), 1);
        subtotal15 += AddOrderItem(order15, menuItems.First(m => m.Name == "Vegetable Lasagna"), 1);
        subtotal15 += AddOrderItem(order15, menuItems.First(m => m.Name == "Crème brûlée"), 1);
        order15.TotalAmount = subtotal15 * 0.80M;


        context.Orders.UpdateRange(orders);
        await context.OrderItems.AddRangeAsync(orderItems);
        await context.SaveChangesAsync();
        Console.WriteLine("Seeded OrderItems and updated Order TotalAmounts.");


        // --- 6. Seed Sales Transactions (for completed orders) ---
        var salesTransactions = new List<SalesTransaction>();
        foreach (var order in orders.Where(o => o.Status == OrderStatus.Completed))
        {
            salesTransactions.Add(new SalesTransaction
            {
                OrderId = order.Id,
                Amount = order.TotalAmount,
                PaymentMethod = (PaymentMethod)random.Next(0, 3), // Randomly pick Cash, CreditCard, or OnlinePayment
                PaymentReference = $"TRANS-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                // CreatedDate will be handled by DbContext's UpdateAuditFields
            });
        }
        await context.SalesTransactions.AddRangeAsync(salesTransactions);
        await context.SaveChangesAsync();
        Console.WriteLine("Seeded SalesTransactions.");


        // --- 7. Seed Table Reservations ---
        var tableReservations = new List<TableReservation>
        {
            new TableReservation { TableId = tables.First(t => t.Number == 3).Id, CustomerId = customerIds[0], Date = DateTime.Today.AddDays(1), Time = new TimeSpan(19, 0, 0) },
            new TableReservation { TableId = tables.First(t => t.Number == 2).Id, CustomerId = customerIds[1], Date = DateTime.Today.AddDays(2), Time = new TimeSpan(12, 30, 0) },
            new TableReservation { TableId = tables.First(t => t.Number == 5).Id, CustomerId = customerIds[4], Date = DateTime.Today.Date, Time = new TimeSpan(21, 0, 0) },
            new TableReservation { TableId = tables.First(t => t.Number == 4).Id, CustomerId = customerIds[2], Date = DateTime.Today.AddDays(3), Time = new TimeSpan(18, 0, 0) }
        };
        await context.TableReservations.AddRangeAsync(tableReservations);
        await context.SaveChangesAsync();
        Console.WriteLine("Seeded TableReservations.");

        Console.WriteLine("Database seeding complete!");
    }
}
