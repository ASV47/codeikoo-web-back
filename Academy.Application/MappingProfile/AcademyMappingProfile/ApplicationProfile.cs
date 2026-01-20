using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.LangHelper;
using Academy.Interfaces.DTOs;
using Academy.Interfaces.DTOs.AcademyDTOs;
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

            CreateMap<ImageSlider, ImageSliderDto>()
            .ForMember(d => d.ImageUrl, opt => opt.MapFrom<ImageSliderPictureUrlResolver>());

            // Create DTO -> Entity (مش هنماب Image هنا لأن الرفع هيتم في السيرفس)
            CreateMap<CreateImageSliderDto, ImageSlider>()
                .ForMember(d => d.ImageUrl, opt => opt.Ignore());

            CreateMap<CreateJobDto, Job>()
		 .ForMember(d => d.Title, opt => opt.MapFrom(s => new LocalizedString { Ar = s.TitleAr, En = s.TitleEn }))
		 .ForMember(d => d.Description, opt => opt.MapFrom(s => new LocalizedString { Ar = s.DescriptionAr, En = s.DescriptionEn }))
		 .ForMember(d => d.Location, opt => opt.MapFrom(s => new LocalizedString { Ar = s.LocationAr, En = s.LocationEn }))
		 .ForMember(d => d.Requirements, opt => opt.MapFrom(s => new LocalizedStringList { Ar = s.RequirementsAr, En = s.RequirementsEn }))
		 .ForMember(d => d.PostedAt, opt => opt.MapFrom(s => s.PostedAt ?? DateTime.UtcNow));

			
			CreateMap<Job, JobDto>()
	.ForMember(d => d.Title, opt => opt.MapFrom((src, _, __, ctx) =>
		LangHelper.IsArabic(ctx.Items["lang"]?.ToString()) ? src.Title.Ar : src.Title.En))
	.ForMember(d => d.Description, opt => opt.MapFrom((src, _, __, ctx) =>
		LangHelper.IsArabic(ctx.Items["lang"]?.ToString()) ? src.Description.Ar : src.Description.En))
	.ForMember(d => d.Location, opt => opt.MapFrom((src, _, __, ctx) =>
		LangHelper.IsArabic(ctx.Items["lang"]?.ToString()) ? src.Location.Ar : src.Location.En))
	.ForMember(d => d.Requirements, opt => opt.MapFrom((src, _, __, ctx) =>
		LangHelper.IsArabic(ctx.Items["lang"]?.ToString()) ? src.Requirements.Ar : src.Requirements.En));


			CreateMap<JobApplication, JobApplicationDto>()
                    .ForMember(dest => dest.CvFilePath, opt => opt.MapFrom<JobApplicationFilesUrlResolver>());

            CreateMap<CreateJobApplicationDto, JobApplication>()
                .ForMember(dest => dest.CvFilePath, opt => opt.Ignore());
        }
    }
}
