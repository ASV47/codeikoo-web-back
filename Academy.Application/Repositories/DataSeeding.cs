using Academy.Infrastructure.Data;
using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces;
using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Academy.Application.Repositories
{
    public class DataSeeding(ApplicationDbContext _dbContext) : IDataSeeding
    {
        public async Task SeedDataAsync()
        {
            if (!_dbContext.CompanyCourses.Any())
            {
                var CoursesData = File.ReadAllText("../Academy.Infrastructure/Data/DataSeed/course.json");
                var Courses = JsonSerializer.Deserialize<List<CompanyCourse>>(CoursesData);
                if (Courses?.Count > 0)
                {
                    await _dbContext.CompanyCourses.AddRangeAsync(Courses);
                    await _dbContext.SaveChangesAsync();
                }
            }

            //if (!_dbContext.CourseEnrollments.Any())
            //{
            //    var CourseEnrollmentsData = File.ReadAllText("../PersistanceLayer/Data/DataSeeding/courseEnrollement.json");
            //    var CourseEnrollments = JsonSerializer.Deserialize<List<CourseEnrollment>>(CourseEnrollmentsData);
            //    if (CourseEnrollments?.Count > 0)
            //    {
            //        await _dbContext.CourseEnrollments.AddRangeAsync(CourseEnrollments);
            //        await _dbContext.SaveChangesAsync();
            //    }
            //}

            if (!_dbContext.Articles.Any())
            {
                var ArticlesData = File.ReadAllText("../Academy.Infrastructure/Data/DataSeed/article.json");
                var Articles = JsonSerializer.Deserialize<List<Article>>(ArticlesData);
                if (Articles?.Count > 0)
                {
                    await _dbContext.Articles.AddRangeAsync(Articles);
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (!_dbContext.Services.Any())
            {
                var ServicesData = File.ReadAllText("../Academy.Infrastructure/Data/DataSeed/services.json");
                var Services = JsonSerializer.Deserialize<List<Service>>(ServicesData);
                if (Services?.Count > 0)
                {
                    await _dbContext.Services.AddRangeAsync(Services);
                    await _dbContext.SaveChangesAsync();
                }
            }

           

            if (!_dbContext.Clients.Any())
            {
                var ClientsData = File.ReadAllText("../Academy.Infrastructure/Data/DataSeed/Clients.json");
                var Clients = JsonSerializer.Deserialize<List<Client>>(ClientsData);
                if (Clients?.Count > 0)
                {
                    await _dbContext.Clients.AddRangeAsync(Clients);
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (!_dbContext.FlexibilityItems.Any())
            {
                var FlexibilityItemsData = File.ReadAllText("../Academy.Infrastructure/Data/DataSeed/FlexibilityItems.json");
                var FlexibilityItems = JsonSerializer.Deserialize<List<FlexibilityItem>>(FlexibilityItemsData);
                if (FlexibilityItems?.Count > 0)
                {
                    await _dbContext.FlexibilityItems.AddRangeAsync(FlexibilityItems);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
    }
