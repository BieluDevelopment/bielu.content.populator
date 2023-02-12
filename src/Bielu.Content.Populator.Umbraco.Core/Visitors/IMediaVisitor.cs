using Umbraco.Cms.Core.Models.PublishedContent;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public interface IMediaVisitor
{
    void Visit(Models.Media contentDef, IPublishedContent content);
}