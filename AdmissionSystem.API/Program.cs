using NLog;
using NLog.Web;
using AdmissionSystem.Infrastructure.Data;
using AdmissionSystem.Core.Interfaces;
using AdmissionSystem.Services.Services;
using AdmissionSystem.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using AutoMapper;
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

   
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    
    builder.Services.AddDbContext<AdmissionDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    
    builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));

    
    builder.Services.AddScoped<IAuthService, AuthService>();


    builder.Services.AddAutoMapper(typeof(Program).Assembly);

    
    builder.Services.AddHealthChecks()
        .AddCheck<DatabaseHealthCheck>("database");

    
    var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfig>();
    if (jwtConfig == null || string.IsNullOrEmpty(jwtConfig.Secret))
    {
        throw new InvalidOperationException("JWT configuration is missing or invalid");
    }

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
            };
        });

    builder.Services.AddAuthorization();

    var app = builder.Build();

    
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AdmissionDbContext>();
        await context.Database.MigrateAsync();
       
    }

    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    
    app.UseAuthentication();
    app.UseAuthorization();

   
    app.MapHealthChecks("/health");

    app.MapControllers();

    logger.Info("Application started successfully");
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}


public class DatabaseHealthCheck : IHealthCheck
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DatabaseHealthCheck(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AdmissionDbContext>();
            
            var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);
            
            return canConnect 
                ? HealthCheckResult.Healthy("Database is connected") 
                : HealthCheckResult.Unhealthy("Database connection failed");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database connection failed", ex);
        }
    }
}