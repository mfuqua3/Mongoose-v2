using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mongoose.Api.Models.Series.Request;
using Mongoose.Api.Models.Series.Response;
using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IMapper _mapper;

        public SeriesController(ISeriesRepository seriesRepository, IMapper mapper)
        {
            _seriesRepository = seriesRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddSeries([FromBody] CreateSeriesRequest request)
        {
            var entity = _mapper.Map<CreateSeriesRequest, Series>(request);
            await _seriesRepository.Post(entity);
            await _seriesRepository.Save();
            var response = _mapper.Map<Series, SeriesInfoResponse>(entity);
            return Created("", response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSeries([FromBody] UpdateSeriesRequest request)
        {
            var entity = _mapper.Map<UpdateSeriesRequest, Series>(request);
            await _seriesRepository.Put(entity);
            await _seriesRepository.Save();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeries(int id, [FromQuery] bool detailed = false)
        {
            var entity = detailed ? await _seriesRepository.GetFull(id) : await _seriesRepository.Get(id);
            var response = _mapper.Map<Series, SeriesDetailResponse>(entity);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSeries()
        {
            var entities = await _seriesRepository.GetAll();
            var response = _mapper.Map<List<Series>, List<SeriesInfoResponse>>(entities);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            var deleted = await _seriesRepository.Delete(id);
            if (deleted == null)
                return NotFound();
            await _seriesRepository.Save();
            return NoContent();
        }
    }
}