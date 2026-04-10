using System;
using UnityEngine;

public class ConnectChannelButtonAttribute : PropertyAttribute
{
    public readonly Type ChannelType;
    public readonly string FolderPath;
    public readonly string FileNamePrefix;

    public ConnectChannelButtonAttribute(Type channelType, string folderPath, string fileNamePrefix = "New")
    {
        ChannelType = channelType;
        FolderPath = folderPath;
        FileNamePrefix = fileNamePrefix;
    }
}