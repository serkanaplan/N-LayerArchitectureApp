﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;

namespace NLayer.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductFeature> ProductFeatures { get; set; }//normalde product ile beraber eklemen gerek. bu daha best practice dir

    public override int SaveChanges()
    {
        foreach (var item in ChangeTracker.Entries())
            if (item.Entity is BaseEntity entityReference)
                switch (item.Entity)
                {
                    case EntityState.Added:
                        {
                            entityReference.CreatedDate = DateTime.Now;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            entityReference.UpdatedDate = DateTime.Now;
                            break;
                        }
                }

        return base.SaveChanges();
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        foreach (var item in ChangeTracker.Entries())
            if (item.Entity is BaseEntity entityReference)
                switch (item.State)
                {
                    case EntityState.Added:
                        {
                            entityReference.CreatedDate = DateTime.Now;
                            break;
                        }
                    case EntityState.Modified:
                        {
                            Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                            entityReference.UpdatedDate = DateTime.Now;
                            break;
                        }
                }

        return base.SaveChangesAsync(cancellationToken);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<ProductFeature>().HasData(
        new ProductFeature()
        {
            Id = 1,
            Color = "Kırmızı",
            Height = 100,
            Width = 200,
            ProductId = 1
        },
        new ProductFeature()
        {
            Id = 2,
            Color = "Mavi",
            Height = 300,
            Width = 500,
            ProductId = 2
        });

        base.OnModelCreating(modelBuilder);
    }
}
