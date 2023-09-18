using Microsoft.EntityFrameworkCore;
using Shopthoitrang.Data;
using Shopthoitrang.DataContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Shopthoitrang.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//cache
builder.Services.AddDistributedMemoryCache();
//build sesstion 
builder.Services.AddSession(option =>
{
    option.IdleTimeout=TimeSpan.FromMinutes(30);
    option.Cookie.IsEssential = true;

});
//Identity

//Conect 
builder.Services.AddDbContext<Datacontext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectDb"]);
});
var app = builder.Build();
//section
app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
//Ahtnetion
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();
//Areas
app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//đăng kí seeding
var contex = app.Services.CreateScope().ServiceProvider.GetRequiredService<Datacontext>();
Seeding.seed(contex);
app.Run();
