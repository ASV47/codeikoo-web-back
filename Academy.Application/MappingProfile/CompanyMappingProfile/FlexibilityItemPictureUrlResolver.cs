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
	public class FlexibilityItemPictureUrlResolver : IValueResolver<FlexibilityItem, FlexibilityItemDto, string>
	{
		public string Resolve(FlexibilityItem source, FlexibilityItemDto destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrEmpty(source.IconUrl))
				return string.Empty;

			return $"https://codeikoo.runasp.net/{source.IconUrl}";
		}
	}
}
