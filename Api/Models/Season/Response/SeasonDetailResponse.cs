using System.Collections.Generic;
using Mongoose.Api.Models.Episode.Response;

namespace Mongoose.Api.Models.Season.Response
{
    public class SeasonDetailResponse
    {
        public int Id { get; set; }
        public int SeriesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int Number { get; set; }
        public List<EpisodeDetailResponse> Episodes { get; set; }
    }
}