using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Infrastructure.Configrations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
       

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // IdentityUser base class usually handles Id, but you can configure additional properties
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Address).HasMaxLength(500);
            builder.Property(u => u.CreatedDate).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.Property(u => u.Salary).HasColumnType("decimal(18,2)");

            // Configure the one-to-many relationship from AppUser to Order
            builder.HasMany(u => u.Orders)
                   .WithOne()
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict); // Ensure consistency with OrderConfiguration
        }
    }
}
