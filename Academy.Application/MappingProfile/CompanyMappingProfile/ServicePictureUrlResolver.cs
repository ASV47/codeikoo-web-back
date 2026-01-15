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
	//public class ServicePictureUrlResolver(IConfiguration configuration) : IValueResolver<Service, ServiceDto, string>
	//{
	//	public string Resolve(Service source, ServiceDto destination, string destMember, ResolutionContext context)
	//	{
	//		if (string.IsNullOrEmpty(source.IconUrl))
	//			return string.Empty;
	//		else
	//			return $"{"https://localhost:7048/"}{source.IconUrl}";
	//	}
	//}
}
