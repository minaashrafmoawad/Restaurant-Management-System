//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using RestaurantManagementSystem.Domain.Enums;
//using RestaurantManagementSystem.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RestaurantManagementSystem.Infrastructure.Configrations
//{
//    public class OrderConfigration:IEntityTypeConfiguration<Order>
//    {
//        public void Configure(EntityTypeBuilder<Order> builder)
//        {
//            // Configure primary key
//            builder.HasKey(o => o.Id);


//            builder.Property(o => o.CustomerId)
//                   .IsRequired();

//            builder.Property(o => o.Type)
//                .IsRequired()
//                .HasConversion<string>(); // Store enum as string in DB for readability

//            builder.Property(o => o.Status)
//                .IsRequired()
//                .HasDefaultValue(OrderStatus.Pending)
//                .HasConversion<string>(); // Store enum as string in DB

//            builder.Property(o => o.TotalAmount)
//                .IsRequired()
//                .HasColumnType("decimal(18,2)");

//            builder.Property(o => o.SpecialInstructions)
//                .HasMaxLength(500);

//            builder.Property(o => o.EstimatedDeliveryTime);
//            builder.Property(o => o.ActualDeliveryTime);
//            builder.Property(o => o.PickupTime);



//            // An Order has many OrderItems
//            builder.HasMany(o => o.OrderItems)
//                   .WithOne(oi => oi.Order)
//                   .HasForeignKey(oi => oi.OrderId)
//                   .OnDelete(DeleteBehavior.Cascade); // Cascade delete order items if order is deleted


//            // Configure common BaseEntity properties
//            builder.ConfigureBaseEntityProperties();
//        }
//    }
//}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagementSystem.Domain.Enums;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Infrastructure.Configrations
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            
            // builder.HasKey(o => o.Id); // No need to explicitly configure here if BaseEntity handles it

            builder.Property(o => o.CustomerId)
                    .IsRequired(false); // CustomerId can be null if order is placed by guest or through other means

            builder.Property(o => o.Type)
                .IsRequired()
                .HasConversion<string>(); // Store enum as string in DB for readability

            builder.Property(o => o.Status)
                .IsRequired()
                .HasDefaultValue(OrderStatus.Pending)
                .HasConversion<string>(); // Store enum as string in DB

            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.SpecialInstructions)
                .HasMaxLength(500);

            //builder.Property(o => o.EstimatedDeliveryTime);
            //builder.Property(o => o.ActualDeliveryTime);
            //builder.Property(o => o.PickupTime);
            builder.Property(o => o.DeliveryAddress)
                   .HasMaxLength(500); // Assuming a reasonable max length for addresses

            // An Order has many OrderItems
            builder.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete order items if order is deleted

   

            // Configure common BaseEntity properties using the extension method
            builder.ConfigureBaseEntityProperties();
        }
    }
}