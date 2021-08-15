using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using CourseManagementSystem.Configuration;
using CourseManagementSystem.Web;

namespace CourseManagementSystem.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class CourseManagementSystemDbContextFactory : IDesignTimeDbContextFactory<CourseManagementSystemDbContext>
    {
        public CourseManagementSystemDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CourseManagementSystemDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            CourseManagementSystemDbContextConfigurer.Configure(builder, configuration.GetConnectionString(CourseManagementSystemConsts.ConnectionStringName));

            return new CourseManagementSystemDbContext(builder.Options);
        }
    }
}
