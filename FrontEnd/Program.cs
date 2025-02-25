using FrontEnd.Services;
using FrontEnd.Services.IServices;
using FrontEnd.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// For HttpClientFactory 
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IDiscountCouponService, DiscountCouponService>();

// Configured Services Urls
SD.DiscountCouponApiBase = builder.Configuration["ServiceUrls:DiscountCouponApi"];

builder.Services.AddScoped<IBaseService,BaseService>();
builder.Services.AddScoped<IDiscountCouponService, DiscountCouponService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
