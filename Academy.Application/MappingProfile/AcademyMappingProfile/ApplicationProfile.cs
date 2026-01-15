using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.MappingProfile.AcademyMappingProfile
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ContactMessage, ContactMessageDto>();
            CreateMap<CreateContactMessageDto, ContactMessage>();


            CreateMap<InstructorApplication, InstructorApplicationDto>()
                .ForMember(dest => dest.CvFilePath, opt => opt.MapFrom<InstructorFilesUrlResolver>());

            CreateMap<CreateInstructorApplicationDto, InstructorApplication>()
                .ForMember(dest => dest.CvFilePath, opt => opt.Ignore());

            CreateMap<Job, JobDto>()
                    .ForMember(d => d.Requirements, opt => opt.MapFrom(s => s.Requirements ?? new List<string>()));

            CreateMap<CreateJobDto, Job>()
                .ForMember(dest => dest.PostedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(d => d.Requirements, opt => opt.MapFrom(s => s.Requirements ?? new List<string>()));


            CreateMap<JobApplication, JobApplicationDto>()
                    .ForMember(dest => dest.CvFilePath, opt => opt.MapFrom<JobApplicationFilesUrlResolver>());

            CreateMap<CreateJobApplicationDto, JobApplication>()
                .ForMember(dest => dest.CvFilePath, opt => opt.Ignore());
        }
    }
}
