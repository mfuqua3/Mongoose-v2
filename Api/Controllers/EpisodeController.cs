using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mongoose.Api.Models.Episode.Request;
using Mongoose.Api.Models.Episode.Response;
using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IMapper _mapper;

        public EpisodeController(IEpisodeRepository episodeRepository, IMapper mapper)
        {
            _episodeRepository = episodeRepository;
            _mapper = mapper;
        }
        [HttpGet("{seasonId}")]
        public async Task<IActionResult> GetEpisodesBySeason(int seasonId)
        {
            var entities = await _episodeRepository.GetWhere(e => e.SeasonId == seasonId);
            var response = _mapper.Map<List<Episode>, List<EpisodeDetailResponse>>(entities);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEpisode([FromBody] CreateEpisodeRequest request)
        {
            var entity = _mapper.Map<CreateEpisodeRequest, Episode>(request);
            await _episodeRepository.Post(entity);
            await _episodeRepository.Save();
            var response = _mapper.Map<Episode, EpisodeDetailResponse>(await _episodeRepository.Get(entity.Id));
            return Created("", response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEpisode([FromBody] UpdateEpisodeRequest request)
        {
            var entity = _mapper.Map<UpdateEpisodeRequest, Episode>(request);
            await _episodeRepository.Put(entity);
            await _episodeRepository.Save();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisode(int id)
        {
            var deleted = await _episodeRepository.Delete(id);
            if (deleted == null)
                NotFound();
            await _episodeRepository.Save();
            return NoContent();
        }
    }
}
