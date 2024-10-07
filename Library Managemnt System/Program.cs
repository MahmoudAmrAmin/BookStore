using Library_Managemnt_System;
using Library_Managemnt_System.Models;
using Library_Managemnt_System.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // Set the limit (here 100MB)
});

builder.Services.AddDbContext<LibraryContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 4;
    option.Password.RequireDigit = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<LibraryContext>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


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
