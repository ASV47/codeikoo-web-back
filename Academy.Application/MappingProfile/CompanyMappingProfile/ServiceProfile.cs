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
	public class ServiceProfile : Profile
	{
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDto>();

        }
    }
}
