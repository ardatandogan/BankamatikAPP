using Bankamatik.Business.Services;
using Bankamatik.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<KurService>();
builder.Services.AddScoped<LogService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<LogRepository>();





// ** Session servisini ekle **
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ** Session middleware'ini ekle **
app.UseSession();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");  // Default route login olsun

app.Run();
