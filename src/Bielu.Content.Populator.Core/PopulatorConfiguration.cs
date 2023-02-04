
using System.Reflection;
using Bielu.Content.Populator.Models;
using Newtonsoft.Json;

namespace Bielu.Content.Populator;

public class PopulatorConfiguration
{
    private static PopulatorConfiguration _current;
    public Dictionary<Assembly, List<string>> EmbedAssemblies { get; set; }
    //public Dictionary<Assembly, List<Func<>>> IntiatedFromCode { get; set; }
    public static PopulatorConfiguration CurrentInstance
    {
        get
        {
            if (_current == null)
            {
                _current = new PopulatorConfiguration();
            }

            return _current;
        }
    }

    public PopulatorConfiguration()
    {
        Assembly test;
        test = typeof(PopulatorConfiguration).Assembly;
        EmbedAssemblies = new Dictionary<Assembly, List<string>>();
    }

    public IList<ContentDefitinion> GetFromEmbedAssemblies()
    {
        IList<ContentDefitinion> configurations = new List<ContentDefitinion?>();
        foreach (var embedAssembly in this.EmbedAssemblies)
        {
           

            foreach (var file in embedAssembly.Value)
            {          
                using (Stream stream = embedAssembly.Key.GetManifestResourceStream($"{embedAssembly.Key.GetName().Name}.{file}")){

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(result))
                        {
                            continue;
                        }

                        var configuration = JsonConvert.DeserializeObject<ContentDefitinion>(result);
                        configuration.AssemblySource = embedAssembly.Key.GetName().Name;
                        configurations.Add(configuration);
                    }
                }
            }
            
        }

        return configurations;
    }
    public IList<ContentDefitinion> GetAllConfigurations()
    {
        List<ContentDefitinion> configurations = new List<ContentDefitinion?>();
        configurations.AddRange(GetFromEmbedAssemblies());
        return configurations;

    }
}