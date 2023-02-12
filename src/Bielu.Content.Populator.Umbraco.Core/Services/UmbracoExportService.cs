using System.Text;
using Bielu.Content.Populator.Models;
using Bielu.Content.Populator.Services;
using Bielu.Content.Populator.Umbraco.Visitors;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace Bielu.Content.Populator.Umbraco.Services;

public class UmbracoExportService : IExportService
{
    private readonly IContentDefinitionVisitor _contentDefinitionVisitor;
    private readonly IUmbracoContextFactory _contextFactory;
    private readonly IHostingEnvironment _env;
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public UmbracoExportService(IContentDefinitionVisitor contentDefinitionVisitor, IUmbracoContextFactory contextFactory, IHostingEnvironment env)
    {
        _contentDefinitionVisitor = contentDefinitionVisitor;
        _contextFactory = contextFactory;
        _env = env;
        _jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };
    }

    public bool ExportAllContent()
    {
        var path = Path.Combine(_env.ContentRootPath,$"ContentPopulatordata/content-{DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm")}.json");

        using (var contextReference = _contextFactory.EnsureUmbracoContext())
        {
            var context = contextReference.UmbracoContext;
            ContentDefitinion contentDef = new ContentDefitinion();

            foreach (var content in context.Content.GetAtRoot())
            {
                
                _contentDefinitionVisitor.VisitContent(context,contentDef, content);
                ExportChildren(context,contentDef, content);
            }
            using (FileStream fs = System.IO.File.Create(path))
            {
                byte[] content = new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(contentDef, _jsonSerializerSettings));

                fs.Write(content, 0, content.Length);
            }

        }

        return true;
    }

    public bool ExportAll()
    {
        throw new NotImplementedException();
    }

    private void ExportChildren(IUmbracoContext contextReference, ContentDefitinion contentDef, IPublishedContent content)
    {
        foreach (var child in content.Children)
        {
                
            _contentDefinitionVisitor.VisitContent(contextReference,contentDef, child);
            ExportChildren(contextReference,contentDef, content);
        }
    }

    public bool ExportContent(string id)
    {
        var path = Path.Combine(_env.ContentRootPath,$"ContentPopulatordata/content-{id}.json");

        using (var contextReference = _contextFactory.EnsureUmbracoContext())
        {
            var context = contextReference.UmbracoContext;
            ContentDefitinion contentDef = new ContentDefitinion();
            _contentDefinitionVisitor.VisitContent(context,contentDef, Guid.Parse(id));
            using (FileStream fs = System.IO.File.Create(path))
            {
                byte[] content = new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(contentDef, _jsonSerializerSettings));

                fs.Write(content, 0, content.Length);
            }

        }

        return true;
    }

    public bool ExportContentById(string id)
    {
        var path = Path.Combine(_env.ContentRootPath,$"ContentPopulatordata/content-{id}.json");

        using (var contextReference = _contextFactory.EnsureUmbracoContext())
        {
            var context = contextReference.UmbracoContext;
            ContentDefitinion contentDef = new ContentDefitinion();
            contentDef.CompatibleCmses.Add("umbraco");
            _contentDefinitionVisitor.VisitContent(context,contentDef, int.Parse(id));
            using (FileStream fs = System.IO.File.Create(path))
            {
                byte[] content = new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(contentDef, _jsonSerializerSettings));

                fs.Write(content, 0, content.Length);
            }

        }

        return true;
    }

    public bool ExportMedia(string id)
    {
        using (var contextReference = _contextFactory.EnsureUmbracoContext())
        {
            var context = contextReference.UmbracoContext;
            ContentDefitinion contentDef = new ContentDefitinion();
            _contentDefinitionVisitor.VisitMedia(context,contentDef,Guid.Parse(id) );
        }

        return true;
    }
}