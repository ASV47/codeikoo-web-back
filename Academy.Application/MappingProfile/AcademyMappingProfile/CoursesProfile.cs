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
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            //CreateMap<CreateCourseDto, Course>()
            //.ForMember(d => d.CourseImage, opt => opt.Ignore())
            //.ForMember(d => d.UserId, opt => opt.Ignore());

            //CreateMap<Course, CourseDto>()
            //	.ForMember(d => d.CourseImageUrl, opt => opt.MapFrom<CoursePictureUrlResolver>());

            CreateMap<CreateCourseDto, Course>()
                    .ForMember(d => d.CourseImage, opt => opt.Ignore())
                    .ForMember(d => d.UserId, opt => opt.Ignore())
                    .ForMember(d => d.Features, opt => opt.MapFrom(s => s.Features ?? new List<string>()));

            CreateMap<Course, CourseDto>()
                    .ForMember(d => d.CourseImageUrl, opt => opt.MapFrom<CoursePictureUrlResolver>())
                    .ForMember(d => d.Features, opt => opt.MapFrom(s => s.Features ?? new List<string>()));


            // CourseUnit
            CreateMap<CreateCourseUnitDto, CourseUnit>()
                    .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                    .ForMember(d => d.CourseId, opt => opt.MapFrom(s => s.CourseId));

            CreateMap<CourseUnit, CourseUnitDto>();



            // UnitLesson
            CreateMap<CreateUnitLessonDto, UnitLesson>();
            CreateMap<UnitLesson, UnitLessonDto>();
        }
    }
}
