using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<ShanLiang21Context>(
 options => options.UseSqlServer(
 builder.Configuration.GetConnectionString("ShanLiangConnection")
));

// Add EmailSender service
builder.Services.AddTransient<IEmailSender, YourEmailSenderService>();
// Configure IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

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
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
  //name: "default",
  //pattern: "{controller=Shopping}/{action=Menu}/{StoreId=1}");
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
