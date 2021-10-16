using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace ReclameAquiDoTrader.UI.Extensions
{
    public static class FormFileExtensions
    {
        public static byte[] GetByteArray(this IFormFile formFile)
        {
            using var ms = new MemoryStream();
            formFile.CopyTo(ms);
            return ms.ToArray();
        }

        public static string GetExtension(this IFormFile formFile)
        {
            var filenameSplited = formFile.FileName.Split(".");
            var extension = filenameSplited[filenameSplited.Count() - 1];
            return string.IsNullOrEmpty(extension) ? string.Empty : extension.ToLower();
        }
    }
}
