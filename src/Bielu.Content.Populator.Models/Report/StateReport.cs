namespace Bielu.Content.Populator.Models;

public class StateReport
{
    public StateReport()
    {
        CurrentAssemblies = new Dictionary<string, string>();
    }

    public Dictionary<string, string> CurrentAssemblies { get; set; }
}