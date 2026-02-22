namespace BTWSaver.Models;

public class SaveFile
{
    public string SaveFileName { get; set; } = "";
    public string SaveFilePath { get; set; } = "";
    public int LastIndex { get; set; } = 0;
    public int CurrentIndex { get; set; } = 0;
}