using AspNetCore.Swagger.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

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