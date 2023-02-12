using Bielu.Content.Populator.Umbraco.Assets.Extensions;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Bielu.Content.Populator.Umbraco.Assets;

public class AssetsBoot : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddStaticAssets();
    }
}