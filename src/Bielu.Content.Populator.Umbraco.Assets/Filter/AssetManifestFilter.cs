using System.Diagnostics;
using Bielu.Content.Populator.Umbraco.Constants;
using Umbraco.Cms.Core.Manifest;

namespace Bielu.Content.Populator.Umbraco.Assets.Filter;

public class AssetManifestFilter : IManifestFilter
{
    public void Filter(List<PackageManifest> manifests)
    {
        var assembly = typeof(AssetManifestFilter).Assembly;
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        string version = fileVersionInfo.ProductVersion;
            
        manifests.Add(new PackageManifest
        {
            PackageName = ContentPopulator.Package.Name,
            Version = assembly.GetName().Version.ToString(3),
            AllowPackageTelemetry = true,
            BundleOptions = BundleOptions.None,
            Scripts = new[]
            {
                $"{ContentPopulator.Package.PluginPath}/contentPopulator.dashboard.controller.js"
            },
            Stylesheets = new[]
            {
                $"{ContentPopulator.Package.PluginPath}/contentPopulator.{version}.min.css"
            }
        }); ;
    }
}