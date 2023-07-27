using Intern.IdentityServer;
using Intern.IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (seed)
{
    SeedData.EnsureSeedData(defaultConnectionString);
}


builder.Services.AddDbContext<AspNetIdentityDbContext>(options => options.UseSqlServer(defaultConnectionString, b => b.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(defaultConnectionString, opt => opt.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.Run();
