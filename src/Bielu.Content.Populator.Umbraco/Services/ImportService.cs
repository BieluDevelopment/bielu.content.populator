using Bielu.Content.Populator.Models;
using Bielu.Content.Populator.Services;

namespace Bielu.Content.Populator.Umbraco.Services;

public class ImportService : IImportService
{
    private readonly ICompareService _service;
    private readonly IContentImportService _contentImportService;
    private readonly IContentTypeImportService _contentTypeImportService;
    private readonly IDataTypeImportService _dataTypeImportService;
    private readonly PopulatorConfiguration _configuration;

    public ImportService(ICompareService service, IContentImportService contentImportService,
        IContentTypeImportService contentTypeImportService, IDataTypeImportService dataTypeImportService,
        PopulatorConfiguration configuration)
    {
        _service = service;
        _contentImportService = contentImportService;
        _contentTypeImportService = contentTypeImportService;
        _dataTypeImportService = dataTypeImportService;
        _configuration = configuration;
    }

    public bool Import()
    {
        ChangesReport changeLog = new ChangesReport();
        IList<ContentDefitinion> configurations = _configuration.GetAllConfigurations();
        changeLog.DataTypes = _dataTypeImportService.Import(configurations);
        return true;
    }

    public bool ImportDataTypes()
    {
        ChangesReport changeLog = new ChangesReport();
        IList<ContentDefitinion> configurations = _configuration.GetAllConfigurations();
        changeLog.DataTypes = _dataTypeImportService.Import(configurations);
        return true;
    }
    public bool ImportContentTypes()
    {
        ChangesReport changeLog = new ChangesReport();
        IList<ContentDefitinion> configurations = _configuration.GetAllConfigurations();
        changeLog.ContentTypes = _contentTypeImportService.Import(configurations);
        return true;
    }
}