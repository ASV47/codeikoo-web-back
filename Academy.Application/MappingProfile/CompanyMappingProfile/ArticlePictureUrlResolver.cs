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
	public class ArticlePictureUrlResolver(IConfiguration configuration) : IValueResolver<Article, ArticleDTO, string>
	{
		public string Resolve(Article source, ArticleDTO destination, string destMember, ResolutionContext context)
		{
            return UploadcareUrlHelpers.ResolveUrl(source.ImageUrl, configuration["BaseUrl"], isImage: true);
        }
    }
}
