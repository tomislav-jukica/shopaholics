using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Shopaholics.Infrastructure.Persistance
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            string projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\Shopaholics.Server");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();            
            optionsBuilder.UseSqlServer(connectionString);


            return new UserDbContext(optionsBuilder.Options);
        }
    }
}
