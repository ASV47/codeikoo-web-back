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
	public class TechnologyProfile : Profile
	{
        public TechnologyProfile()
        {
			CreateMap<Technology, TechnologyDto>()
			.ForMember(d => d.TechnologyUrl, o => o.MapFrom<TechnologyPictureUrlResolver>());
		}
    }
}
