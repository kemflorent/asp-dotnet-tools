using Microsoft.EntityFrameworkCore;
using CrudWebApp.Data;
using CrudWebApp.Models;
using CrudWebApp.Models.Seeder;
using CrudWebApp.Interfaces;
using CrudWebApp.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
// using CrudWebApp.Controllers;


var policyName = "allowAllOrigins";
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
    {
        EnvironmentName = Environments.Development // Production
    }); // args

// Add Logging to file
builder.Logging.AddFile("Logs/log-{Date}.txt");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo{
        Version = "v1",
        Title = "TODO API",
        Description = "An ASP.NET Core web API for managing Todo items",
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
});
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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        // options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        // options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors(policyName);
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapRazorPages();
    endpoints.MapControllers(); // Added for webAPI.
});

app.MapRazorPages();

app.Run();
