using System;

namespace Mongoose.Core.Entities.BaseTypes
{
    public interface ITracked
    {
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
    }
}