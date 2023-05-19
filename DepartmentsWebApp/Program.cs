using BusinessLayerLib;
using BusinessLayerLib.Implementations;
using DepartmentsWebApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TestDBLib;
using TestDBLib.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var optionsBuilder = new DbContextOptionsBuilder<TestDBContext>();
var options = optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("TestDb")).Options;
builder.Services.AddScoped<Service>(_ => new(new DataManager(
                                         new EFRepository<Department>(new TestDBContext(options)),
                                         new EFRepository<Employee>(new TestDBContext(options))))); //dependency injection


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

app.UseAuthorization();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
