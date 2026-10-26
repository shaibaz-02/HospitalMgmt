using System.Security.Cryptography;
using HospitalMgmt.Application.Interfaces;
using HospitalMgmt.Application.Services;
using HospitalMgmt.Core.Interfaces;
using HospitalMgmt.Infrastructure;
using HospitalMgmt.Infrastructure.Data;
using HospitalMgmt.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using HospitalMgmt.Application.Mappings;
using HospitalMgmt.Web.Mappings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); 

builder.Services.AddDbContext<HospitalDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddAutoMapper(
    typeof(DepartmentProfile).Assembly,
    typeof(DepartmentViewModelProfile).Assembly);

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.Services
        .GetRequiredService<AutoMapper.IConfigurationProvider>()
        .AssertConfigurationIsValid();
}

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
