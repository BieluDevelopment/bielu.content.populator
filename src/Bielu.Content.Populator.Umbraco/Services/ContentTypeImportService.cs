using Bielu.Content.Populator.Models;
using Lucene.Net.Util;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using ContentType = Bielu.Content.Populator.Models.ContentType;

namespace Bielu.Content.Populator.Umbraco.Services;

public class ContentTypeImportService : IContentTypeImportService
{
    private readonly IContentTypeService _contentTypeService;
    private readonly IShortStringHelper _shortStringHelper;

    public ContentTypeImportService(IContentTypeService contentTypeService, IShortStringHelper shortStringHelper)
    {
        _contentTypeService = contentTypeService;
        _shortStringHelper = shortStringHelper;
    }

    public IList<Changes> Import(IList<ContentDefitinion> configurations)
    {
        var documentTypesAliases = _contentTypeService.GetAllContentTypeAliases();
        var documentTypesIds = _contentTypeService.GetAllContentTypeIds(documentTypesAliases.ToArray());
        var documentTypes = _contentTypeService.GetAll(documentTypesIds.ToArray()).ToList();
        IList<Changes> list = new List<Changes>();
        foreach (var configuration in configurations)
        {
            list.AddRange(ImportContainers(configuration, documentTypes));
            list.AddRange(ImportContentTypes(configuration, documentTypes));
        }

        return list;
    }

    public IList<Changes> ImportContainers(ContentDefitinion configurations,
        List<IContentType> containers)
    {
        IList<Changes> list = new List<Changes>();
        foreach (var container in configurations.ContentTypes.Where(x => x.IsContainer).OrderBy(x => x.Path.Length))
        {
            var change = DoChange(container, containers, list);
            change.Source = configurations.AssemblySource;
            list.Add(change);
        }

        return list;
    }

    private Changes DoChange(ContentType container, IList<IContentType> containers, IEnumerable<Changes> changes)
    {
        var change = new Changes();
        IContentType? containerTarget = null;

        change.FirstOccurance = false;
        if (changes.Any(x => x.Key == container.Key))
        {
            change.Key = container.Key;
            change.Skipped = true;
            change.DuplicatedInDefinitions = true;
            change.FirstOccurance = false;
            return change;
        }

        if (containers.Any(x => x.Key == container.Key))
        {
            change.Key = container.Key;
            change.Skipped = true;
            change.Existed = true;
            containerTarget = containers.FirstOrDefault(x => x.Key == container.Key);
        }

        if (!change.Existed)
        {
            //   containerTarget = new global::Umbraco.Cms.Core.Models.ContentType(_shortStringHelper, );
            containerTarget.IsContainer = true;
            containerTarget.Name = container.Name;
        }

        if (containerTarget != null)
        {
            containers.Add(containerTarget);
            _contentTypeService.Save(containerTarget);
        }
        else
        {
            change.Created = false;
            change.Exception = new InvalidOperationException("Container not created");
        }

        return change;
    }

    public IList<Changes> ImportContentTypes(ContentDefitinion configurations,
        List<IContentType> contentTypes)
    {
        IList<Changes> list = new List<Changes>();
        foreach (var container in configurations.ContentTypes.Where(x => !x.IsContainer).OrderBy(x => x.Path?.Length ?? 0))
        {
            var change = DoChange(container, contentTypes, list);
            change.Source = configurations.AssemblySource;
            list.Add(change);
        }

        return list;
    }
}