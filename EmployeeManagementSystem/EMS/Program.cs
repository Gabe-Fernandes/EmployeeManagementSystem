using EMS.Data;
using EMS.Data.Models;
using EMS.Data.RepoInterfaces;
using EMS.Data.Repositories;
using EMS.Services.Email;
using EMS.Views.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContextConnection")));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
		.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(IdentityVM.Cookie).AddCookie(IdentityVM.Cookie, options =>
{
	options.Cookie.Name = IdentityVM.Cookie;
	options.LoginPath = "/Identity/Login";
	options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminOnly", policy => policy.RequireRole(IdentityVM.Admin));
});

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.AddTransient<IAppUserRepo, AppUserRepo>();
builder.Services.AddTransient<ITimecardRepo, TimecardRepo>();
builder.Services.AddTransient<IWorkdayRepo, WorkdayRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Timecard}/{action=EnterTimecard}/{id?}");

app.Run();
