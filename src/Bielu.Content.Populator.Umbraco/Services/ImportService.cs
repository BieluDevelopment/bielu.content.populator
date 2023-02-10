using System.Text;
using Bielu.Content.Populator.Models;
using Bielu.Content.Populator.Services;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Bielu.Content.Populator.Umbraco.Services;

public class ImportService : IImportService
{
    private readonly IContentImportService _contentImportService;
    private readonly IContentTypeImportService _contentTypeImportService;
    private readonly IDataTypeImportService _dataTypeImportService;
    private readonly PopulatorConfiguration _configuration;
    private readonly IHostingEnvironment _env;
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public ImportService(IContentImportService contentImportService,
        IContentTypeImportService contentTypeImportService, IDataTypeImportService dataTypeImportService,
        PopulatorConfiguration configuration, IHostingEnvironment env)
    {
        _contentImportService = contentImportService;
        _contentTypeImportService = contentTypeImportService;
        _dataTypeImportService = dataTypeImportService;
        _configuration = configuration;
        _env = env;
        _jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };
    }

    public bool Import()
    {
        ChangesReport changeLog = new ChangesReport();
        IList<ContentDefitinion> configurations = _configuration.GetAllConfigurations();
        changeLog.DataTypes = _dataTypeImportService.Import(configurations);
        changeLog.ContentTypes = _contentTypeImportService.Import(configurations);
        changeLog.Content = _contentImportService.Import(configurations);

        var dir = Path.Combine(_env.ContentRootPath, $"ContentPopulatordata");

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        dir = Path.Combine(dir, "HistoryLogs");
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        dir = Path.Combine(dir, $"history-{DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm")}.json");

        using (FileStream fs = System.IO.File.Create(dir))
        {
            byte[] content =
                new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(changeLog, _jsonSerializerSettings));

            fs.Write(content, 0, content.Length);
        }


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

    public bool ImportContent()
    {
        ChangesReport changeLog = new ChangesReport();
        IList<ContentDefitinion> configurations = _configuration.GetAllConfigurations();
        changeLog.Content = _contentImportService.Import(configurations);
        return true;
    }
}