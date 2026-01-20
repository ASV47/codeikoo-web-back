using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Infrastructure.LangHelper;
using Academy.Interfaces.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.MappingProfile.AcademyMappingProfile
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
			CreateMap<CreateCourseDto, Course>()
					.ForMember(d => d.CourseImage, opt => opt.Ignore())
					.ForMember(d => d.UserId, opt => opt.Ignore())
					.ForMember(d => d.Title, opt => opt.MapFrom(s =>
						new LocalizedString { Ar = s.TitleAr, En = s.TitleEn }))
					.ForMember(d => d.Description, opt => opt.MapFrom(s =>
						new LocalizedString { Ar = s.DescriptionAr, En = s.DescriptionEn }))
					.ForMember(d => d.Features, opt => opt.MapFrom(s =>
						new LocalizedStringList
						{
							Ar = s.FeaturesAr ?? new List<string>(),
							En = s.FeaturesEn ?? new List<string>()
						}));

			CreateMap<Course, CourseDto>()
				.ForMember(d => d.Title, opt => opt.MapFrom((src, _, __, ctx) =>
				{
					var lang = ctx.Items.TryGetValue("lang", out var v) ? v?.ToString() : "en";
					return LangHelper.IsArabic(lang) ? src.Title.Ar : src.Title.En;
				}))
				.ForMember(d => d.Description, opt => opt.MapFrom((src, _, __, ctx) =>
				{
					var lang = ctx.Items.TryGetValue("lang", out var v) ? v?.ToString() : "en";
					return LangHelper.IsArabic(lang) ? src.Description.Ar : src.Description.En;
				}))
				.ForMember(d => d.Features, opt => opt.MapFrom((src, _, __, ctx) =>
				{
					var lang = ctx.Items.TryGetValue("lang", out var v) ? v?.ToString() : "en";
					return LangHelper.IsArabic(lang)
						? (src.Features.Ar ?? new List<string>())
						: (src.Features.En ?? new List<string>());
				}))
				.ForMember(d => d.CourseImageUrl, opt => opt.MapFrom<CoursePictureUrlResolver>());


			// CourseUnit
			CreateMap<CreateCourseUnitDto, CourseUnit>()
	.ForMember(d => d.Title, opt => opt.MapFrom(s =>
		new LocalizedString { Ar = s.TitleAr, En = s.TitleEn }))
	.ForMember(d => d.CourseId, opt => opt.MapFrom(s => s.CourseId));

			CreateMap<CourseUnit, CourseUnitDto>()
				.ForMember(d => d.Title, opt => opt.MapFrom((src, _, __, ctx) =>
					LangHelper.IsArabic(ctx.Items["lang"]?.ToString()) ? src.Title.Ar : src.Title.En));




			// UnitLesson
			CreateMap<CreateUnitLessonDto, UnitLesson>()
	.ForMember(d => d.Title, opt => opt.MapFrom(s =>
		new LocalizedString { Ar = s.TitleAr, En = s.TitleEn }));

			CreateMap<UnitLesson, UnitLessonDto>()
				.ForMember(d => d.Title, opt => opt.MapFrom((src, _, __, ctx) =>
					LangHelper.IsArabic(ctx.Items["lang"]?.ToString()) ? src.Title.Ar : src.Title.En));

		}
	}
}
