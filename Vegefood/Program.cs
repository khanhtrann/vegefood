using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vegefood.Models;
using Vegefood.Models.Interfaces;
using Vegefood.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IInventory, InventoryManager>();
builder.Services.AddScoped<IShop, ShopManager>();
builder.Services.AddScoped<IPayment, PaymentManager>();
builder.Services.AddScoped<IOrder, OrderManager>();

builder.Services.AddDbContext<ApplicationDbContext>
	(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDb")));
builder.Services.AddDbContext<StoreDbContext>
	(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VegeshopDb")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
})
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();
builder.Services.AddScoped<RoleInitializer>();

builder.Services.AddAuthorization(options =>
			options.AddPolicy("AdminOnly", policy => policy.RequireRole(ApplicationRoles.Admin)));
builder.Services.ConfigureApplicationCookie(configure => { configure.LoginPath = "/Login"; });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

using var scope = app.Services.CreateScope();
var roleInitializer = scope.ServiceProvider.GetRequiredService<RoleInitializer>();
await roleInitializer.InitializeAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();


app.Run();
