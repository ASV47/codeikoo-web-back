using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application
{
    public class UploadcareUrlHelpers
    {
        public static bool IsAbsoluteHttpUrl(string value)
       => Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
          (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);

        /// <summary>
        /// For images: returns a display-friendly URL (preview/resize) to avoid browser download behavior.
        /// For files: returns the original URL unchanged.
        /// For relative paths: prefixes BaseUrl.
        /// </summary>
        public static string ResolveUrl(
            string? storedValue,
            string? baseUrl,
            bool isImage,
            string imageOperation = "/-/preview/",   // or "/-/resize/800x/"
            string defaultBaseUrl = "http://api.codeikoo.com")
        {
            var value = storedValue?.Trim();
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            // 1) Absolute URL (Uploadcare or any CDN)
            if (IsAbsoluteHttpUrl(value))
            {
                if (!isImage) return value;

                // Add operation only if it's not already present
                var normalized = value.TrimEnd('/');
                if (normalized.Contains("/-/", StringComparison.Ordinal))
                    return value; // already transformed

                return normalized + imageOperation;
            }

            // 2) Relative/local path -> prefix BaseUrl
            var b = (baseUrl?.TrimEnd('/') ?? defaultBaseUrl);
            var path = value.StartsWith("/") ? value : "/" + value;

            return b + path;
        }

        // Convenience wrappers (optional)
        public static string ResolveImage(string? storedValue, string? baseUrl)
            => ResolveUrl(storedValue, baseUrl, isImage: true, imageOperation: "/-/preview/");

        public static string ResolveFile(string? storedValue, string? baseUrl)
            => ResolveUrl(storedValue, baseUrl, isImage: false);
    }
}
