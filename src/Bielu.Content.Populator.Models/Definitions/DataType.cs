namespace Bielu.Content.Populator.Models;

public class DataType
{
    public Guid Key { get;  set;}
    public int Id { get; set; }
    public object? Configuration { get; set; }
    public int DatabaseType { get; set; }
    public string EditorAlias { get; set; }
    public string Editor { get; set; }

    public DataType(Guid key)
    {
        Key = key;
    }
}