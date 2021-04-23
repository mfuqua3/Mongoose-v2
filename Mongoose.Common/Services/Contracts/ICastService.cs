using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleCast.Models.Media;
using Mongoose.Common.Models;

namespace Mongoose.Common.Services.Contracts
{
    public interface ICastService
    {
        bool IsConnected { get; }
        Task<List<CastReceiver>> GetReceivers();
        Task Connect(string receiverId);
        Task<MediaStatus> Load(string requestReceiverId, string contentId);

        Task Disconnect();
        Task<List<object>> GetStatus();
    }
}