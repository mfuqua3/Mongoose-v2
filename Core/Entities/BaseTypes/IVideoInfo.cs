namespace Mongoose.Core.Entities.BaseTypes
{
    public interface IVideoInfo
    {
        string FilePath { get; set; }
        long Duration { get; set; }
    }
}