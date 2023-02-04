using Microsoft.Extensions.DependencyInjection;

namespace Bielu.Content.Populator;

public static class HostConfigurationExtension
{
    public static IServiceCollection AddContentPopulator(this IServiceCollection services,
        Action<PopulatorConfiguration> conf = null)
    {
        var configuration = PopulatorConfiguration.CurrentInstance;
        conf.Invoke(configuration);
        services.AddSingleton<PopulatorConfiguration>(configuration);
        return services;
    }
}