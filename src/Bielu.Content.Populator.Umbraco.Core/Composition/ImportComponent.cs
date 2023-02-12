using Bielu.Content.Populator.Services;
using Umbraco.Cms.Core.Composing;

namespace Bielu.Content.Populator.Umbraco.Composition;

public class ImportComponent : IComponent
{
    private readonly PopulatorConfiguration _configuration;
    private readonly IImportService _importService;

    public ImportComponent(PopulatorConfiguration configuration, IImportService importService)
    {
        _configuration = configuration;
        _importService = importService;
    }

    public void Initialize()
    {
        _importService.Import();
    }

    public void Terminate()
    {
    }
}