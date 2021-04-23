using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongoose.Api.Models.Media.Response;
using Mongoose.Api.Services.Contracts;
using Mongoose.Core.Entities;
using Mongoose.Core.Entities.BaseTypes;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController:Controller
    {
        private readonly IFilmRepository _filmRepository;
        private readonly ISeriesRepository _seriesRepository;
        private readonly IStaticFileService _staticFileService;
        private readonly IVideoInfoRepository _videoInfoRepository;
        private readonly IMapper _mapper;

        public MediaController(IFilmRepository filmRepository, 
            ISeriesRepository seriesRepository, 
            IStaticFileService staticFileService,
            IVideoInfoRepository videoInfoRepository,
            IMapper mapper)
        {
            _filmRepository = filmRepository;
            _seriesRepository = seriesRepository;
            _staticFileService = staticFileService;
            _videoInfoRepository = videoInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMappedMedia()
        {
            var media = new List<IMediaInfo>();
            media.AddRange(await _filmRepository.GetAll());
            media.AddRange(await _seriesRepository.GetAll());
            var response = _mapper.Map<List<IMediaInfo>, List<MediaInfoResponse>>(media);
            return Ok(response);
        }
        [HttpGet("unmapped")]
        public async Task<IActionResult> GetUnmappedMedia()
        {
            var entities = await _videoInfoRepository.GetWhere(v => v.EpisodeId == null && v.FilmId == null);
            return Ok(_mapper.Map<List<VideoInfo>, List<VideoInfoResponse>>(entities));
        }
    }
}