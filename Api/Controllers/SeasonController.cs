using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mongoose.Api.Models.Season.Request;
using Mongoose.Api.Models.Season.Response;
using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mongoose.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IMapper _mapper;

        public SeasonController(ISeasonRepository seasonRepository, IMapper mapper)
        {
            _seasonRepository = seasonRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddSeason([FromBody] CreateSeasonRequest request)
        {
            var entity = _mapper.Map<CreateSeasonRequest, Season>(request);
            await _seasonRepository.Post(entity);
            await _seasonRepository.Save();
            var response = _mapper.Map<Season, SeasonInfoResponse>(entity);
            return Created("", response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSeason([FromBody] UpdateSeasonRequest request)
        {
            var entity = _mapper.Map<UpdateSeasonRequest, Season>(request);
            await _seasonRepository.Put(entity);
            await _seasonRepository.Save();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeasonsBySeries(int id, [FromQuery] bool detailed = false)
        {
            var entities = detailed
                ? await _seasonRepository.GetWhereFull(s => s.SeriesId == id)
                : await _seasonRepository.GetWhere(s => s.SeriesId == id);
            var response = _mapper.Map<List<Season>, List<SeasonDetailResponse>>(entities);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeason(int id)
        {
            var deleted = await _seasonRepository.Delete(id);
            if (deleted == null)
                return NotFound();
            await _seasonRepository.Save();
            return NoContent();
        }
    }
}
