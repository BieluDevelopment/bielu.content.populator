using Bielu.Content.Populator.Umbraco.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Attributes;

namespace Bielu.Content.Populator.Umbraco.Dashboard;
/// <summary>
///  Tree controller for the 'ContentPopulator' tree
/// </summary>
[Tree(global::Umbraco.Cms.Core.Constants.Applications.Settings, ContentPopulator.Trees.ContentPopulator,
    TreeGroup = ContentPopulator.Trees.Group,
    TreeTitle = ContentPopulator.Name, SortOrder = 35)]
[PluginController(ContentPopulator.Name)]
public class ContentPopulatorTreeController : TreeController
{
    public ContentPopulatorTreeController(ILocalizedTextService localizedTextService, UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, IEventAggregator eventAggregator) : base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
    {
    }
    /// <inheritdoc/>
    protected override ActionResult<TreeNode> CreateRootNode(FormCollection queryStrings)
    {
        var result = base.CreateRootNode(queryStrings);

        result.Value.RoutePath = $"{this.SectionAlias}/{ContentPopulator.Trees.ContentPopulator}/dashboard";
        result.Value.Icon = "icon-outbox";
        result.Value.HasChildren = false;
        result.Value.MenuUrl = null;

        return result.Value;
    }
    /// <inheritdoc/>
    protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, FormCollection queryStrings)
    {
        return new TreeNodeCollection();
    }
    /// <inheritdoc/>
    protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, FormCollection queryStrings)
    {
        return null;
    }
}