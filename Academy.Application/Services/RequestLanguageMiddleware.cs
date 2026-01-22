using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services
{
    public class RequestLanguageMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLanguageMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            // Header مخصص
            var lang = context.Request.Headers["lang"].ToString();

            // أو Accept-Language
            if (string.IsNullOrWhiteSpace(lang))
            {
                var accept = context.Request.Headers["Accept-Language"].ToString();
                var first = accept.Split(',').FirstOrDefault()?.Trim() ?? "";
                lang = first.Length >= 2 ? first[..2] : "";
            }

            lang = (lang ?? "").Trim().ToLower();
            if (lang != "ar" && lang != "en") lang = "ar";

            context.Items["CurrentLanguage"] = lang;

            await _next(context);
        }
    }
}
