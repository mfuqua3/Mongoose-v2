using System.Collections.Generic;
using Mongoose.Api.Models.Season.Response;

namespace Mongoose.Api.Models.Series.Response
{
    public class SeriesDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public List<SeasonDetailResponse> Seasons { get; set; }
    }
}