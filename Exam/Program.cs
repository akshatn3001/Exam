using Exam.Interfaces;
using Exam.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text;

public static class Program
{
    public static void Main(string[] args)
    {
        var config = InitializeConfiguration(args);
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder, config);
        var app = builder.Build();
        Configure(app, builder.Environment);
        app.Run();
    }

    private static IConfigurationRoot InitializeConfiguration(string[] args)
    {
        var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddCommandLine(args)
             .AddEnvironmentVariables();
        return builder.Build();
    }

    private static void ConfigureServices(WebApplicationBuilder builder, IConfigurationRoot config)
    {
        builder.Services.AddControllers();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Task app Auth",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        builder.Services.AddCors(c =>
        {
            c.AddPolicy("CorsPolicy",
             options => options.WithOrigins("https://localhost:7259")
                              .AllowAnyMethod()
                              .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header")
                              .AllowCredentials());
        });

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IExamService, ExamService>();

        builder.Services.AddScoped<JwtService>();



        builder.Services.AddDbContext<ExamDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
    }

    private static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.UseCors(policy => policy.AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        /*  app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true)
              .AllowCredentials());*/

        app.MapControllers();
    }
}