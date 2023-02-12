using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using ContentType = Bielu.Content.Populator.Models.ContentType;
using MediaType = Bielu.Content.Populator.Models.MediaType;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public interface IPropertyVisitor
{
    void Visit(ContentDefitinion contentDefitinion, Models.Content contentDef, IPublishedProperty content);

    void Visit(ContentDefitinion contentDefitinion, ContentType contentDef, IPropertyType property);
    void Visit(ContentDefitinion contentDefitinion, MediaType contentDef, IPropertyType property);
    void Visit(object propertyValue);
}