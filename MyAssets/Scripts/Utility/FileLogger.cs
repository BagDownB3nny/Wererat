using System.IO;
using UnityEngine;

public static class FileLogger
{
    public static readonly string LogFilePath = Path.Combine(Application.persistentDataPath, "debug.txt");

    public static void Log(string message)
    {
        // Write the log message to the file with a timestamp
        File.AppendAllText(LogFilePath, $"{System.DateTime.Now}: {message}\n");
    }

    public static void LogWarning(string message)
    {
        File.AppendAllText(LogFilePath, $"{System.DateTime.Now} [WARNING]: {message}\n");
    }

    public static void LogError(string message)
    {
        File.AppendAllText(LogFilePath, $"{System.DateTime.Now} [ERROR]: {message}\n");
    }
}
