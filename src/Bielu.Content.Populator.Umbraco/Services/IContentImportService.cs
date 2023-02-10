using Bielu.Content.Populator.Models;

namespace Bielu.Content.Populator.Umbraco.Services;

public class ContentImportService : IContentImportService
{
    public IList<Changes> Import(IList<ContentDefitinion> configurations)
    {
        return new List<Changes>();
    }
}
public interface IContentImportService
{
    IList<Changes> Import(IList<ContentDefitinion> configurations);
}