using BackendAssignment.Infrastructure.Data;

namespace BackendAssignment.FunctionalTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
  /// <summary>
  /// Overriding CreateHost to avoid creating a separate ServiceProvider per this thread:
  /// https://github.com/dotnet-architecture/eShopOnWeb/issues/465
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.UseEnvironment("Development");
    var host = builder.Build();
    host.Start();

    var serviceProvider = host.Services;

    using (var scope = serviceProvider.CreateScope())
    {
      var scopedServices = scope.ServiceProvider;
      var db = scopedServices.GetRequiredService<AppDbContext>();

      var logger = scopedServices
          .GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

      // recreate database
      db.Database.EnsureDeleted();
      db.Database.EnsureCreated();

      try
      {
        SeedData.PopulateTestDataAsync(db).Wait();
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Error occurs when seeding database. Error: {exceptionMessage}", ex.Message);
      }
    }

    return host;
  }
}
