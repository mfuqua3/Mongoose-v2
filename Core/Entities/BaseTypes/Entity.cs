using System;
using System.ComponentModel.DataAnnotations;

namespace Mongoose.Core.Entities.BaseTypes
{
    public abstract class Entity<T> : IEntity<T> where T : IEquatable<T>
    {
        [Key]
        public virtual T Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}