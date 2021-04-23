using System.Collections.Generic;
using System.IO;
using Mongoose.Api.Models.Media.Response;

namespace Mongoose.Api.Services.Contracts
{
    public interface IStaticFileService
    {
        List<VideoInfoResponse> GetAllVideoFiles();
    }
}