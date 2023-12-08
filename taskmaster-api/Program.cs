using Microsoft.EntityFrameworkCore;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Repositories;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;
using taskmaster_api.Services;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Entities.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection"));
});

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<ITaskAppService, TaskAppService>();

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
