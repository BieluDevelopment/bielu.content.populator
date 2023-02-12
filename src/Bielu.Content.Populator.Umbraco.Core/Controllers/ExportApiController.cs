using Bielu.Content.Populator.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Controllers;

namespace Bielu.Content.Populator.Umbraco.Controllers;

public class ExportApiController : UmbracoApiController
{
    private readonly IExportService _service;

    public ExportApiController(IExportService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<bool> ExportAllContent()
    {
        return _service.ExportAllContent();
    }
    [HttpGet]
    public async Task<bool> ExportContent([FromQuery] string id)
    {
        return _service.ExportContent(id);
    }
    [HttpGet]
    public async Task<bool> ExportContentById([FromQuery] string id)
    {
        return _service.ExportContentById(id);
    }
}