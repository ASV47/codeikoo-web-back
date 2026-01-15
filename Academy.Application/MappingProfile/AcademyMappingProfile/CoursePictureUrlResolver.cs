using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.DTOs;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.MappingProfile.AcademyMappingProfile
{
    public class CoursePictureUrlResolver(IConfiguration configuration) : IValueResolver<Course, CourseDto, string>
    {
        public string Resolve(Course source, CourseDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.CourseImage))
                return string.Empty;

            var baseUrl = configuration["BaseUrl"]?.TrimEnd('/') ?? "https://localhost:7267";
            return $"{baseUrl}{source.CourseImage}";
        }
    }
}
