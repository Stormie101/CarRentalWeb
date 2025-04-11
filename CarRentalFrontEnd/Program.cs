using Microsoft.EntityFrameworkCore;
using CarRentalFrontEnd.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (including DbContext for MySQL).
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 31)) // Use your MySQL version
    )
);

builder.Services.AddSession(); // Enable session
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//database seed
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!context.Cars.Any())
    {
        context.Cars.AddRange(
            new Car
            {
                Model = "Model 3",
                Brand = "Tesla",
                PricePerDay = 650,
                ImagePath = "cars/hondacivic.jpg"
            },
            new Car
            {
                Model = "Corolla",
                Brand = "Toyota",
                PricePerDay = 500,
                ImagePath = "cars/toyotacorolla.jpg"
            },
            new Car
            {
                Model = "Civic",
                Brand = "Honda",
                PricePerDay = 500,
                ImagePath = "cars/teslamodel3.jpg"
            }
        );
        context.SaveChanges();
    }
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseSession(); // Use session middleware


// Define the default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
