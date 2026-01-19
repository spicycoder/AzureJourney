using DataAccess;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.ConfigureFunctionsWebApplication();

builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ProductsDB");
    options.UseSqlServer(connectionString);
});

builder.Services
     .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
