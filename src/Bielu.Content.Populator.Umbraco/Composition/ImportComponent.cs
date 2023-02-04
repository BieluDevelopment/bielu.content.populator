using Bielu.Content.Populator.Models;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Composing;

namespace Bielu.Content.Populator.Umbraco.Composition;

public class ImportComponent : IComponent
{
    private readonly PopulatorConfiguration _configuration;

    public ImportComponent(PopulatorConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Initialize()
    {
       
        var x = configurations;
    }

    public void Terminate()
    {
        throw new NotImplementedException();
    }
}