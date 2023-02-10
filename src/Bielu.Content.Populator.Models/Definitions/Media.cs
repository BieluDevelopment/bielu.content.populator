namespace Bielu.Content.Populator.Models;

public class Media
{
    public int Id { get; set; } = 0;
    public Guid Key { get; set; }
    public Media(Guid key)
    {
        Key = key;
        Properties = new Dictionary<string, object>();
        DependsOn = new Dictionary<DefinitionType,List<Guid>>();
    }

    public Guid ContentType { get; set; }
    public Dictionary<DefinitionType,List<Guid>> DependsOn { get; set; }
    public IDictionary<string,object> Properties { get; set; }
    public byte[] File { get; set; }
}