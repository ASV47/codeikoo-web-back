using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.DTOs.AcademyDTOs;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.MappingProfile.AcademyMappingProfile
{
    public class ImageSliderPanarUrlResolver(IConfiguration configuration) : IValueResolver<ImageSlider, ImageSliderDto, string>
    {
        public string Resolve(ImageSlider source, ImageSliderDto destination, string destMember, ResolutionContext context)
        {
            return UploadcareUrlHelpers.ResolveUrl(source.ImagePanar, configuration["BaseUrl"], isImage: true);
        }
    }
}
