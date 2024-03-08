using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartMaintApi.Models;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureAuditDataMapping<User>(modelBuilder);
    }

    private void ConfigureAuditDataMapping<TEntity>(ModelBuilder modelBuilder)
        where TEntity : class, IAuditTrail
    {
        modelBuilder.Entity<TEntity>()
            .Property(u => u.Audit.LastAction)
            .HasColumnName("LastAction");

        modelBuilder.Entity<TEntity>()
            .Property(u => u.Audit.UpdateUser)
            .HasColumnName("UpdateUser");

        modelBuilder.Entity<TEntity>()
            .Property(u => u.Audit.TimeStamp)
            .HasColumnName("TimeStamp");
    }
}