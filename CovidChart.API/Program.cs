using CovidChart.API.Hubs;
using CovidChart.API.Models;
using CovidChart.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<CovidService>();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["ConnectionString"]);
});
builder.Services.AddSignalR();
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

app.MapHub<CovidHub>("/CovidHub");

app.MapControllers();

app.Run();
