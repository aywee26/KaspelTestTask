using Microsoft.EntityFrameworkCore.Design;

namespace KaspelTestTask.Infrastructure;

internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var creator = new AppDbContextCreator<AppDbContext>(
            builder =>
            {
                builder.MigrationsAssembly(typeof(AppDbContextFactory).Assembly.GetName().Name);
            },
            options =>
            {
                var instance = new AppDbContext(options);
                return instance;
            });

        var instance = creator.CreateDbContext(args);
        return instance;
    }
}
