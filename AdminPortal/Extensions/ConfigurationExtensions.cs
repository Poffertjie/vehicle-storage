namespace AdminPortal.Extensions;

public static class ConfigurationExtensions
{
    public static void SetupConfig(this IServiceCollection services, IConfiguration configuration)
    {
        Config.RawConfig rawConfig = new Config.RawConfig()
        {
            API = configuration.GetSection(nameof(rawConfig.API)).Value,
        };
        Config.SetRawConfigInstance(rawConfig);
    }
    

}