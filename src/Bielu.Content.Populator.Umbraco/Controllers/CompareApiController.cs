using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;

namespace Bielu.Content.Populator.Umbraco.Controllers;

public class CompareApiController : UmbracoApiController
{
    [HttpPost]
    public async Task<bool> CompareContent([FromQuery] string id, LoadFromType sourceType,  LoadingDetails loadingDetails)
    {
        return true;
    }
}