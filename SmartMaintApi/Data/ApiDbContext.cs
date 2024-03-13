using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartMaintApi.Interceptors;
using SmartMaintApi.Models;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    //private UpdateEntityInfoInterceptor _interceptor = interceptor;
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddInterceptors(new UpdateEntityInfoInterceptor());
}