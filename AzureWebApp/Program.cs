using AspNetCore.Swagger.Themes;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("ProductsDB");
builder.Services.Initialize(connectionString);

var app = builder.Build();

app.MapDefaultEndpoints();

await app.Services.Migrate();

app.MapOpenApi();
app.UseSwaggerUI(
    Theme.Futuristic,
    options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Azure Web Api");
        options.EnableAllAdvancedOptions();
        options.ShowBackToTopButton();
        options.EnableThemeSwitcher();
    });

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();