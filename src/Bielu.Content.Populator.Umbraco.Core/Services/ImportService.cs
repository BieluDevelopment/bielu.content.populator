using System.Text;
using Bielu.Content.Populator.Models;
using Bielu.Content.Populator.Models.Report;

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
        var context = PrepareContext();
        ChangesReport changeLog = new ChangesReport();
        IList<ContentDefitinion> configurations = _configuration.GetAllConfigurations(context.State);
        changeLog.DataTypes = _dataTypeImportService.Import(configurations);
        changeLog.ContentTypes = _contentTypeImportService.Import(configurations);
        changeLog.Content = _contentImportService.Import(configurations);
        if (configurations.Any())
        {
            ReportChanges(configurations, context, changeLog);
        }

        return true;
    }

    private void ReportChanges(IList<ContentDefitinion> configurations, ContentPopulatorImportContext context, ChangesReport changeLog)
    {
        var  dir = Path.Combine(context.HistoryPath, $"history-{DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm")}.json");

        using (FileStream fs = System.IO.File.Create(dir))
        {
            byte[] content =
                new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(changeLog, _jsonSerializerSettings));

            fs.Write(content, 0, content.Length);
        }
        dir = Path.Combine(context.DataDirectory, $"state.json");

        using (FileStream fs = System.IO.File.Create(dir))
        {
            byte[] content =
                new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(context.State, _jsonSerializerSettings));

            fs.Write(content, 0, content.Length);
        }
    }

    private void EnsureBaseDirectories(string dataDir, string dir)
    {
        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
        }
     
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    private StateReport GetState(string reportFilePath)
    {
        return JsonConvert.DeserializeObject<StateReport>(File.ReadAllText(reportFilePath));
    }

    private ContentPopulatorImportContext PrepareContext()
    {
        var dataDir = Path.Combine(_env.ContentRootPath, $"ContentPopulatordata");
        var dir = Path.Combine(dataDir, "HistoryLogs");
        EnsureBaseDirectories(dataDir, dir);
        var reportFilePath = Path.Combine(dataDir, "state.json");
        StateReport state = File.Exists(reportFilePath) ? GetState(reportFilePath) : new StateReport();
        return new ContentPopulatorImportContext()
        {
            State = state,
            HistoryPath = dir,
            DataDirectory = dataDir
        };
    }

    public bool ImportDataTypes()
    {
       
        var context = PrepareContext();
        ChangesReport changeLog = new ChangesReport();
        IList<ContentDefitinion> configurations = _configuration.GetAllConfigurations(context.State).Where(x=>x.DataTypes.Any()).ToList();
        changeLog.DataTypes = _dataTypeImportService.Import(configurations);
        if (configurations.Any())
        {
            ReportChanges(configurations, context, changeLog);
        }
       
        return true;
    }

    public bool ImportContentTypes()
    {
           
        var context = PrepareContext();
        ChangesReport changeLog = new ChangesReport();
        IList<ContentDefitinion> configurations = _configuration.GetAllConfigurations(context.State).Where(x=>x.ContentTypes.Any()).ToList();
        changeLog.DataTypes = _contentTypeImportService.Import(configurations);
        if (configurations.Any())
        {
            ReportChanges(configurations, context, changeLog);
        }
        return true;
    }

    public bool ImportContent()
    {var context = PrepareContext();
        ChangesReport changeLog = new ChangesReport();
        IList<ContentDefitinion> configurations = _configuration.GetAllConfigurations(context.State).Where(x=>x.Content.Any()).ToList();
        changeLog.DataTypes = _contentImportService.Import(configurations);
        if (configurations.Any())
        {
            ReportChanges(configurations, context, changeLog);
        }
        return true;
    }
}