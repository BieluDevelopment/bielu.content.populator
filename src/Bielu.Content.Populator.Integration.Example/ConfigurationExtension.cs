namespace Bielu.Content.Populator.Integration.Example;

public static class ConfigurationExtension
{
    public static void AddExample(this PopulatorConfiguration configuration)
    {
        configuration.EmbedAssemblies.Add(typeof(ConfigurationExtension).Assembly, new List<string>()
        {
            "Content.content-1.0.0.json"
        });
    }
}