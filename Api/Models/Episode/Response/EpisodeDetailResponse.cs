namespace Mongoose.Api.Models.Episode.Response
{
    public class EpisodeDetailResponse
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public string FilePath { get; set; }
        public long Duration { get; set; }
        public int Number { get; set; }
    }
}