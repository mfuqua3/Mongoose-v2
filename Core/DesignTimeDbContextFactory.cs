using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mongoose.Core
{
    public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<MongooseContext>
    {
        public MongooseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MongooseContext>()
                .UseNpgsql("Host=localhost;Database=Mongoose;Username=postgres;password=Apples!Pandemic");
            return new MongooseContext(optionsBuilder.Options);
        }
    }
}