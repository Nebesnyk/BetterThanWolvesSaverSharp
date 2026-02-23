using System.Text.Json;
using BTWSaver.Models;

namespace BTWSaver.Services;

public class TrackerBrain
{
    public List<SaveFile> SaveFiles { get; set; } = new();
    public string? LastOpenedFilePath { get; set; }
    
    public string BackupFolder { get; private set; }
    
    private string appFile;

    public TrackerBrain()
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        BackupFolder = Path.Combine(documentsPath, "BTWSaver_Backups");
        Directory.CreateDirectory(BackupFolder);
        
        appFile = Path.Combine(BackupFolder, "BTWSaver.json");
        
    }
    
    private string ToJson()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions{WriteIndented = true});
    }
    
    public void SaveToFile()
    {
        File.WriteAllText(appFile, ToJson());
    }

    public void LoadFromFile()
    {
        if (!File.Exists(appFile))
            return;
        string jsonString = File.ReadAllText(appFile);

        TrackerBrain? loadedBrain = JsonSerializer.Deserialize<TrackerBrain>(jsonString);

        if (loadedBrain != null && loadedBrain.SaveFiles != null)
        {
            this.SaveFiles = loadedBrain.SaveFiles;
            this.LastOpenedFilePath = loadedBrain.LastOpenedFilePath;
        }
    }
}