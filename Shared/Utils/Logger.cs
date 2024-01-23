using System.Diagnostics;
using Shared.Interfaces;

namespace Shared.Utils;

public class Logger(string filePath) : ILogger
{
    private readonly string _filePath = filePath;

    //method: create message, log message, write to file. if fail just log message
    public void Log(string message, string methodName = "")
    {
        try
        {
            var logMessage = $"{DateTime.Now}, method:{methodName} :: {message}";
            Debug.WriteLine(logMessage);
            using var sw = new StreamWriter(_filePath, true);
            sw.WriteLine(logMessage);
        }
        catch (Exception ex) { Debug.WriteLine($"ERROR {DateTime.Now}, method:{nameof(Log)} :: {ex.Message}"); }
    }
}
