using Microsoft.EntityFrameworkCore;
using PostTrades.Data;
using PostTrades.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add database connection
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<PostTradesDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// Add Repositories
builder.Services.AddScoped<IBidRepository, BidRepository>();

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

app.Run();
