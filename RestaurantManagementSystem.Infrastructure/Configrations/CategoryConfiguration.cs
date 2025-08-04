using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagementSystem.Domain.Models;
namespace RestaurantManagementSystem.Infrastructure.Configrations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.Property(e => e.Id)
            //    .ValueGeneratedOnAdd()
            //    .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(c => c.Description)
                .HasMaxLength(500); 

           
            // A Category has many MenuItems
            builder.HasMany(c => c.MenuItems)
                   .WithOne(mi => mi.Category)
                   .HasForeignKey(mi => mi.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);


            //// Configure common BaseEntity properties
            builder.ConfigureBaseEntityProperties();

        }
    }
}
