using AbstractionLayer;
using Academy.Application;
using Academy.Interfaces.IServices;
using Academy.Web.Controllers;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Helpers;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
	[ApiExplorerSettings(GroupName = "Company")]
	public class ArticleController(IServiceManager _serviceManager, IWebHostEnvironment env) : APIBaseController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetAll()
		=> Ok(await _serviceManager.ArticleService.GetAllArticlesAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<ArticleDTO>> GetById(int id)
		{
			var article = await _serviceManager.ArticleService.GetArticleByIdAsync(id);
			return article == null ? NotFound() : Ok(article);
		}

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ArticleDTO>> Create([FromForm] CreateArticleDTO dto)
        {
            var result = await _serviceManager.ArticleService
                .CreateArticleAsync(dto.Title, dto.Description, dto.Image);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Update(int id, [FromForm] CreateArticleDTO dto)
        {
            var result = await _serviceManager.ArticleService
                .UpdateArticleAsync(id, dto.Title, dto.Description, dto.Image);

            return result ? Ok(new { Message = "Updated Successfully" }) : NotFound(new { Message = "Article not found" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _serviceManager.ArticleService.DeleteArticleAsync(id);
            return ok ? NoContent() : NotFound();
        }


        //[HttpPost]
        //public async Task<ActionResult<ArticleDTO>> Create([FromForm] CreateArticleDTO dto)
        //      {
        //          var imageUrl = DocumentSettings.UploadFile(dto.Image, "Articles", env); 
        //          var result = await _serviceManager.ArticleService.CreateArticleAsync(dto.Title, dto.Description, imageUrl);

        //	return Ok(result);
        //      }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, [FromForm] CreateArticleDTO dto)
        //{
        //	// 1. جلب المقال الحالي للتأكد من وجوده والحصول على رابط الصورة القديم
        //	var article = await _serviceManager.ArticleService.GetArticleByIdAsync(id);
        //	if (article == null) return NotFound(new { Message = "Article not found" });

        //	string finalPath;

        //	// 2. التحقق هل المستخدم رفع صورة جديدة؟
        //	if (dto.Image != null)
        //	{
        //		// حذف الصورة القديمة من الهارد (يجب تنظيف الـ URL من الـ Domain أولاً)
        //		var oldRelativePath = article.ImageUrl.Replace("https://localhost:7048", "");
        //		DocumentSettings.DeleteFile(oldRelativePath);

        //		// رفع الصورة الجديدة
        //		finalPath = DocumentSettings.UploadFile(dto.Image, "Articles", env);
        //	}
        //	else
        //	{
        //		// إذا لم يرفع صورة، نستخدم المسار القديم المخزن في الداتابيز (بدون الـ Domain)
        //		finalPath = article.ImageUrl.Replace("https://localhost:7048", "");
        //	}

        //	// 3. استدعاء السيرفس (التي تتأكد بدورها أن القيمة ليست Null أو Empty قبل التحديث)
        //	var result = await _serviceManager.ArticleService.UpdateArticleAsync(id, dto.Title, dto.Description, finalPath);

        //	return result ? Ok(new { Message = "Updated Successfully" }) : BadRequest();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //	// 1. جلب المقال أولاً لحذف الصورة من الهارد
        //	var article = await _serviceManager.ArticleService.GetArticleByIdAsync(id);
        //	if (article == null) return NotFound();

        //	// 2. حذف الصورة
        //	var relativePath = article.ImageUrl.Replace("https://localhost:7048/", "");
        //	DocumentSettings.DeleteFile(relativePath);

        //	// 3. حذف السجل
        //	await _serviceManager.ArticleService.DeleteArticleAsync(id);
        //	return NoContent();
        //}
    }
}
