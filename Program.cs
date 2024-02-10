using Microsoft.EntityFrameworkCore;
using StudentCourseHub.Data;
using StudentCourseHub.Endpoints;
using StudentCourseHub.Models.Domain;
using StudentCourseHub.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString: connString));
builder.Services.AddScoped<StudentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapStudentEndpoints();

app.Run();