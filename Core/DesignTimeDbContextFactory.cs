using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core
{
    public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<DbContext>
    {
        public DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>()
                .UseNpgsql("Host=localhost;Database=BoilerplateTest;Username=postgres;password=Apples!Pandemic");
            return new DbContext(optionsBuilder.Options);
        }
    }
}