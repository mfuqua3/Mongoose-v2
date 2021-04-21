using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mongoose.Core.Entities;

namespace Mongoose.Core
{
    public class MongooseContext:IdentityDbContext<AppUser>
    {
        public MongooseContext(DbContextOptions<MongooseContext> options):base(options)
        {
            
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            var savedEntries = ChangeTracker.Entries();
            foreach (var entry in savedEntries)
            {
                if (!(entry.Entity is ITracked tracked))
                    continue;
                switch (entry.State)
                {
                    case EntityState.Added:
                        tracked.Created = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        tracked.Updated = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}