using Bielu.Content.Populator.Umbraco.Assets.Filter;
using Umbraco.Cms.Core.DependencyInjection;

namespace Bielu.Content.Populator.Umbraco.Assets.Extensions;

public static class StaticAssetsExtensions
{
    public static IUmbracoBuilder AddStaticAssets(this IUmbracoBuilder builder)
    {
        // don't add if the filter is already there .
        if (builder.ManifestFilters().Has<AssetManifestFilter>())
            return builder;

        // add the package manifest programatically. 
        builder.ManifestFilters().Append<AssetManifestFilter>();

        return builder;
    }
}