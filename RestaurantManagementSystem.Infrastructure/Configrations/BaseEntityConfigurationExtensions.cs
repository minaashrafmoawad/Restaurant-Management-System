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
    public static class BaseEntityConfiguration
    {
        public static void ConfigureBaseEntityProperties<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : BaseEntity
        {
            builder.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(e => e.CreatedDate)
                .IsRequired();

            builder.Property(e => e.UpdatedDate); // Nullable

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false); // Default value for soft delete

            builder.Property(e => e.CreatedById)
                .HasMaxLength(100);

            builder.Property(e => e.UpdatedById)
                .HasMaxLength(100);
        }
    }
}
