using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public interface IDataTypeVisitor
{
    void Visit(ContentDefitinion contentDefitinion, Models.DataType contentDef, IDataType content);
}