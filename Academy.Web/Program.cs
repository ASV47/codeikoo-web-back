using Academy.Application.MappingProfile.AcademyMappingProfile;
using Academy.Application.Repositories;
using Academy.Application.Services.AcademyServices;
using Academy.Infrastructure.Data;
using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using Academy.Interfaces.IServices.IAcademyServices;
using Academy.Web.CustomMiddlewares;
using Academy.Web.ErrorsModel;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceLayer.Mapping;
using System.Text;

namespace Academy.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Academy", new OpenApiInfo { Title = "Academy API", Version = "v1" });
                c.SwaggerDoc("Company", new OpenApiInfo { Title = "Company API", Version = "v1" });

                c.DocInclusionPredicate((docName, apiDesc) =>
                    string.Equals(apiDesc.GroupName, docName, StringComparison.OrdinalIgnoreCase));

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "Enter: Bearer <space> <token>"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IFileStorageService, UploadcareFileStorageService>();


            builder.Services.AddAutoMapper(typeof(ApplicationProfile));
            builder.Services.AddAutoMapper(typeof(CoursesProfile));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddAutoMapper(typeof(ServiceProfile));
            builder.Services.AddAutoMapper(typeof(TechnologyProfile));
            builder.Services.AddAutoMapper(typeof(ClientProfile));

            //builder.Services.AddScoped<IdentityDataSeeder>();


            builder.Services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(2);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", cors =>
                {
                    cors.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                        .Where(m => m.Value.Errors.Any())
                        .Select(m => new ValidationError
                        {
                            Field = m.Key,
                            Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                        });

                    var response = new ValidationErrorToReturn
                    {
                        ValidationErrors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWTOptions:ISSuer"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWTOptions:Audience"],

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"]!)
                    )
                };
            });

            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var Service = Scope.ServiceProvider;
            var Objects = Service.GetRequiredService<IDataSeeding>();

            

            await Objects.SeedDataAsync();

            
            // Middleware
            app.UseMiddleware<CustomExceptionHandler>();
            app.UseCors("AllowAll");

            // ✅ Swagger in ALL environments (Option 1)
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("./Academy/swagger.json", "Academy API v1");
                c.SwaggerEndpoint("./Company/swagger.json", "Company API v1");
            });

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            // ✅ Important order
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}