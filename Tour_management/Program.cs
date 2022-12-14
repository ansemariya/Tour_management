using Microsoft.EntityFrameworkCore;
using Tour_management.Data;

var builder = WebApplication.CreateBuilder(args);
string connection= builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbCon>(db=>db.UseSqlServer(connection,
    sqlServerOptionsAction:x=>x.EnableRetryOnFailure().CommandTimeout(60)
               ).EnableSensitiveDataLogging());
builder.Services.AddSession(options =>
{
    options.IdleTimeout= TimeSpan.FromSeconds(60);       
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
