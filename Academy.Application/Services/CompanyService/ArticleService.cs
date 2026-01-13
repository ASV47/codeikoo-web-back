using AbstractionLayer;
using AutoMapper;
using CoreLayer.Entities;
using Academy.Interfaces.Interfaces;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class ArticleService(IUnitOfWork _unitOfWork, IMapper _mapper) : IArticleService
	{
		public async Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync()
		{
			var repo = _unitOfWork.GetRepository<Article, int>();
			var articles = await repo.GetAllAsync();
			return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
		}

		public async Task<ArticleDTO?> GetArticleByIdAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<Article, int>();
			var article = await repo.GetByIdAsync(id);
			return article == null ? null : _mapper.Map<ArticleDTO>(article);
		}

		public async Task<ArticleDTO> CreateArticleAsync(string title, string description, string imageUrl)
		{
			var article = new Article
			{
				Title = title,
				Description = description,
				ImageUrl = imageUrl 
			};

			var repo = _unitOfWork.GetRepository<Article, int>();
			await repo.AddAsync(article);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<ArticleDTO>(article);
		}

		//public async Task<bool> UpdateArticleAsync(int id, string title, string description, string? imageUrl)
		//{
		//	var repo = _unitOfWork.GetRepository<Article, int>();
		//	var article = await repo.GetByIdAsync(id);
		//	if (article == null) return false;

		//	article.Title = title;
		//	article.Description = description;
		//	if (!string.IsNullOrEmpty(imageUrl)) article.ImageUrl = imageUrl;

		//	repo.Update(article);
		//	return await _unitOfWork.SaveChangesAsync() > 0;
		//}

		public async Task<bool> UpdateArticleAsync(int id, string title, string description, string? imageUrl)
		{
			var repo = _unitOfWork.GetRepository<Article, int>();
			var article = await repo.GetByIdAsync(id);
			if (article == null) return false;

			article.Title = title;
			article.Description = description;

			// هذا السطر هو "الأمان": إذا كان imageUrl القادم NULL فلن يلمس القيمة القديمة في الداتابيز
			if (!string.IsNullOrEmpty(imageUrl))
			{
				article.ImageUrl = imageUrl;
			}

			repo.Update(article);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteArticleAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<Article, int>();
			var article = await repo.GetByIdAsync(id);
			if (article == null) return false;

			repo.Delete(article);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}
	}
}
