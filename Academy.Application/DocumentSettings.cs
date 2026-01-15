using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application
{
	public static class DocumentSettings
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			var filePath = Path.Combine(folderPath, fileName);

			using var fileStream = new FileStream(filePath, FileMode.Create);
			file.CopyTo(fileStream);

			return $"/Files/{folderName}/{fileName}";
		}

		public static void DeleteFile(string fileUrl)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileUrl.TrimStart('/'));
			if (File.Exists(filePath)) File.Delete(filePath);
		}
	}
}
