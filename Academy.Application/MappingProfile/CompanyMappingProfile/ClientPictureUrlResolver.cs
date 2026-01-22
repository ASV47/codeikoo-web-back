using Academy.Application;
using AutoMapper;
using AutoMapper.Execution;
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
	public class ClientPictureUrlResolver(IConfiguration configuration) : IValueResolver<Client, ClientDto, string>
	{
		public string Resolve(Client source, ClientDto destination, string destMember, ResolutionContext context)
		{
            return UploadcareUrlHelpers.ResolveUrl(source.LogoUrl, configuration["BaseUrl"], isImage: true);

        }
    }
}
