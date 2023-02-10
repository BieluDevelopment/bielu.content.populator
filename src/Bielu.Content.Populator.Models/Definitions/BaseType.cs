namespace Bielu.Content.Populator.Models;

public class BaseType
{
    public Guid Key { get; }
    public bool IsContainer { get; set; }
    public Guid Parent { get; set; }
    public Guid[] Path { get; set; }
    public BaseType(Guid key)
    {
        Key = key;
        Properties = new List<PropertyType>();
        DependsOn = new Dictionary<DefinitionType, List<Guid>>();
    }

    public Dictionary<DefinitionType,List<Guid>> DependsOn { get; set; }
    public IList<PropertyType> Properties { get; set; }
    public string? Name { get; set; }
}