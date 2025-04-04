var builder = WebApplication.CreateBuilder(args);

// Add services to the container (Keep basic functionality like Views).
builder.Services.AddControllersWithViews(); // Needed for rendering views (e.g., Login page).

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Static files like CSS and JavaScript
app.UseStaticFiles();

// Define the default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
