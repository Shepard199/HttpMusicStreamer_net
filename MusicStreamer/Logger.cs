using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MusicStreamer;

public static class Logger
{
    private const string LogFile = "logs.txt";

    public static async Task SetErrorAsync(string error)
    {
        // Use asynchronous file operations
        await using (var fileStream = new FileStream(LogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
        await using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
        {
            await streamWriter.WriteLineAsync($"{DateTime.Now}: {error}");
        }
    }
}