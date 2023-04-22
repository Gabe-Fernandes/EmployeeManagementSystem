using EMS.Data;
using EMS.Data.RepoInterfaces;
using EMS.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContextConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAppUserRepo, AppUserRepo>();
builder.Services.AddTransient<ITimecardRepo, TimecardRepo>();
builder.Services.AddTransient<IWorkdayRepo, WorkdayRepo>();

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
