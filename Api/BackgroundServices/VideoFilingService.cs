using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mongoose.Api.Services.Contracts;
using Mongoose.Core.Entities;
using Mongoose.Core.Repository.BaseTypes;

namespace Mongoose.Api.BackgroundServices
{
    public class VideoFilingService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<VideoFilingService> _logger;
        private readonly IStaticFileService _staticFileService;
        private HashSet<string> _indexedVideos = new HashSet<string>();

        public VideoFilingService(IServiceScopeFactory scopeFactory, ILogger<VideoFilingService> logger, IStaticFileService staticFileService)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _staticFileService = staticFileService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var videosToIndex = _staticFileService.GetAllVideoFiles()
                    .Where(v => !_indexedVideos.Contains(v.Path)).ToList();
                if (!videosToIndex.Any())
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                    continue;
                }
                _logger.LogInformation($"{videosToIndex.Count()} new videos found in static file directory. Checking against database.");
                using var scope = _scopeFactory.CreateScope();
                var videoRepo = scope.ServiceProvider.GetService<IVideoInfoRepository>();
                var newVideos = 0;
                foreach (var video in videosToIndex)
                {
                    _indexedVideos.Add(video.Path);
                    if (await videoRepo.Contains(v => v.FilePath == video.Path))
                        continue;
                    var videoInfo = new VideoInfo
                    {
                        FilePath = video.Path,
                        Name = video.Name
                    };
                    await videoRepo.Post(videoInfo);
                    newVideos++;
                }
                if (newVideos > 0)
                {
                    await videoRepo.Save();
                }
                _logger.LogInformation($"Video indexing complete. {newVideos} total new videos added to the database.");
            }
        }
    }
}