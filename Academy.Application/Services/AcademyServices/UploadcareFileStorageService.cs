using Academy.Interfaces.IServices.IAcademyServices;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Uploadcare;
using Uploadcare.Upload;

namespace Academy.Application.Services.AcademyServices
{
    public class UploadcareFileStorageService : IFileStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UploadcareClient _client;

        private readonly string _publicKey;
        private readonly string _secretKey;

        public UploadcareFileStorageService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;

            _publicKey = configuration["Uploadcare:PublicKey"] ?? throw new Exception("Uploadcare:PublicKey is missing");
            _secretKey = configuration["Uploadcare:SecretKey"] ?? throw new Exception("Uploadcare:SecretKey is missing");

            _client = new UploadcareClient(_publicKey, _secretKey);
        }

        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            var tempPath = Path.Combine(
                Path.GetTempPath(),
                $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"
            );

            await using (var fs = File.Create(tempPath))
                await file.CopyToAsync(fs);

            try
            {
                var uploader = new FileUploader(_client);
                var result = await uploader.Upload(new FileInfo(tempPath));

                var uuid = ExtractUuid(result);
                if (string.IsNullOrWhiteSpace(uuid))
                    throw new Exception("Uploadcare upload succeeded but UUID could not be extracted.");

                var cdnBase = _configuration["Uploadcare:CdnBase"]?.TrimEnd('/');
                if (string.IsNullOrWhiteSpace(cdnBase))
                    throw new Exception("Uploadcare:CdnBase is missing. Put it in appsettings from Dashboard > Delivery.");

                // ✅ ده اللينك اللي هيتخزن في DB
                return $"{cdnBase}/{uuid}/";
            }
            finally
            {
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }

        public async Task DeleteAsync(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl)) return;

            // https://4r7e6wkrbx.ucarecd.net/{uuid}/...
            var uri = new Uri(fileUrl);
            var uuid = uri.AbsolutePath.Trim('/').Split('/')[0];

            if (string.IsNullOrWhiteSpace(uuid)) return;

            var http = _httpClientFactory.CreateClient();

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.uploadcare-v0.7+json")
            );

            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Uploadcare.Simple", $"{_publicKey}:{_secretKey}");

            // ✅ DELETE /files/{uuid}/storage/
            var res = await http.DeleteAsync($"https://api.uploadcare.com/files/{uuid}/storage/");

            if (!res.IsSuccessStatusCode)
            {
                var body = await res.Content.ReadAsStringAsync();
                throw new Exception($"Uploadcare delete failed ({(int)res.StatusCode}): {body}");
            }
        }

        private static string? ExtractUuid(object result)
        {
            var t = result.GetType();
            string? TryGet(string name) => t.GetProperty(name)?.GetValue(result)?.ToString();

            return TryGet("FileId") ?? TryGet("Uuid") ?? TryGet("uuid") ?? TryGet("file_id");
        }
    }
}
