using AutoMapper;
using CoreLayer.Entities;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<CompanyCourse, CompanyCourseDTO>().ReverseMap();
			CreateMap<Article, ArticleDTO>().ReverseMap();

			CreateMap<CourseEnrollmentDTO, CourseEnrollment>().ReverseMap();
			CreateMap<CompanyJopApplicationDTO, CompanyJopApplication>().ReverseMap();
			CreateMap<CompanyContactMessageDTO, CompanyContactMessage>().ReverseMap();

			CreateMap<SiteContactDto, SiteContact>()
					.ForMember(dest => dest.Type, opt => opt.MapFrom(src =>
					(ContactType)Enum.Parse(typeof(ContactType), src.Type.Trim(), true))).ReverseMap();

			CreateMap<MissionDto, Mission>().ReverseMap();

			CreateMap<AboutUs, AboutUsDto>().ReverseMap();

			CreateMap<Experience, ExperienceDto>().ReverseMap();

			CreateMap<ExperienceCategory, ExperienceCategoryDto>();
			CreateMap<CreateExperienceCategoryDto, ExperienceCategory>();

			CreateMap<ExperienceItem, ExperienceItemDto>();
			CreateMap<CreateExperienceItemDto, ExperienceItem>();

			CreateMap<Achievement, AchievementDto>().ReverseMap();

			CreateMap<FlexibilitySection, FlexibilitySectionDto>().ReverseMap();

			CreateMap<FlexibilityItem, FlexibilityItemDto>()
				.ForMember(d => d.IconUrl, o => o.MapFrom<FlexibilityItemPictureUrlResolver>()).ReverseMap();

			CreateMap<AboutUs, CreateAboutUsDto>().ReverseMap();

			CreateMap<CreateCompanyCourseDTO, CompanyCourse>();

			CreateMap<CreateCompanyJopApplicationDTO, CompanyJopApplication>().ReverseMap();

			CreateMap<CreateCompanyContactMessageDTO, CompanyContactMessage>().ReverseMap();

			CreateMap<CreateAchievementDTO, Achievement>().ReverseMap();

			CreateMap<CreateExperienceDto, Experience>().ReverseMap();

			CreateMap<CreateSiteContactDto, SiteContact>();

			CreateMap<WebSettings, WebSettingsDto>();

			CreateMap<CreateWebSettingsDto, WebSettings>();

			CreateMap<CreateMissionDto, Mission>();

			CreateMap<CreateFlexibilitySectionDto, FlexibilitySection>().ReverseMap();

			CreateMap<Article, ArticleDTO>()
			.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ArticlePictureUrlResolver>()).ReverseMap();
		}
	}
}
