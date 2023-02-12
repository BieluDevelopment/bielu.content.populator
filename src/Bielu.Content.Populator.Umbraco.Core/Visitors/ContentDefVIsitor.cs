using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public class ContentDefVIsitor : IContentDefinitionVisitor
{
    private readonly IContentVisitor _contentVisitor;
    private readonly IMediaVisitor _mediaVisitor;
    private readonly IContentTypeVisitor _contentTypeVisitor;
    private readonly IMediaTypeService _mediaTypeService;
    private readonly IContentTypeService _contentTypeService;

    public ContentDefVIsitor(IContentVisitor contentVisitor, IMediaVisitor mediaVisitor, IContentTypeVisitor contentTypeVisitor, IMediaTypeService mediaTypeService, IContentTypeService contentTypeService)
    {
        _contentVisitor = contentVisitor;
        _mediaVisitor = mediaVisitor;
        _contentTypeVisitor = contentTypeVisitor;
        _mediaTypeService = mediaTypeService;
        _contentTypeService = contentTypeService;
    }

    public void VisitContent(IUmbracoContext context, ContentDefitinion contentDef, int key)
    {
        var content = context.Content?.GetById(key);

        if (content != null)
        {
            var contentModel = new Models.Content(content.Key);
            contentDef.Content.Add(contentModel);
          
            _contentVisitor.Visit(contentDef, contentModel, content);
            VisitDepedencies(context, contentDef, contentModel.DependsOn);
        }
    }

    public void VisitMedia(IUmbracoContext context, ContentDefitinion contentDef, int key)
    {
        var content = context.Media?.GetById(key);

        if (content != null)
        {
            var contentModel = new Models.Content(content.Key);
            contentDef.Content.Add(contentModel);
            _contentVisitor.Visit(contentDef, contentModel, content);
            VisitDepedencies(context, contentDef, contentModel.DependsOn);
        }
    }



    public void VisitContent(IUmbracoContext context, ContentDefitinion contentDef, Guid key)
    {
        var content = context.Content?.GetById(key);

        if (content != null)
        {
            var contentModel = new Models.Content(content.Key);
            contentDef.Content.Add(contentModel);
            _contentVisitor.Visit(contentDef, contentModel, content);
            VisitDepedencies(context, contentDef, contentModel.DependsOn);
        }
    }

    private void VisitDepedencies(IUmbracoContext context, ContentDefitinion contentDef,
        Dictionary<DefinitionType, List<Guid>> dependsOn)
    {
        foreach (var dependencies in dependsOn)
        {
            switch (dependencies.Key)
            {
                case DefinitionType.Media:
                    foreach (var item in dependencies.Value)
                    {
                        VisitMedia(context, contentDef, item);
                    }

                    break;
                case DefinitionType.Content:
                    foreach (var item in dependencies.Value)
                    {
                        VisitContent(context, contentDef, item);
                    }

                    break;
                case DefinitionType.ContentType:
                    foreach (var item in dependencies.Value)
                    {
                        VisitContentType(contentDef, item);
                    }
                    break;
                case DefinitionType.MediaType:
                    foreach (var item in dependencies.Value)
                    {
                        VisitMediaType(contentDef, item);
                    }
                    break;
               
            }
        }
    }

 

    public void VisitMediaType(ContentDefitinion contentDef, Guid dependenciesValue)
    {
        var contentType = _mediaTypeService.Get(dependenciesValue);
        var contentTypeModel = new MediaType(dependenciesValue)
        {
            Path = contentType.Path.Split(",").Skip(1).Select(x=>_contentTypeService.Get(x)!.Key).ToArray()
        };
        contentTypeModel.Parent = contentTypeModel.Path.Last();
        contentTypeModel.DependsOn.Add(DefinitionType.ContentType, contentTypeModel.Path.ToList());
        contentDef.MediaTypes.Add(contentTypeModel);
     //   _mediaTypeVisitor.Visit(contentDef,contentTypeModel, contentType );
    }

    public void VisitContentType(ContentDefitinion contentDef, Guid dependenciesValue)
    {
        var contentType = _contentTypeService.Get(dependenciesValue);
        var contentTypeModel = new ContentType(dependenciesValue)
        {
            Path = contentType.Path.Split(",").Skip(1).Select(x=>_contentTypeService.Get(int.Parse(x))?.Key ?? _contentTypeService.GetContainer(int.Parse(x)).Key).ToArray()
        };
        contentTypeModel.Name = contentType.Name;
        contentTypeModel.Parent = contentTypeModel.Path.Last();
        contentTypeModel.DependsOn.Add(DefinitionType.ContentType, contentTypeModel.Path.ToList());
        contentDef.ContentTypes.Add(contentTypeModel);
        _contentTypeVisitor.Visit(contentDef,contentTypeModel, contentType );
    }

    public void VisitMedia(IUmbracoContext context, ContentDefitinion contentDef,
        Guid mediaKey)
    {
        var media = context.Media?.GetById(mediaKey);

        if (media != null)
        {
            var contentModel = new Models.Media(media.Key);
            contentDef.Media.Add(contentModel);
            _mediaVisitor.Visit(contentModel, media);
            VisitDepedencies(context, contentDef, contentModel.DependsOn);
        }
    }

    public void VisitContent(IUmbracoContext context, ContentDefitinion contentDef, IPublishedContent content)
    {
        if (content != null)
        {
            var contentModel = new Models.Content(content.Key);
            contentDef.Content.Add(contentModel);
            _contentVisitor.Visit(contentDef, contentModel, content);
            VisitDepedencies(context, contentDef, contentModel.DependsOn);
        }
    }
}