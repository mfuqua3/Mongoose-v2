using System;
using Microsoft.AspNetCore.Identity;

namespace Mongoose.Core.Entities
{
    public class AppUser : IdentityUser, IEntity<string>
    {
        public override string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}