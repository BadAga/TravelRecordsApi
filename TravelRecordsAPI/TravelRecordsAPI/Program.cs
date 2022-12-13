using Microsoft.EntityFrameworkCore;
using TravelRecordsAPI.Models;
using TravelRecordsAPI.Repository;
using TravelRecordsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect to DB
var connectionString = builder.Configuration.GetConnectionString("TRConnection");
builder.Services.AddDbContext<CoreDbContext>(options => options.UseSqlServer(connectionString));
//blob storage
builder.Services.AddTransient<IAzureStorage, AzureStorage>();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
