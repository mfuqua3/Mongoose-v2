using AutoMapper;
using Mongoose.Api.Models.Episode.Request;
using Mongoose.Api.Models.Episode.Response;
using Mongoose.Api.Models.Media.Response;
using Mongoose.Api.Models.Season.Request;
using Mongoose.Api.Models.Season.Response;
using Mongoose.Api.Models.Series.Request;
using Mongoose.Api.Models.Series.Response;
using Mongoose.Core.Entities;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Api.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<IMediaInfo, MediaInfoResponse>();

            CreateMap<VideoInfo, VideoInfoResponse>()
                .ForMember(dto=>dto.Path, o=>o.MapFrom(e=>e.FilePath));

            CreateMap<CreateSeriesRequest, Core.Entities.Series>();
            CreateMap<UpdateSeriesRequest, Core.Entities.Series>();
            CreateMap<Core.Entities.Series, SeriesInfoResponse>();
            CreateMap<Core.Entities.Series, SeriesDetailResponse>();

            CreateMap<Core.Entities.Season, SeasonDetailResponse>();
            CreateMap<Core.Entities.Season, SeasonInfoResponse>();
            CreateMap<CreateSeasonRequest, Core.Entities.Season>();
            CreateMap<UpdateSeasonRequest, Core.Entities.Season>();

            CreateMap<Core.Entities.Episode, EpisodeDetailResponse>()
                .ForMember(dto=>dto.FilePath, o=>o.MapFrom(e=>e.VideoInfo.FilePath))
                .ForMember(dto => dto.ContentId, o => o.MapFrom(e => e.VideoInfo.Id))
                .ForMember(dto => dto.Duration, o => o.MapFrom(e => e.VideoInfo.Duration));
            CreateMap<CreateEpisodeRequest, Core.Entities.Episode>();
            CreateMap<UpdateEpisodeRequest, Core.Entities.Episode>();
        }
    }
}