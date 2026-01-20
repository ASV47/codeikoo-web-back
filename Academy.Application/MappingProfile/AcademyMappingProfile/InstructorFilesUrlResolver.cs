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
    public class InstructorFilesUrlResolver(IConfiguration configuration) : IValueResolver<InstructorApplication, InstructorApplicationDto, string>
    {
        public string Resolve(InstructorApplication source, InstructorApplicationDto destination, string destMember, ResolutionContext context)
        {
           
            return UploadcareUrlHelpers.ResolveUrl(source.CvFilePath, configuration["BaseUrl"], isImage: true);

        }
    }
}
