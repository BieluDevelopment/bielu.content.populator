using Bielu.Content.Populator.Models;
using Umbraco.Cms.Core.Models;
using ContentType = Umbraco.Cms.Core.Models.ContentType;

namespace Bielu.Content.Populator.Umbraco.Services;

public interface IContentTypeImportService
{
    IList<Changes> Import(IList<ContentDefitinion> configurations);
}