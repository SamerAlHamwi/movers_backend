using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Mofleet.EntityFrameworkCore
{
    public static class MofleetDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MofleetDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MofleetDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
