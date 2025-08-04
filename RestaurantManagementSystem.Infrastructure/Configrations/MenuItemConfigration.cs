using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Infrastructure.Configrations
{
    public class MenuItemConfigration : IEntityTypeConfiguration<MenuItem>
    {

        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(mi => mi.Id);

            // Configure properties
            builder.Property(mi => mi.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(mi => mi.Description)
                    .HasMaxLength(500);

            builder.Property(mi => mi.ImageURL)
                    .HasMaxLength(255); 

            builder.Property(mi => mi.Price)
                    .HasColumnType("decimal(18,2)");

           
            
            builder.Property(mi => mi.CategoryId)
                       .IsRequired();

            // Configure relationships
            // A MenuItem belongs to one Category
            builder.HasOne(mi => mi.Category)
                       .WithMany(c => c.MenuItems)
                       .HasForeignKey(mi => mi.CategoryId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Restrict);


            //Configure BaseEntity Properties
            builder.ConfigureBaseEntityProperties();
        }



    }
}
