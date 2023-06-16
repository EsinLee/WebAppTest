using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Policy;
using WebAppTest.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Database Operations - Add services to the database context.
builder.Services.AddDbContext<MainDbContext>(options => 
    options.UseSqlServer(builder.Configuration.
    GetConnectionString("AspPjMDbConnectionString")));

// 使用記憶體模擬 IDistributedCache，正式環境可用 Redis/SQL/NCache
builder.Services.AddDistributedMemoryCache();
// 設定 Cookie 式登入驗證，指定登入登出 Action
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Member/Index";
        options.LogoutPath = "/Member/Logout";
        //options.AccessDeniedPath = "/Auth/AccessDenied";
    });

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

// 啟用身分認證
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 隱藏URL??
/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}"
    );

    // Add a catch-all route to handle unmatched URLs
    endpoints.MapControllerRoute(
        name: "Accounting",
        pattern: "{*url}",
        defaults: new { controller = "Accounting", action = "Index" }
    );
});*/

app.Run();
