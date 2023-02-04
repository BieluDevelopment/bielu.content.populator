using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public interface IContentDefinitionVisitor
{
    void VisitContent(IUmbracoContext context, ContentDefitinion contentDef, int key);
    void VisitMedia(IUmbracoContext context, ContentDefitinion contentDef, int key);
    void VisitContent(IUmbracoContext context, ContentDefitinion contentDef, Guid key);
    void VisitMedia(IUmbracoContext context, ContentDefitinion contentDef, Guid key);
    void VisitContent(IUmbracoContext umbracoContext, ContentDefitinion contentDef, IPublishedContent content);
    void VisitMediaType(ContentDefitinion contentDef, Guid key);   
    void VisitContentType(ContentDefitinion contentDef, Guid dependenciesValue);

}