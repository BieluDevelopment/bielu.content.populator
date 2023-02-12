using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public interface IMediaTypeVisitor
{
    void Visit(ContentDefitinion contentDefitinion, Models.MediaType contentDef, IMediaType content);
}