using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;
using SIOP.FEVRIPS;
using SIOP.Services.DockerRips;
using SIOP.Services.Email;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
           .CreateLogger();

var misReglasCors = "ReglasCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: misReglasCors,
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "SIOP API FOSCAL", Version = "v1" });
});

builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("settings").GetSection("secretKey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddScoped<DockerRips>();
builder.Services.AddScoped<ServicesFEV>();
builder.Services.AddScoped<EmailServices>();
var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(misReglasCors);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


