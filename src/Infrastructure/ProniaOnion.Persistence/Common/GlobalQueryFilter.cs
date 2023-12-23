using Microsoft.EntityFrameworkCore;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyQuery<T>(ModelBuilder modelBuilder) where T : BaseEntity, new()
        {
            modelBuilder.Entity<T>().HasQueryFilter(c => c.IsDeleted == false);
        }

        public static void ApplyQueryFilter(this ModelBuilder modelBuilder)
        {
            ApplyQuery<Category>(modelBuilder);
            ApplyQuery<Product>(modelBuilder);
            ApplyQuery<Color>(modelBuilder);
            ApplyQuery<Tag>(modelBuilder);
            ApplyQuery<ProductColor>(modelBuilder);
            ApplyQuery<ProductTag>(modelBuilder);
        }
    }
}
