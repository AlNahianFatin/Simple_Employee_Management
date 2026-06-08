using Microsoft.EntityFrameworkCore;
using Simple_Employee_Management.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SimpleEmployeeManagementDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SEMDbContext")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromDays(7);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();