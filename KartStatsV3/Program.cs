using KartStatsV3.BLL;
using KartStatsV3.BLL.Interfaces;
using KartStatsV3.DAL;
using KartStatsV3.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YourNamespace.BLL.Services;
using YourNamespace.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllersWithViews();
services.AddSession();

services.AddSingleton<IGroupRepository, GroupRepository>();
services.AddScoped<IGroupService, GroupService>();

services.AddSingleton<IInviteRepository, InviteRepository>();
services.AddScoped<IInviteService, InviteService>();

services.AddSingleton<ICircuitRepository, CircuitRepository>();
services.AddScoped<ICircuitService, CircuitService>();

services.AddSingleton<ILaptimeRepository, LaptimeRepository>();
services.AddScoped<ILaptimeService, LaptimeService>();

services.AddSingleton<IResultRepository, ResultRepository>();
services.AddScoped<IResultService, ResultService>();

services.AddSingleton<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();

services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
