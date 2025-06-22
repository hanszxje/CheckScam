using CheckScam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

// Configure SQL Server
builder.Services.AddDbContext<CheckScamDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<CheckScamDbContext>()
    .AddDefaultTokenProviders();

// Configure login path
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Gr1/Login";
    options.AccessDeniedPath = "/Gr1/Index";
});

// Enable static files
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

// Initialize Superuser role
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Superuser"))
    {
        await roleManager.CreateAsync(new IdentityRole("Superuser"));
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CheckScam.Middleware.AdminRequiredMiddleware>();

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "media")),
    RequestPath = "/media"
});

app.MapControllerRoute(
    name: "urlScam",
    pattern: "UrlScam/{action=Index}/{id?}",
    defaults: new { controller = "UrlScam" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gr1}/{action=Index}/{id?}");

app.Run();