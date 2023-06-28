using BL.Service;
using DAL.Models;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CalculosDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
    );
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICalculo, CalculoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Calculo/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculo}/{action=Index}/{id?}");

app.Run();
