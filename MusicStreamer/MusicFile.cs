using NAudio.Wave;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using TagLib;

namespace MusicStreamer;

internal class MusicFile(string filename)
{
    private readonly object durationLock = new(); // объект для блокировки Monitor

    private Mp3FileReader Reader;
    public string Filename { get; } = filename;
    private long Duration;
    private File File { get; } = File.Create(filename); //get music file metadata
    private Stopwatch Stopwatch;

    public string Title
    {
        get => File.Tag.Title;
        set => File.Tag.Title = value;
    }

    public string Artist => File.Tag.FirstPerformer;

    public void Open()
    {
        Stopwatch = new Stopwatch();
        Reader = new Mp3FileReader(Filename);
        Duration = 0;
        Stopwatch.Start();
    }

    public Mp3Frame GetFrame()
    {
        lock (durationLock) // Захватываем блокировку для синхронизации доступа к Duration
        {
            while (Stopwatch.ElapsedMilliseconds < Duration)
                Thread.Sleep(1);
            var frame = Reader.ReadNextFrame();
            if (frame == null)
                return null;
            Duration += FrameDuration(frame);
            return frame;
        }
    }

    private static long FrameDuration(Mp3Frame frame)
    {
        var byterate = frame.BitRate / 8;
        return frame.FrameLength * 1000 / byterate;
    }

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(Title))
            return Title;
        var res = new Regex(@"/|\\(.*)\.mp3").Match(Filename);
        return res.Captures[0].Value;
    }
}