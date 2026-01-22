using Microsoft.AspNetCore.Hosting;
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

        private static readonly HashSet<string> ImageFolders =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "Articles",
            "ClientsLogo",
            "Logo",
            "Flexibility",
            "CourseImages",
            "InstructorCVs",
            "JobApplicationCVs"
            // زوّد هنا أي فولدر صور عندك
        };
        //      public static string UploadFile(IFormFile file, string folderName)
        //{
        //          var rootFolder = ImageFolders.Contains(folderName) ? "Images" : "Files";

        //          var folderPath = Path.Combine(
        //              Directory.GetCurrentDirectory(),
        //              "wwwroot",
        //              rootFolder,
        //              folderName
        //          );

        //          // ✅ يمنع مشكلة "Could not find a part of the path"
        //          Directory.CreateDirectory(folderPath);

        //          var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        //          var filePath = Path.Combine(folderPath, fileName);

        //          using var fileStream = new FileStream(filePath, FileMode.Create);
        //          file.CopyTo(fileStream);

        //          return $"/{rootFolder}/{folderName}/{fileName}";
        //      }

        public static string UploadFile(IFormFile file, string folderName, IWebHostEnvironment env)
        {
            var rootFolder = "Files"; // أو حسب النوع
            var folderPath = Path.Combine(env.WebRootPath, rootFolder, folderName);

            Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            return $"/{rootFolder}/{folderName}/{fileName}";
        }

        public static void DeleteFile(string fileUrl)
		{
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileUrl.TrimStart('/'));
            //if (File.Exists(filePath)) File.Delete(filePath);
            if (string.IsNullOrWhiteSpace(fileUrl)) return;

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                fileUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar)
            );

            if (File.Exists(filePath)) File.Delete(filePath);
        }
	}
}
