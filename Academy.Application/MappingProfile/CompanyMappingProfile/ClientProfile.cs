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
	public class ClientProfile : Profile
	{
        public ClientProfile()
        {
			CreateMap<Client, ClientDto>()
				.ForMember(d => d.LogoUrl, o => o.MapFrom<ClientPictureUrlResolver>()).ReverseMap();
		}
    }
}
