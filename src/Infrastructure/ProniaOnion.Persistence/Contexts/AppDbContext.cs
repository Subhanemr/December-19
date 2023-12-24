﻿using Microsoft.EntityFrameworkCore;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Common;
using System.Reflection;

namespace ProniaOnion.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyQueryFilter();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in entities)
            {
                switch (data.State)
                {
                    case EntityState.Modified:
                        data.Entity.CreateAt = DateTime.Now;
                        break;
                    case EntityState.Added:
                        data.Entity.UpdateAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
