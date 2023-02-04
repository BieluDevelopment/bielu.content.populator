using Bielu.Content.Populator.Models;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Cms.Core.Services;
using DataType = Bielu.Content.Populator.Models.DataType;

namespace Bielu.Content.Populator.Umbraco.Services;

public interface IDataTypeImportService
{
    IList<Changes> Import(IList<ContentDefitinion> configurations);
}

public class DataTypeImportService : IDataTypeImportService
{
    private readonly IDataTypeService _service;
    private readonly IConfigurationEditorJsonSerializer _serializer;

    public DataTypeImportService(IDataTypeService service,
        IConfigurationEditorJsonSerializer serializer)
    {
        _service = service;
        _serializer = serializer;
    }

    public IList<Changes> Import(IList<ContentDefitinion> configurations)
    {
        List<Changes> changes = new List<Changes>();
        var existingDataTypes = _service.GetAll();
        foreach (var contentDefitinion in configurations)
        {
            foreach (var dataType in contentDefitinion.DataTypes)
            {
                var change = new Changes();
                change.Source = contentDefitinion.AssemblySource;
                change.FirstOccurance = false;
                if (changes.Any(x => x.Key == dataType.Key))
                {
                    change.Key = dataType.Key;
                    change.Skipped = true;
                    change.DuplicatedInDefinitions = true;
                    change.FirstOccurance = false;
                    changes.Add(change);
                    continue;
                }

                if (existingDataTypes.Any(x => x.Key == dataType.Key))
                {
                    change.Key = dataType.Key;
                    change.Skipped = true;
                    change.Existed = true;
                    changes.Add(change);
                    continue;
                }

                try
                {
                    IDataType target =
                        new global::Umbraco.Cms.Core.Models.DataType(
                            JsonConvert.DeserializeObject(dataType.Editor) as IDataEditor, _serializer);
                    _service.Save(target, -1);
                    change.Created = true;
                }
                catch (Exception e)
                {
                }
            }
        }

        return changes;
    }
}