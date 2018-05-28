using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillsCub.Core.Services;

namespace SkillsCub.MVC.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IStorageClient _client;
        private readonly IContentTypeResolver _contentTypeResolver;

        public FileController(IStorageClient client, IContentTypeResolver contentTypeResolver)
        {
            _client = client;
            _contentTypeResolver = contentTypeResolver;
        }

        public async Task<IActionResult> Download(string filename, Guid exerciseId)
        {
            var ext = $".{filename.Split('.').LastOrDefault()}";
            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            return File(await _client.DownloadFileFromNodeAsync(filename, $"exercise_{exerciseId:N}"), _contentTypeResolver.GetContentType(ext));
        }

        [HttpPost]
        public async Task<JsonResult> Remove(string filename, Guid exerciseId)
        {
            return Json(await _client.RemoveNodeAsync(filename, $"exercise_{exerciseId:N}"));
        }
    }
}