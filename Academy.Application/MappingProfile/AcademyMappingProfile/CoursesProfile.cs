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
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<CreateCourseDto, Course>()
           .ForMember(d => d.CourseImage, opt => opt.Ignore())
           .ForMember(d => d.UserId, opt => opt.Ignore());



            // CourseUnit
            CreateMap<CreateCourseUnitDto, CourseUnit>()
					.ForMember(d => d.CourseId, opt => opt.MapFrom(s => s.CourseId))
					.ForMember(d => d.TilteArabic, opt => opt.MapFrom(s => s.TilteArabic))
					.ForMember(d => d.TitleEnglish, opt => opt.MapFrom(s => s.TitleEnglish));

           



            // UnitLesson
            CreateMap<CreateUnitLessonDto, UnitLesson>()
                    .ForMember(d => d.CourseUnitId, opt => opt.MapFrom(s => s.CourseUnitId))
                    .ForMember(d => d.TilteArabic, opt => opt.MapFrom(s => s.TilteArabic))
                    .ForMember(d => d.TitleEnglish, opt => opt.MapFrom(s => s.TitleEnglish));


            CreateMap<StudentCourseComment, StudentCourseCommentDto>();
            CreateMap<StudentCourseCommentCreateDto, StudentCourseComment>();
        }
    }
}
