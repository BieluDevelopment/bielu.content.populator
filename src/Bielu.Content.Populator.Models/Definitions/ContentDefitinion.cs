using System.Text.Json.Serialization;

namespace Bielu.Content.Populator.Models;

public class ContentDefitinion
{
    public ContentDefitinion()
    {
        DataTypes = new List<DataType>();
        Content = new List<Content>();
        Media = new List<Media>();
        ContentTypes = new List<ContentType>();
        MediaTypes = new List<MediaType>();
        CompatibleCmses = new List<string>();
    }
    [JsonIgnore]
    public string AssemblySource { get; set; }   
    [JsonIgnore]
    public string AssemblyVersion { get; set; }
    public string TargetVersion { get; set; }
    public IList<string> CompatibleCmses { get; set; }
    public IList<DataType> DataTypes { get; set; }
    public IList<Content> Content { get; set; }
    public IList<Media> Media { get; set; }
    public IList<ContentType> ContentTypes { get; set; }
    public IList<MediaType> MediaTypes { get; set; }
}