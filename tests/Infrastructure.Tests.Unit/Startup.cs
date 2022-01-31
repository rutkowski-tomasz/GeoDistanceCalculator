using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests.Unit;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructure();
    }
}