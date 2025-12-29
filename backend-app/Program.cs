using backend_app.Middlewares;
using backend_app.Repositories;
using backend_app.Repositories.Implementation;
using backend_app.Repositories.Interface;
using backend_app.Services.Implementation;
using backend_app.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Injeção
builder.Services.AddScoped<ConnectionDb, ConnectionDb>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

