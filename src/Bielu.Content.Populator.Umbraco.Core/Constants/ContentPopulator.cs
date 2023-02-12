namespace Bielu.Content.Populator.Umbraco.Constants;

public class ContentPopulator
{
    public const string Name = "ContentPopulator";
    public class Trees
    {
        public const string ContentPopulator = "contentPopulator";
        public const string Group = "sync";
    }

    public class Package
    {
        public static string Name { get; set; } ="ContentPopulator";
        public static string PluginPath { get; set; } = "/App_Plugins/contentPopulator";
    }
}