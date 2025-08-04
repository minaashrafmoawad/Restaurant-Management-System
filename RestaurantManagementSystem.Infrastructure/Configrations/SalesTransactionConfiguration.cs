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
    public class SalesTransactionConfiguration : IEntityTypeConfiguration<SalesTransaction>
    {
        public void Configure(EntityTypeBuilder<SalesTransaction> builder)
        {
           
            builder.HasKey(st => st.Id);

        
            builder.Property(st => st.OrderId)
                   .IsRequired();

            builder.Property(st => st.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(st => st.PaymentMethod)
                .IsRequired()
                .HasConversion<string>(); // Store PaymentMethod (enum) as string for readability

            builder.Property(st => st.PaymentReference)
                .HasMaxLength(100);

            // Configure relationships
            
            builder.HasOne(st=>st.Order)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // If order is deleted, sales transaction is deleted.
          
                   


            //Configure BaseEntity Properties
            builder.ConfigureBaseEntityProperties();
        }
    }
}
