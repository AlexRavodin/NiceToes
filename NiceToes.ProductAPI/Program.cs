using AutoMapper;
using NiceToes.ProductAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using NiceToes.ProductAPI;
using NiceToes.ProductAPI.Data;
using NiceToes.ProductAPI.Extensions;
using Microsoft.Extensions.Options;
using System.Reflection;
using Serilog;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks()
    .AddCheck<DbHealthCheck>("Database");

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Logging.ClearProviders();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product API",
        Description = "An ASP.NET Core Web API for managing products items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });

    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.AddAppAuthetication();

builder.Services.AddAuthorization();

builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration
        .MinimumLevel.Debug()
        .WriteTo.File(Path.Combine(AppContext.BaseDirectory, @"log.txt"), rollingInterval: RollingInterval.Day)
        .WriteTo.Console();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/_health");

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.UseHealthChecks("/_health", 8999);

ApplyMigration();
app.Run();


void ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (_db.Database.GetPendingMigrations().Any())
    {
        _db.Database.Migrate();
    }
}