using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EFCore;

namespace DRIVERI_MANAGEMENT_PROJECT_BACKEND.ContextFactory
{
    public class RepositoryContextFactory
        : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            // configurationBuilder
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                project => project.MigrationsAssembly("DRIVERI_MANAGEMENT_PROJECT_BACKEND"));

            return new RepositoryContext(builder.Options);
        }
    }
}
