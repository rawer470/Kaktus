using System;
using Kaktus.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kaktus.Data;

public class Context : IdentityDbContext<User>
{
    public DbSet<FileModel> Files { get; set; }
    public Context(DbContextOptions<Context> options) : base(options) { }

}
