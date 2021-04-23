using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GoogleCast;
using GoogleCast.Channels;
using GoogleCast.Models.Media;
using Microsoft.Extensions.Logging;
using Mongoose.Common.Models;
using Mongoose.Common.Services.Contracts;

namespace Mongoose.Common.Services
{
    internal class CastService : ICastService
    {
        private readonly ILogger<CastService> _logger;
        private readonly IMapper _mapper;
        private ISender _sender;

        public CastService(IMapper mapper, ILogger<CastService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
        public bool IsConnected { get; private set; }

        public async Task<List<object>> GetStatus()
        {
            if(_sender == null)
                throw new InvalidOperationException("No active connection.");
            var status =  _sender.GetStatuses();
            return status.Values.ToList();
        }
        public async Task Connect(string receiverId)
        {
            var devices = await FindDevices();
            var receiver = devices.FirstOrDefault(d => d.Id == receiverId);
            if (receiver == null)
                throw new ArgumentException("The device is not available to cast.");
            _sender = new Sender();
            try
            {
                await _sender.ConnectAsync(receiver);
            }
            catch (Exception e)
            {
                _logger.LogError("Sender was not able to connect.", e);
                throw;
            }

            IsConnected = true;
        }

        public Task Disconnect()
        {
            if (_sender == null && !IsConnected)
                return Task.CompletedTask;
            _sender?.Disconnect();
            return Task.CompletedTask;
        }

        public async Task<List<CastReceiver>> GetReceivers()
        {
            var devices = await FindDevices();
            return _mapper.Map<IEnumerable<IReceiver>, List<CastReceiver>>(devices);
        }
        public async Task<MediaStatus> Load(string requestReceiverId, string contentId)
        {
            await Connect(requestReceiverId);
            var mediaChannel = _sender.GetChannel<IMediaChannel>();
            await _sender.LaunchAsync(mediaChannel);
            try
            {
                var mediaStatus = await mediaChannel.LoadAsync(
                    new MediaInformation
                    {
                        ContentId = contentId,
                        ContentType = "video/mp4"
                    });
                _logger.LogDebug("Content connected to chromecast.");
                return mediaStatus;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to load the requested media file.", e);
                throw;
            }
        }

        private async Task<IEnumerable<IReceiver>> FindDevices()
        {

            var deviceLocator = new DeviceLocator();
            return await deviceLocator.FindReceiversAsync();
        }
    }
}