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
            return UploadcareUrlHelpers.ResolveUrl(
                source.CourseImage,
                configuration["BaseUrl"],
                isImage: true,
                imageOperation: "/-/preview/"
            );
        }

    }
}
