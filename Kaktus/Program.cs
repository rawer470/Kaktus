using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Kaktus.Data;
using Kaktus.Models;
using Kaktus.Services;
using Kaktus.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Use Notify on Sait
//FIX 

//Service Porstgre Database
builder.Services.AddDbContext<Context>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("MyDatabase")));
builder.Services.AddIdentity<User, IdentityRole>().
   AddEntityFrameworkStores<Context>().
   AddDefaultTokenProviders();

//Repository Db 
builder.Services.AddScoped<IUserRepository, UserRepository>(); //Repository User

// позволяет добраться до сессий
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseNotyf();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
