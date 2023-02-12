using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models;
using ContentType = Bielu.Content.Populator.Models.ContentType;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public class ContentTypeVisitor : IContentTypeVisitor
{
    private readonly IPropertyVisitor _propertyVisitor;

    public ContentTypeVisitor(IPropertyVisitor propertyVisitor)
    {
        _propertyVisitor = propertyVisitor;
    }

    public void Visit(ContentDefitinion contentDefitinion, ContentType contentDef, IContentType content)
    {
        foreach (var property in    content.PropertyTypes)
        {
           
            _propertyVisitor.Visit(contentDefitinion, contentDef, property);
    
        }
    }
}