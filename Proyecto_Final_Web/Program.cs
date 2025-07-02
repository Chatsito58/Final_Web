using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final_Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Proyecto_Final_Web.Logica;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Acceso/Index";
        options.AccessDeniedPath = "/Acceso/Denegado";
        options.ExpireTimeSpan = TimeSpan.FromDays(20); 
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BDCanchasContext>(conexion =>
{
    conexion.UseSqlServer(builder.Configuration.GetConnectionString("ConexionBD"));
});

builder.Services.AddScoped<Logica_Usuarios>();

// En Program.cs
builder.Logging.AddConsole();
builder.Logging.AddDebug();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Index}/{id?}");

app.Run();
