using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.LangHelper;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.DTOs.AcademyDTOs;
using Academy.Interfaces.Interfaces;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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


            CreateMap<ImageSlider, ImageSliderDto>()
                     .ForMember(d => d.ImageUrl, opt => opt.MapFrom<ImageSliderPictureUrlResolver>())
                     .ForMember(d => d.ImagePanarUrl, opt => opt.MapFrom<ImageSliderPanarUrlResolver>())
                     .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));

            // Create DTO -> Entity (مش هنماب الصور لأن الرفع هيتم في السيرفس)
            CreateMap<CreateImageSliderDto, ImageSlider>()
                .ForMember(d => d.ImageUrl, opt => opt.Ignore())
                .ForMember(d => d.ImagePanar, opt => opt.Ignore())
                .ForMember(d => d.Email, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.Email) ? null : s.Email.Trim()));



            CreateMap<CreateJobDto, Job>()
            .ForMember(d => d.PostedAt, opt => opt.Ignore());

           
            CreateMap<JobApplication, JobApplicationDto>()
                    .ForMember(dest => dest.CvFilePath, opt => opt.MapFrom<JobApplicationFilesUrlResolver>());

            CreateMap<CreateJobApplicationDto, JobApplication>()
                .ForMember(dest => dest.CvFilePath, opt => opt.Ignore());
        }
    }
}
