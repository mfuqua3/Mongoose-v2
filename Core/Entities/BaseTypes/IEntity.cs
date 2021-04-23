using System;

namespace Mongoose.Core.Entities.BaseTypes
{
    public interface IEntity<T> : IUnique<T>, ITracked where T : IEquatable<T>
    {
    }
}