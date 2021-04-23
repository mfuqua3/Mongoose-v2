using Microsoft.Extensions.DependencyInjection;
using Mongoose.Core.Repository;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFilmAnthologyRepository, FilmAnthologyRepository>()
                .AddScoped<IFilmRepository, FilmRepository>()
                .AddScoped<ISeriesRepository, SeriesRepository>()
                .AddScoped<ISeasonRepository, SeasonRepository>()
                .AddScoped<IEpisodeRepository, EpisodeRepository>()
                .AddScoped<IVideoInfoRepository, VideoInfoRepository>();
            return services;
        }
    }
}