using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SiteAsientos.Models;
using SiteAsientos.Services;

var builder = WebApplication.CreateBuilder(args);

//Configuracion de cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opcion => {
    opcion.LoginPath = "/Login/SignIn";
    opcion.ExpireTimeSpan = TimeSpan.FromSeconds(20);
    opcion.AccessDeniedPath = "/Login/SignIn";
});

//Sesiones
builder.Services.AddResponseCaching();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CubreasientosContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("SiteAsientos"));
});

//Servicios de login
builder.Services.AddScoped<ILoginService, LoginService>();

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

//Habilitar autenticacion de usuarios
app.UseAuthentication();

app.UseAuthorization();

//Habilitar sesiones
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
