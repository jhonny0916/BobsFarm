using AutoMapper;
using BobsFarm.Models.AutoMapperConfig;
using BobsFarm_AL.Interfaces;
using BobsFarm_AL.Services;
using BobsFarm_BL.Interfaces;
using BobsFarm_BL.Managers;
using BobsFarm_DA.Interfaces;
using BobsFarm_DA.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICornService,CornService>();
builder.Services.AddScoped<ICornManager,CornManager>();
builder.Services.AddScoped<ICornRepository,CornRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.Run();
