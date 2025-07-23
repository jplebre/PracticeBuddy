namespace PracticeBuddy.API.Infrastructure.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddConfigurationSettings(this IConfigurationBuilder builder)
    {
        builder.AddEnvironmentVariables();

        return builder;
    }
}