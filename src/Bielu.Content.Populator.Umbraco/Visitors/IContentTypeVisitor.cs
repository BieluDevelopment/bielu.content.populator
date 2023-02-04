using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public interface IContentTypeVisitor
{
    void Visit(ContentDefitinion contentDefitinion, Models.ContentType contentDef, IContentType content);
}