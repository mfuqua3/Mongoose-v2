using System;

namespace Mongoose.Core.Entities
{
    public interface ITracked
    {
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
    }
}