using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.StaticData;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Experience>()
						.Property(e => e.Content)
						.HasConversion(
							v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!), // تحويل القائمة لنص JSON عند الحفظ
							v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!)! // تحويل النص لقائمة عند القراءة
						);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			modelBuilder.Entity<SiteContact>()
						.Property(e => e.Type)
						.HasConversion<string>();

			modelBuilder.Entity<ApplicationUser>().ToTable("Users");
			modelBuilder.Entity<IdentityRole>().ToTable("Roles");
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
			modelBuilder.Ignore<IdentityUserClaim<string>>();
			modelBuilder.Ignore<IdentityUserToken<string>>();
			modelBuilder.Ignore<IdentityUserLogin<string>>();
			modelBuilder.Ignore<IdentityRoleClaim<string>>();

			modelBuilder.Entity<Job>()
						.ToTable("Jobs", schema: AcademySchema.AcademyDBSchema);

			modelBuilder.Entity<JobApplication>()
				.ToTable("JobApplications", schema: AcademySchema.AcademyDBSchema);

			modelBuilder.Entity<InstructorApplication>()
				.ToTable("InstructorApplications", schema: AcademySchema.AcademyDBSchema);

			modelBuilder.Entity<ContactMessage>()
				.ToTable("ContactMessages", schema: AcademySchema.AcademyDBSchema);

			modelBuilder.Entity<Course>()
				.ToTable("Courses", schema: AcademySchema.AcademyDBSchema);

			modelBuilder.Entity<CourseUnit>()
				.ToTable("CourseUnits", schema: AcademySchema.AcademyDBSchema);

			modelBuilder.Entity<UnitLesson>()
				.ToTable("UnitLessons", schema: AcademySchema.AcademyDBSchema);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			#region Company
			modelBuilder.Entity<CompanyCourse>()
						.ToTable("CompanyCourses", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<CourseEnrollment>()
				.ToTable("CourseEnrollments", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<Article>()
				.ToTable("Articles", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<CompanyJopApplication>()
				.ToTable("CompanyJopApplications", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<CompanyContactMessage>()
				.ToTable("CompanyContactMessages", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<Mission>()
				.ToTable("Missions", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<Technology>()
				.ToTable("Technologies", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<SiteContact>()
				.ToTable("SiteContacts", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<Service>()
				.ToTable("Services", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<Experience>()
				.ToTable("Experiences", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<ExperienceCategory>()
				.ToTable("ExperienceCategories", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<ExperienceItem>()
				.ToTable("ExperienceItems", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<Client>()
				.ToTable("Clients", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<AboutUs>()
				.ToTable("AboutUs", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<Achievement>()
				.ToTable("Achievements", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<FlexibilitySection>()
				.ToTable("FlexibilitySections", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<FlexibilityItem>()
				.ToTable("FlexibilityItems", schema: CompanySchema.CompanyDBSchema);

			modelBuilder.Entity<WebSettings>()
				.ToTable("WebSettings", schema: CompanySchema.CompanyDBSchema);
            #endregion

            var stringListConverter = new ValueConverter<List<string>, string>(
        v => JsonSerializer.Serialize(v ?? new(), (JsonSerializerOptions?)null),
        v => string.IsNullOrWhiteSpace(v)
            ? new List<string>()
            : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
    );

            modelBuilder.Entity<Job>(b =>
            {
                b.Property(x => x.RequirementsAr).HasConversion(stringListConverter).HasColumnType("nvarchar(max)");
                b.Property(x => x.RequirementsEn).HasConversion(stringListConverter).HasColumnType("nvarchar(max)");
            });
        }

		public DbSet<ContactMessage> ContactMessages { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<JobApplication> JobApplications { get; set; }
		public DbSet<ImageSlider> ImageSliders { get; set; }
        public DbSet<InstructorApplication> InstructorApplications { get; set; }
		public DbSet<StudentCourseComment> StudentCourseComments { get; set; }	

		#region Company
		public DbSet<CompanyCourse> CompanyCourses { get; set; }
		public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<CompanyJopApplication> CompanyJopApplications { get; set; }
		public DbSet<CompanyContactMessage> CompanyContactMessages { get; set; }
		public DbSet<Mission> Missions { get; set; }
		public DbSet<Technology> Technologies { get; set; }
		public DbSet<SiteContact> SiteContacts { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<Experience> Experiences { get; set; }
		public DbSet<ExperienceCategory> ExperienceCategories { get; set; }
		public DbSet<ExperienceItem> ExperienceItems { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<AboutUs> AboutUs { get; set; }
		public DbSet<Achievement> Achievements { get; set; }
		public DbSet<FlexibilitySection> FlexibilitySections { get; set; }
		public DbSet<FlexibilityItem> FlexibilityItems { get; set; }
		public DbSet<WebSettings> WebSettings { get; set; }
		#endregion
	}
}
