using Bielu.Content.Populator.Umbraco.Actions;
using Bielu.Content.Populator.Umbraco.Controllers;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Security;

namespace Bielu.Content.Populator.Umbraco.Notifications;

public class TreeNotificationHandler : INotificationHandler<MenuRenderingNotification>
{
    public void Handle(MenuRenderingNotification notification)
    {
        
        var menuItem = new MenuItem("sendValidation", "ExportToContentPopulator");
        menuItem.Action = new ExportAction();
        menuItem.Icon = "shuffle";
        menuItem.SeparatorBefore = true;
        menuItem.NavigateToRoute($"/umbraco/backoffice/api/{nameof(ExportApiController)}/{nameof(ExportApiController.ExportContent)}?id={notification.NodeId}");
        notification.Menu.Items.Add(menuItem);
    }
}