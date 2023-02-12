using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public class ContentVisitor : IContentVisitor
{
    private readonly IPropertyVisitor _propertyVisitor;
    private readonly IDataTypeService _dataTypeService;

    public ContentVisitor(IPropertyVisitor propertyVisitor)
    {
        _propertyVisitor = propertyVisitor;
    }

    public void Visit(ContentDefitinion contentDefitinion, Models.Content contentDef, IPublishedContent content)
    {
        contentDef.ContentType = content.ContentType.Key;
        AddDepedency(contentDef, DefinitionType.ContentType, content.ContentType.Key);
        foreach (var property in content.Properties)
        {
            _propertyVisitor.Visit(contentDefitinion, contentDef, property);
        }
    }
    //todo: move to helper as repeating bettween files
    private void AddDepedency(Models.Content contentDef, DefinitionType type, Guid referenceKey)
    {
        if (contentDef.DependsOn.ContainsKey(type))
        {
            contentDef.DependsOn[type].Add(referenceKey);
        }
        else
        {
            contentDef.DependsOn.Add(type, new List<Guid>()
            {
                referenceKey
            });
        }
    }
}