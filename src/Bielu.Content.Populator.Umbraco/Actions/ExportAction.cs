using Umbraco.Cms.Core.Actions;

namespace Bielu.Content.Populator.Umbraco.Actions;

public class ExportAction : IAction
{
    public const char ActionLetter = 'ï';

    public char Letter => ActionLetter;
    public bool ShowInNotifier => false;
    public bool CanBePermissionAssigned => true;
    public string Icon => "icon-outbox";
    public string Alias => "exportContentPopulator";
    public string Category => "Custom";
}