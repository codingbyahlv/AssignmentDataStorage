using System.Diagnostics;
using Shared.Interfaces;

namespace Shared.Utils;

//public class Logger(string filePath) : Ilogger
public class Logger() : ILogger
{
    //private readonly string _filePath = filePath;
    private readonly string _filePath = Path.Combine(FindSolutionDirectory(), "log.txt");

    //method: find the current solution filepath for the log file
    static string FindSolutionDirectory()
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string? solutionDirectory = currentDirectory;

        while (Directory.GetFiles(solutionDirectory, "*.sln").Length == 0)
        {
            solutionDirectory = Directory.GetParent(solutionDirectory)?.FullName;

            if (solutionDirectory == null)
            {
                throw new InvalidOperationException("Solution mapp hittas inte.");
            }
        }

        return solutionDirectory;
    }

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
