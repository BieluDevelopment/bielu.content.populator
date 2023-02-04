using Bielu.Content.Populator.Umbraco.Notifications;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Bielu.Content.Populator.Umbraco.Composition;

public class ContentPopulatorComposer  : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<MenuRenderingNotification, TreeNotificationHandler>();
        builder.Components().Append<ImportComponent>();
    }
}