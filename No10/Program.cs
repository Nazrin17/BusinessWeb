using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using No10.Context;
using No10.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BusinessDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddIdentity<AppUser, IdentityRole>(op =>
{
    op.Password.RequireNonAlphanumeric=true;
    op.Password.RequireUppercase = true;
    op.Password.RequireDigit = true;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<BusinessDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
