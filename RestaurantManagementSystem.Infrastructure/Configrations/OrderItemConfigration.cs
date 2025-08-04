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
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {


        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

       

          

            // An OrderItem belongs to one MenuItem
            builder.HasOne(oi => oi.MenuItem)
                   .WithMany() 
                   .HasForeignKey(oi => oi.MenuItemId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict); // Prevent menu item deletion if associated with an order item

            // Configure common BaseEntity properties
            builder.Property(oi => oi.CreatedDate)
                .IsRequired();
            builder.Property(oi => oi.UpdatedDate);
            builder.Property(oi => oi.IsDeleted)
                .HasDefaultValue(false);
         


          //  -------------------------

            builder.HasKey(oi => oi.Id);



            builder.Property(oi => oi.OrderId)
                   .IsRequired();

            builder.Property(oi => oi.MenuItemId)
                .IsRequired();

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.Property(oi => oi.SpecialInstructions)
                .HasMaxLength(500);


            
            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade); // If order is deleted, its items are deleted


            // Configure common BaseEntity properties

            builder.ConfigureBaseEntityProperties();

        }
    }
}
