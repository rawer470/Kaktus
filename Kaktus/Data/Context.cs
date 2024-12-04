using System;
using Kaktus.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaktus.Data;

public class Context : DbContext
{
    public DbSet<FileModel> Files { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql(@"Host=185.233.200.76;Port=5432;Username=postgres;Password=rvH46D9UM1;Database=kaktus");

}
