// Runtime assembly, not inside an Editor folder
using UnityEngine;

public class CreateScriptableObjectButtonAttribute : PropertyAttribute
{
    public string FolderPath;
    public string FileNamePrefix;

    public CreateScriptableObjectButtonAttribute(string folderPath, string fileNamePrefix = "New")
    {
        FolderPath = folderPath;
        FileNamePrefix = fileNamePrefix;
    }
}