namespace Bielu.Content.Populator.Models;

public class Content
{
    public int Id { get; set; } = 0;
    public Guid Key { get; set; }
    public Guid ContentType { get; set; }
    public Content(Guid key)
    {
        Key = key;
        Properties = new Dictionary<string, object>();
        DependsOn = new Dictionary<DefinitionType,List<Guid>>();
    }

    public Dictionary<DefinitionType,List<Guid>> DependsOn { get; set; }
    public IDictionary<string,object> Properties { get; set; }
}