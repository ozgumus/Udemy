using dotnet_basic.Models;
using dotnet_basic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Email tanımlaması
builder.Services.AddTransient<IEmailService, SmtpEmailServis>();
builder.Services.AddTransient<ICartService, CartService>(); 
builder.Services.AddScoped<ICartService, CartService>();


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 4; // en kısa şifre uzunluğu
    options.Password.RequireNonAlphanumeric = false;//altçizgi nokta gibi karakterin kullanım zorunluluğu
    options.Password.RequireLowercase = false; // Küçük değer kullanım zorunluluğu
    options.Password.RequireUppercase = false; // Büyük değer kullanım zorunluluğu
    options.Password.RequireDigit = false; //Sayısal karakter zorunluluğu

    options.User.RequireUniqueEmail = true; // Email adresi ile ikinci bir kayıt oluşturulamasın. Email benzersiz olsun
                                            // options.User.AllowedUserNameCharacters="abcdefghijklmnopqrstuvwxyz0123456789@_-"; // UserName alanında sadece bu karakterlere izin verilsin.

    options.Lockout.MaxFailedAccessAttempts = 5; // 5 kez yanlış girdiği zaman kullanıcının girişi engellenir.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // 5 dakika bekledikten sonra tekrar giriş deneyebilir.
});

builder.Services.ConfigureApplicationCookie(Options =>
{
    Options.LoginPath = "/Account/Login"; // Giriş yapmadan yetki gerektiren bir alana girmek isterse login sayfasına otomatik yönlendirilmesi bu alandan ayarlanıyor. Bu alan default fakat değiştirilebilir.
    Options.AccessDeniedPath = "/Account/AccessDenied"; // Giriş yapan bir kullanıcı kendi yetkisi olmayan bir alana girmek istediğinde bu alana yönlendirilir. Bu alan default fakat değiştirilebilir.   
    Options.ExpireTimeSpan = TimeSpan.FromDays(30); // Bir kullanıcı bir tarayıcıdan giriş yaptığı zaman tekrar kullanıcı bilgisini girme zorunluluğu süresi buradan belirlenir. 30 gün boyunca tekrar giriş yapmasına gerek kalmasın demiş oluyoruz burada.
    Options.SlidingExpiration= true; // True olması halinde  Options.ExpireTimeSpan alanındaki süre yeniden başlar.

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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "urunler_by_kategori",
    pattern: "urunler/{url?}",
    defaults: new { controller = "Urun", action = "List" })
    .WithStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
