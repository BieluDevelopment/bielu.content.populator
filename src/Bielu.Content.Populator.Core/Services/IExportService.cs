namespace Bielu.Content.Populator.Services;

public interface IExportService
{
    public bool ExportAll();
    public bool ExportAllContent();

    public bool ExportContent(string id);
    public bool ExportContentById(string id);
}