using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using GoogleCast.Models.Media;
using Mongoose.Api.Models.Cast.Request;
using Mongoose.Common.Services.Contracts;
using Mongoose.Common.Utility;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;
        private readonly IVideoInfoRepository _videoInfoRepository;

        public CastController(ICastService castService, IVideoInfoRepository videoInfoRepository)
        {
            _castService = castService;
            _videoInfoRepository = videoInfoRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _castService.GetReceivers();
            return Ok(devices);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            return Ok(await _castService.GetStatus());
        }

        [HttpPost]
        public async Task<IActionResult> Load([FromBody] LoadRequest request)
        {
            var content = await _videoInfoRepository.Get(request.ContentId);
            if (content == null)
                return NotFound();
            var fullyQualifiedUri = Path.Combine($"http://{IpHelpers.GetLocal()}:5000", content.FilePath);
            MediaStatus status;
            try
            {
                status = await _castService.Load(request.ReceiverId, fullyQualifiedUri);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            if (status.Media?.Duration != null && content.Duration != (long) status.Media.Duration.Value)
            {
                content.Duration = (long) status.Media.Duration.Value;
                await _videoInfoRepository.Put(content);
                await _videoInfoRepository.Save();
            }

            return Created("", status);
        }
    }
}
