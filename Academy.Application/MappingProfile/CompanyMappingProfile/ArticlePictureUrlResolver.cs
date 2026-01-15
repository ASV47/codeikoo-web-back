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
	public class ArticlePictureUrlResolver : IValueResolver<Article, ArticleDTO, string>
	{
		public string Resolve(Article source, ArticleDTO destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrEmpty(source.ImageUrl))
				return string.Empty;

			// لاحظ مفيش / بعد الـ Port لأن المسار المخزن بيبدأ بـ /
			return $"https://codeikoo.runasp.net/{source.ImageUrl}";
		}
	}
}
