using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using ContentType = Umbraco.Cms.Core.Models.ContentType;

namespace Bielu.Content.Populator.Umbraco.Services;

public class ContentTypeImportService : IContentTypeImportService
{
    private readonly IContentTypeService _contentTypeService;

    public ContentTypeImportService(IContentTypeService contentTypeService)
    {
        _contentTypeService = contentTypeService;
    }

    public IList<Changes> Import(IList<ContentDefitinion> configurations)
    {
        var documentType = new ContentType()
        _contentTypeService.Save();
    }
}
public interface IContentTypeImportService
{
    IList<Changes> Import(IList<ContentDefitinion> configurations);
}