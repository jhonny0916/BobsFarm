using AutoMapper;
using BobsFarm.Models.AutoMapperConfig;
using BobsFarm_AL.Interfaces;
using BobsFarm_AL.Services;
using BobsFarm_BL.Interfaces;
using BobsFarm_BL.Managers;
using BobsFarm_DA.Interfaces;
using BobsFarm_DA.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICornService,CornService>();
builder.Services.AddScoped<ICornManager,CornManager>();
builder.Services.AddScoped<ICornRepository,CornRepository>();

// Rate Limiting Middleware to Limit the number of requests by clientId per minute
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("CornRateLimiter", httpContext =>
    {
        // Extract clientId from route parameters
        string clientId = httpContext.Request.RouteValues.TryGetValue("clientId", out var clientIdValue)
            ? clientIdValue?.ToString() ?? ""
            : "";

        Console.WriteLine($"Extracted clientId: {clientId}");

        if (string.IsNullOrEmpty(clientId))
        {
            clientId = "anonymous";
        }

        Console.WriteLine($"Applying rate limiting for client: {clientId}");

        return RateLimitPartition.GetFixedWindowLimiter(
            ComputeHash(clientId),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 1,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }
        );
    });
});


var configMapper = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});

var mapper = configMapper.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom Middleware to Return 429 Instead of 503
app.Use(async (context, next) =>
{
    await next();

    // If rate limiter blocks the request, change 503 -> 429
    if (context.Response.StatusCode == 503)
    {
        context.Response.StatusCode = 429; // Too Many Requests
        await context.Response.WriteAsync("429 Too Many Requests: Please wait before purchasing more corn.");
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.Run();

static string ComputeHash(string input)
{
    using (var sha256 = SHA256.Create())
    {
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hash); // Unique hash key for partitioning
    }
}
