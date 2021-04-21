using System;

namespace Mongoose.Core.Entities
{
    public interface IEntity<T> : IUnique<T>, ITracked where T : IEquatable<T>
    {
    }
}