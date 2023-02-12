using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models;
using ContentType = Bielu.Content.Populator.Models.ContentType;
using MediaType = Bielu.Content.Populator.Models.MediaType;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public class MediaTypeVisitor : IMediaTypeVisitor
{
    private readonly IPropertyVisitor _propertyVisitor;

    public MediaTypeVisitor(IPropertyVisitor propertyVisitor)
    {
        _propertyVisitor = propertyVisitor;
    }

    public void Visit(ContentDefitinion contentDefitinion, MediaType contentDef, IMediaType content)
    {
        foreach (var property in    content.PropertyTypes)
        {
           
            _propertyVisitor.Visit(contentDefitinion, contentDef, property);
    
        }
    }
}