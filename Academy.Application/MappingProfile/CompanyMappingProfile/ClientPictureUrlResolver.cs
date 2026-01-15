using AutoMapper;
using AutoMapper.Execution;
using CoreLayer.Entities;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
	public class ClientPictureUrlResolver : IValueResolver<Client, ClientDto, string>
	{
		public string Resolve(Client source, ClientDto destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrEmpty(source.LogoUrl))
				return string.Empty;
			else
				return $"{"https://codeikoo.runasp.net/"}{source.LogoUrl}";
		}
	}
}
