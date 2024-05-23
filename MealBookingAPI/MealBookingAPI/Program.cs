using MealBookingAPI.Data;
using MealBookingAPI.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using MealBookingAPI.Data.Repository;
using AutoMapper;
using MealBookingAPI.Application.Mapper;
using System.Runtime.CompilerServices;
using MealBookingAPI.Application.Services.IServices;
using MealBookingAPI.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("WorkLaptop"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("HomeLaptop"));
});
builder.Services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MapperProfile())).CreateMapper());
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBookingServices,BookingServices>();
builder.Services.AddCors(cors=> cors.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
