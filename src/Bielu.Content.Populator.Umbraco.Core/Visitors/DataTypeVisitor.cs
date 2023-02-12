using Bielu.Content.Populator.Models;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public class DataTypeVisitor : IDataTypeVisitor
{
    public void Visit(ContentDefitinion contentDefitinion, Models.DataType contentDef, IDataType content)
    {
        contentDef.Configuration = content.Configuration;
        contentDef.DatabaseType = (int)content.DatabaseType;
        contentDef.EditorAlias = content.EditorAlias;
        contentDef.Editor = JsonConvert.SerializeObject(content.Editor);
    }
}