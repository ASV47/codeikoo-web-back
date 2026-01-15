using Academy.Application.MappingProfile.AcademyMappingProfile;
using Academy.Application.Repositories;
using Academy.Application.Services.AcademyServices;
using Academy.Infrastructure.Data;
using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using Academy.Web.CustomMiddlewares;
using Academy.Web.ErrorsModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceLayer.Mapping;
using System.Text;

namespace Academy.Web
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("Academy", new OpenApiInfo { Title = "Academy API", Version = "v1" });
				c.SwaggerDoc("Company", new OpenApiInfo { Title = "Company API", Version = "v1" });

				c.DocInclusionPredicate((docName, apiDesc) =>
					string.Equals(apiDesc.GroupName, docName, StringComparison.OrdinalIgnoreCase));
			});
			builder.Services.AddDbContext<ApplicationDbContext>(Options =>
			{
				Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddScoped<IServiceManager, ServiceManager>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
			//builder.Services.AddScoped<IdentityDataSeeder>();


			builder.Services.AddAutoMapper(typeof(ApplicationProfile));
			builder.Services.AddAutoMapper(typeof(CoursesProfile));
			builder.Services.AddAutoMapper(typeof(MappingProfile));
			builder.Services.AddAutoMapper(typeof(ServiceProfile));
			builder.Services.AddAutoMapper(typeof(TechnologyProfile));
			builder.Services.AddAutoMapper(typeof(ClientProfile));

			//builder.Services.AddIdentityCore<ApplicationUser>()
			//				.AddRoles<IdentityRole>()
			//				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddIdentityCore<ApplicationUser>(options =>
			{
				options.Password.RequiredLength = 6;
			}).AddRoles<IdentityRole>()
			  .AddEntityFrameworkStores<ApplicationDbContext>()
			  .AddDefaultTokenProviders();

			builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
			{
				opt.TokenLifespan = TimeSpan.FromHours(2);
			});

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll",
					builder =>
					{
						builder.AllowAnyOrigin()
							   .AllowAnyMethod()
							   .AllowAnyHeader();
					});
			});

			builder.Services.Configure<ApiBehaviorOptions>((Options) => {
				Options.InvalidModelStateResponseFactory = (context) =>
				{
					var Errors = context.ModelState.Where(M => M.Value.Errors.Any())
					.Select(M => new ValidationError()
					{
						Field = M.Key,
						Errors = M.Value.Errors.Select(E => E.ErrorMessage)
					});
					var Response = new ValidationErrorToReturn()
					{
						ValidationErrors = Errors
					};

					return new BadRequestObjectResult(Response);
				};
			});

			builder.Services.AddAuthentication(Options =>
			{
				Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["JWTOptions:ISSuer"],

					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWTOptions:Audience"],

					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"]))
				};
			});

			var app = builder.Build();

			//using (var scope = app.Services.CreateScope())
			//{
			//	var seeder = scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();
			//	await seeder.IdentityDataSeedAsync();
			//}


			// Configure the HTTP request pipeline.
			app.UseMiddleware<CustomExceptionHandler>();
			app.UseCors("AllowAll");
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				//app.UseSwaggerUI();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/Academy/swagger.json", "Academy API v1");
					c.SwaggerEndpoint("/swagger/Company/swagger.json", "Company API v1");
				});
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
