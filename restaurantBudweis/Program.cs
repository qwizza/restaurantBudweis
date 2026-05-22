using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Model.AuthApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("restaurantBudweisDb")));

builder.Services.AddScoped<IPasswordHasher<AuthUser>, PasswordHasher<AuthUser>>();

// Регистрация Typed Client


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // куда перекидывать неавторизованных
        options.AccessDeniedPath = "/Account/AccessDenied"; // можно отдельно /AccessDenied
        options.Cookie.Name = "StudentLibraryCookie";
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // Срок действия
    });

builder.Services.AddAuthorization(); // авторизация

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();// порядок важен
app.UseAuthorization(); // внимание эта после UseAuthentication

app.UseStaticFiles();
app.MapRazorPages();

app.Run();

System.Diagnostics.Process.Start("http://localhost:5000");
