namespace Mongoose.Core.Entities.BaseTypes
{
    public interface IMediaInfo : INamed
    {
        string Description { get; set; }
        string IconPath { get; set; }
    }
}