using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using ContentType = Bielu.Content.Populator.Models.ContentType;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public interface IPropertyVisitor
{
    void Visit(ContentDefitinion contentDefitinion, Models.Content contentDef, IPublishedProperty content);

    void Visit(ContentDefitinion contentDefitinion, ContentType contentDef, IPropertyType property);
}