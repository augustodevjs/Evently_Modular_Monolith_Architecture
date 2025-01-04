namespace Evently.API.Extensions;

/// <summary>
/// Provides extension methods for the IConfigurationBuilder interface.
/// </summary>
internal static class ConfigurationExtensions
{
    /// <summary>
    /// Adds module-specific configuration files to the IConfigurationBuilder.
    /// </summary>
    /// <param name="configurationBuilder">The IConfigurationBuilder instance to which the method is being added.</param>
    /// <param name="modules">An array of module names for which configuration files need to be added.</param>
    /// <remarks>
    /// This method iterates over each module name in the modules array and adds two JSON configuration files to the configurationBuilder:
    /// <list type="bullet">
    /// <item>
    /// <description>modules.{module}.json: The main configuration file for the module.</description>
    /// </item>
    /// <item>
    /// <description>modules.{module}.Development.json: The development-specific configuration file for the module, which is optional.</description>
    /// </item>
    /// </list>
    /// This allows the application to load configuration settings from JSON files specific to each module, making it easier to manage settings for different parts of the application.
    /// </remarks>
    internal static void AddModuleConfiguration(this IConfigurationBuilder configurationBuilder, string[] modules)
    {
        foreach (string module in modules)
        {
            configurationBuilder.AddJsonFile($"modules.{module}.json", false, true);
            configurationBuilder.AddJsonFile($"modules.{module}.Development.json", true, true);
        }
    }
}
