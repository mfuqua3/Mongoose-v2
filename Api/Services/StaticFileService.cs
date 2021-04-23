using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Mongoose.Api.Models.Media.Response;
using Mongoose.Api.Services.Contracts;

namespace Mongoose.Api.Services
{
    public class StaticFileService : IStaticFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StaticFileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public List<VideoInfoResponse> GetAllVideoFiles()
        {
            var directory = Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "videos"));
            var files = directory.GetFiles("*.m4v", SearchOption.AllDirectories);
            return files.Select(fi=>new VideoInfoResponse
            {
                Path = Path.GetRelativePath(_webHostEnvironment.WebRootPath, fi.FullName),
                Name = fi.Name
            }).ToList();
        }
    }
}