using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalizationService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public string GetCurrentLanguage()
            => _httpContextAccessor.HttpContext?.Items["CurrentLanguage"]?.ToString() ?? "ar";

        public string GetLocalizedTitle(LocalizableEntity entity)
        {
            if (entity is null) return string.Empty;
            var lang = GetCurrentLanguage()?.ToLower();

            return lang switch
            {
                "ar" => entity.TilteArabic ?? entity.TitleEnglish ?? string.Empty,
                "en" => entity.TitleEnglish ?? entity.TilteArabic ?? string.Empty,
                _ => entity.TilteArabic ?? entity.TitleEnglish ?? string.Empty
            };
        }

        public string GetLocalizedDescription(LocalizableEntity entity)
        {
            if (entity is null) return string.Empty;
            var lang = GetCurrentLanguage()?.ToLower();

            return lang switch
            {
                "ar" => entity.DescriptionAr ?? entity.DescriptionEn ?? string.Empty,
                "en" => entity.DescriptionEn ?? entity.DescriptionAr ?? string.Empty,
                _ => entity.DescriptionAr ?? entity.DescriptionEn ?? string.Empty
            };
        }

        public List<string> GetLocalizedList(List<string>? ar, List<string>? en)
        {
            var lang = GetCurrentLanguage()?.ToLower();

            return lang switch
            {
                "ar" => ar ?? en ?? new(),
                "en" => en ?? ar ?? new(),
                _ => ar ?? en ?? new()
            };
        }
    }
}
