using Academy.Application;
using AutoMapper;
using CoreLayer.Entities;
using Microsoft.Extensions.Configuration;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
	public class FlexibilityItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<FlexibilityItem, FlexibilityItemDto, string>
	{
		public string Resolve(FlexibilityItem source, FlexibilityItemDto destination, string destMember, ResolutionContext context)
		{
            return UploadcareUrlHelpers.ResolveUrl(source.IconUrl, configuration["BaseUrl"], isImage: true);
        }
    }
}
