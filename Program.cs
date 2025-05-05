using MapTask.Data;
using MapTask.Services.Interfaces;
using MapTask.Models;
using MapTask.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) 
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment;

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: false)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

environment = builder.Environment;


Console.WriteLine($" Environment: {builder.Environment.EnvironmentName}");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                .Replace("|ROOT|", builder.Environment.ContentRootPath);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var emailUser = builder.Configuration["EmailSettings:Username"];
builder.Services.Configure<PasswordSettings>(
    builder.Configuration.GetSection("PasswordSettings"));


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Host.UseSerilog();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<ISearchHistoryService, SearchHistoryService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

