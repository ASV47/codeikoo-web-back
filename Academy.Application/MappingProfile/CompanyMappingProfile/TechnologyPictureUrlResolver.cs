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
	public class TechnologyPictureUrlResolver : IValueResolver<Technology, TechnologyDto, string>
	{
		public string Resolve(Technology source, TechnologyDto destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrEmpty(source.TechnologyUrl))
				return string.Empty;
			else
				return $"{"https://codeikoo.runasp.net/"}{source.TechnologyUrl}";
		}
	}
}
