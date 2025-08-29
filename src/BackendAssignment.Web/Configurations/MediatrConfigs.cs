using Ardalis.SharedKernel;
using BackendAssignment.UseCases.Orders.Create;
using System.Reflection;

namespace BackendAssignment.Web.Configurations;

public static class MediatrConfigs
{
  public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
  {
    var mediatRAssemblies = new[]
      {
        Assembly.GetAssembly(typeof(Core.OrdersAggregate.Order)), // Core
        Assembly.GetAssembly(typeof(CreateOrderCommand)) // UseCases
      };

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

    return services;
  }
}
