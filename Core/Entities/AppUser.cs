using System;
using Microsoft.AspNetCore.Identity;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core.Entities
{
    public class AppUser : IdentityUser, IEntity<string>
    {
        public override string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}