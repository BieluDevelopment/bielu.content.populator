using System.Text.RegularExpressions;
using Bielu.Content.Populator.Models;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using ContentType = Bielu.Content.Populator.Models.ContentType;
using DataType = Bielu.Content.Populator.Models.DataType;
using PropertyType = Bielu.Content.Populator.Models.PropertyType;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public class PropertyVisitor : IPropertyVisitor
{
    private readonly IDataTypeService _dataTypeService;
    private readonly IDataTypeVisitor _dataTypeVisitor;

    public PropertyVisitor(IDataTypeService dataTypeService,IDataTypeVisitor dataTypeVisitor)
    {
        _dataTypeService = dataTypeService;
        _dataTypeVisitor = dataTypeVisitor;
    }

    public void Visit(ContentDefitinion contentDefitinion, Models.Content contentDef, IPublishedProperty property)
    {
       
        var value = property.GetSourceValue();
        var json = JsonConvert.SerializeObject(value);
        var datatype = _dataTypeService.GetDataType(property.PropertyType.DataType.Id);
        if (!contentDefitinion.DataTypes.Any(x => x.Id == property.PropertyType.DataType.Id))
        {
            var propertyType = new DataType(datatype.Key);
            contentDefitinion.DataTypes.Add(propertyType);
            _dataTypeVisitor.Visit(contentDefitinion,propertyType, datatype );

        }
        contentDef.Properties.Add(property.Alias, value);

        var regex = @"[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?";
        var matches = Regex.Matches(json, regex);
        AddDepedency(contentDef,DefinitionType.DataType, datatype.Key);
        foreach (Match match in matches)
        {
            var referenceKey = Guid.Parse(match.Value);
            if (referenceKey == contentDef.Key)
            {
                continue;
            }

            var type = GetDepedencyType(property.PropertyType, json);
            AddDepedency(contentDef,type, referenceKey);

        }

    }

    public void Visit(ContentDefitinion contentDefitinion, ContentType contentDef, IPropertyType property)
    {
        var datatype = _dataTypeService.GetDataType(property.DataTypeId);

        var propertyType = new DataType(datatype.Key);
        var propertyModel = new PropertyType();
        contentDefitinion.DataTypes.Add(propertyType);
        contentDef.Properties.Add(propertyModel);
        AddDepedency(contentDef,DefinitionType.DataType, datatype.Key);
        propertyModel.Alias = property.Alias;
        propertyModel.Name = property.Name;
        propertyModel.Datatype = datatype.Key;
        _dataTypeVisitor.Visit(contentDefitinion,propertyType, datatype );
    }

    private void AddDepedency(Models.Content contentDef, DefinitionType type, Guid referenceKey)
    {
        if (contentDef.DependsOn.ContainsKey(type)){
            contentDef.DependsOn[type].Add(referenceKey);
        }
        else
        {
            contentDef.DependsOn.Add(type,new List<Guid>()
            {
                referenceKey
            });
        }
    }
    private void AddDepedency(Models.ContentType contentDef, DefinitionType type, Guid referenceKey)
    {
        if (contentDef.DependsOn.ContainsKey(type)){
            contentDef.DependsOn[type].Add(referenceKey);
        }
        else
        {
            contentDef.DependsOn.Add(type,new List<Guid>()
            {
                referenceKey
            });
        }
    }

    private DefinitionType GetDepedencyType(IPublishedPropertyType propertyPropertyType, string json)
    {
        if (propertyPropertyType.ClrType.FullName.ToLower().Contains("IPublishedContent".ToLower()))
        {
            return DefinitionType.Content;
        }
        if (propertyPropertyType.ClrType.FullName.ToLower().Contains("Media".ToLower()))
        {
            return DefinitionType.Media;
        }

        return DefinitionType.Content;
    }
}