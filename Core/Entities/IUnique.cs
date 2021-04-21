using System;

namespace Core.Entities
{
    public interface IUnique<T> where T : IEquatable<T>
    {
        T Id { get; set; }
    }
}