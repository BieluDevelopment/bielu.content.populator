using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public interface IContentVisitor
{
    void Visit(ContentDefitinion contentDefitinion, Models.Content contentDef, IPublishedContent content);
}