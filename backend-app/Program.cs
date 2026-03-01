using System.Text;
using System.Threading.RateLimiting;
using backend_app.Core.Application.Interfaces.Repositories;
using backend_app.Core.Application.Interfaces.Services;
using backend_app.Core.Application.Services.Implementation;
using backend_app.Infrastructure.Data.Repositories.Implementations;
using backend_app.Infrastructure.ExternalServices;
using backend_app.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Injeção
builder.Services.AddScoped<ConnectionDb, ConnectionDb>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITokenPasswordRepository, TokenPasswordRepositoryImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

builder.Services.AddControllers();

var key = Encoding.ASCII.GetBytes("issodeveserumachavedesegurancatopdemias123456789");

static string GetKey(HttpContext context)
{
    var userId = context.User.Identity?.IsAuthenticated == true
        ? context.User.Identity.Name
        : context.Connection.RemoteIpAddress?.ToString();

    return $"{userId}_{context.Request.Path}";
}

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(token =>
{
    token.RequireHttpsMetadata = false;
    token.SaveToken = true;
    token.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };

});

builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("ResetPassword", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: GetKey(context),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(10),
            })
    );

    options.AddPolicy("Login", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: GetKey(context),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromHours(1),
            })
    );

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
} else
{
    app.UseHttpsRedirection();
}

app.UseRateLimiter();

app.UseGlobalExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

