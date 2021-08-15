using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.EntityFrameworkCore
{
    public static class CourseManagementSystemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CourseManagementSystemDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CourseManagementSystemDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
