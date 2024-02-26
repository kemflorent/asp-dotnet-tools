using Microsoft.EntityFrameworkCore;
using orderService.Context;
// using orderService.Models;
using orderService.Seeder;

using Microsoft.OpenApi.Models;
using System.Reflection;
using MediatR;
using Config;

var policyName = "allowAllOrigins";
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
    {
        EnvironmentName = Environments.Development // Production
    });

// Add Logging to file
builder.Logging.AddFile("Logs/log-{Date}.txt");
// builder.Configuration.AddJsonFile($"appsettings.Dev.json", optional: true);
builder.Services.AddConsulConfig(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddControllers(); //.AddNewtonsoftJson();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*
options => {
    options.SwaggerDoc("v1", new OpenApiInfo{
        Version = "v1",
        Title = "Order service API",
        Description = "An ASP.NET Core Order service API",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact{
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense{
             Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
        
    });

    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}
*/

builder.Services.AddCors(options => {
    options.AddPolicy(name : policyName, 
        builder => {
            builder
                // .WithOrigins("");
                .AllowAnyOrigin()
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .AllowAnyHeader();
        }
    );
});

// Enable Postgresql to save timestamp with time zone
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

using (var scope = app.Services.CreateScope() )
{
    var services = scope.ServiceProvider; 
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Make db migration before seed
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        // options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        // options.RoutePrefix = string.Empty;
    });
}

app.UseConsul();   // IConfiguration configuration = app.Configuration;
app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers(); // Added for webAPI.
});
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseCors(policyName);

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
