using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mongoose.Core.Entities;
using Mongoose.Core.Entities.BaseTypes;

namespace Mongoose.Core
{
    public class MongooseContext:IdentityDbContext<AppUser>
    {
        public DbSet<Series> Series { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<FilmAnthology> FilmAnthologies { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<VideoInfo> VideoInfo { get; set; }
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FilmAnthology>()
                .HasMany<Film>()
                .WithOne(f => f.Anthology)
                .HasForeignKey(f => f.AnthologyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Episode>()
                .HasOne(e => e.VideoInfo)
                .WithOne()
                .HasForeignKey<Episode>(e => e.VideoInfoId);
            builder.Entity<Film>()
                .HasOne(e => e.VideoInfo)
                .WithOne()
                .HasForeignKey<Film>(e => e.VideoInfoId);
            base.OnModelCreating(builder);
        }
    }
}