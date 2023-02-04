using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;

namespace Bielu.Content.Populator.Umbraco.Visitors;

public class MediaVisitor : IMediaVisitor
{
    private readonly MediaFileManager _mediaFileManager;
    private readonly IMediaService _mediaService;

    public MediaVisitor(MediaFileManager mediaFileManager, IMediaService mediaService)
    {
        _mediaFileManager = mediaFileManager;
        _mediaService = mediaService;
    }

    public void Visit(Models.Media contentDef, IPublishedContent content)
    {
        contentDef.ContentType = content.ContentType.Key;
        foreach (var property in content.Properties)
        {
            contentDef.Properties.Add(property.Alias, property.GetValue());
        }

        if (content.Properties.Any(x => x.Alias == "umbracoFile"))
        {
            var media = _mediaService.GetById(content.Id);
            var file = content.Properties.FirstOrDefault(x => x.Alias == "umbracoFile") ;
            if (file != null)
            {
             var fileStream=   _mediaFileManager.GetFile(media,out var path);
             if (!string.IsNullOrWhiteSpace(path))
             {
                 contentDef.File = ReadAllBytes(fileStream);
             }
            }
  
        }
    }
    public static byte[] ReadAllBytes(Stream instream)
    {
        if (instream is MemoryStream)
            return ((MemoryStream) instream).ToArray();

        using (var memoryStream = new MemoryStream())
        {
            instream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}