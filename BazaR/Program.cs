using BazaR.Data;
using BazaR.Filters;
using BazaR.Interfaces;
using BazaR.Models;
using BazaR.Repositories;
using BazaR.Repository;
using BazaR.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(o =>
{
    o.Filters.Add<UserContextFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;

    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;

    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(12);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "BazaR";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.ExpireTimeSpan = TimeSpan.FromHours(12);
    options.SlidingExpiration = true;
    options.LoginPath = "/";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services
    .AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = "596714054566-thnvk9mt56pa9fr64escum0ucj1hsr9b.apps.googleusercontent.com";
        options.ClientSecret = "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";
        options.SaveTokens = true;
    });

builder.Services.AddScoped<DbMaker>();
builder.Services.AddScoped<UserContextFilter>();
builder.Services.AddScoped<LoggerActionFilter>();
builder.Services.AddScoped<BlockResourseFilter>();
builder.Services.AddScoped<OnlineResourceFilter>();
builder.Services.AddSingleton<ActiveUsersService>();
builder.Services.AddScoped<IUserDb, UserRepository>();
builder.Services.AddTransient<ILogDb, LogDbRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IUserStatistick, UserStatistickRpeository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbMaker = scope.ServiceProvider.GetRequiredService<DbMaker>();
    await dbMaker.MakeAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".svg"] = "image/svg+xml";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Site}/{action=Index}/{id?}");

app.Run();