using Bielu.Content.Populator.Models;
using DataType = Bielu.Content.Populator.Models.DataType;

namespace Bielu.Content.Populator.Umbraco.Services;

public interface IDataTypeImportService
{
    IList<Changes> Import(IList<ContentDefitinion> configurations);
}