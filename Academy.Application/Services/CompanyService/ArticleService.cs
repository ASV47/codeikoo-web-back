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
using Microsoft.AspNetCore.Http;
using Academy.Interfaces.IServices.IAcademyServices;

namespace ServiceLayer
{
	public class ArticleService(IUnitOfWork _unitOfWork,
        IMapper _mapper, IFileStorageService fileStorage) : IArticleService
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

        public async Task<ArticleDTO> CreateArticleAsync(string title, string description, IFormFile? image)
        {
            string imageUrl = string.Empty;

            if (image is not null)
                imageUrl = await fileStorage.UploadAsync(image, "Articles");

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

        public async Task<bool> UpdateArticleAsync(int id, string title, string description, IFormFile? image)
        {
            var repo = _unitOfWork.GetRepository<Article, int>();
            var article = await repo.GetByIdAsync(id);
            if (article == null) return false;

            article.Title = title;
            article.Description = description;

            if (image is not null)
            {
                // احذف القديمة لو موجودة
                if (!string.IsNullOrWhiteSpace(article.ImageUrl))
                    await fileStorage.DeleteAsync(article.ImageUrl);

                // ارفع الجديدة
                article.ImageUrl = await fileStorage.UploadAsync(image, "Articles");
            }

            repo.Update(article);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteArticleAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Article, int>();
            var article = await repo.GetByIdAsync(id);
            if (article == null) return false;

            var imageUrl = article.ImageUrl; // خزّنها قبل الحذف

            repo.Delete(article);
            var saved = await _unitOfWork.SaveChangesAsync() > 0;

            if (saved && !string.IsNullOrWhiteSpace(imageUrl))
            {
                try
                {
                    await fileStorage.DeleteAsync(imageUrl);
                }
                catch
                {
                    // اختياري: log فقط، ما تكسرش العملية
                }
            }

            return saved;
        }


    }
}
