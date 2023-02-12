
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

    public IList<ContentDefitinion> GetFromEmbedAssemblies(StateReport stateReport)
    {
        IList<ContentDefitinion> configurations = new List<ContentDefitinion?>();
        foreach (var embedAssembly in this.EmbedAssemblies)
        {
           

            foreach (var file in embedAssembly.Value)
            {
                var assembly = embedAssembly.Key.GetName();
                var assemblyName = assembly.Name;
                var assemblyVersion = assembly.Version.ToString();
                if (stateReport.CurrentAssemblies.ContainsKey(assemblyName))
                {
                    if (stateReport.CurrentAssemblies[assemblyName] ==assemblyVersion)
                    {
                        continue;
                        
                    }

                    stateReport.CurrentAssemblies[assemblyName] = assemblyVersion;
                }
                else
                {
                  
                    stateReport.CurrentAssemblies.Add(assemblyName,assemblyVersion);  
                }
                using (Stream stream = embedAssembly.Key.GetManifestResourceStream($"{assemblyName}.{file}")){

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(result))
                        {
                            continue;
                        }

                        var configuration = JsonConvert.DeserializeObject<ContentDefitinion>(result);
                        configuration.AssemblySource = embedAssembly.Key.GetName().Name;
                        configuration.AssemblyVersion = embedAssembly.Key.GetName().Version.ToString();
                        configurations.Add(configuration);
                    }
                }
            }
            
        }

        return configurations;
    }
    public IList<ContentDefitinion> GetAllConfigurations(StateReport stateReport)
    {
        List<ContentDefitinion> configurations = new List<ContentDefitinion>();
        configurations.AddRange(GetFromEmbedAssemblies(stateReport));
        return configurations;

    }
}