namespace Bielu.Content.Populator.Models;

public class ChangesReport
{
    public IList<Changes> DataTypes = new List<Changes>();
    public IList<Changes> ContentTypes { get; set; }
}

public class Changes
{
    public Guid Key { get; set; }
    public bool Skipped { get; set; }
    public bool Existed { get; set; }
    public bool DuplicatedInDefinitions { get; set; }
    public string Source { get; set; }
    public bool FirstOccurance { get; set; }
    public bool Created { get; set; }
}