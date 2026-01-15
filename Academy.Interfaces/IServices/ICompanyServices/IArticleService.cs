using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionLayer
{
	public interface IArticleService
	{
		Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync();
		Task<ArticleDTO?> GetArticleByIdAsync(int id);
		Task<ArticleDTO> CreateArticleAsync(string title, string description, string imageUrl);
		Task<bool> UpdateArticleAsync(int id, string title, string description, string? imageUrl);
		Task<bool> DeleteArticleAsync(int id);
	}
}
