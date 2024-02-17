using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MusicStreamer
{
    internal class MusicPlayer
    {
        public struct MusicPlayerStatus
        {
            public MusicFile CurrentFile;
            public int Connections;
            public List<MusicFile> Queue;
        }

        public event Func<MusicPlayerStatus, Task> OnStatusUpdate;
        public event Func<List<MusicFile>, Task> OnFileListUpdate;

        private List<MusicFile> Queue { get; } = new List<MusicFile>();
        private ConcurrentDictionary<Stream, DateTime> OutputStreams { get; } = new ConcurrentDictionary<Stream, DateTime>();
        private CancellationTokenSource CancellationTokenSource { get; } = new CancellationTokenSource();
        private CancellationToken CancellationToken => CancellationTokenSource.Token;

        private readonly object queueLock = new object();
        private int CurrentIndex = 0;

        private readonly SemaphoreSlim queueSemaphore = new SemaphoreSlim(1, 1);

        public MusicPlayer()
        {
            Task.Run(Player);
        }

        public async Task ClearQueue()
        {
            await queueSemaphore.WaitAsync();
            try
            {
                Queue.Clear();
                CurrentIndex = 0; // Сброс текущего индекса
                OnFileListUpdate?.Invoke(new List<MusicFile>(Queue));
            }
            finally
            {
                queueSemaphore.Release();
            }
        }

        public async Task EnqueueMusic(MusicFile file)
        {
            await queueSemaphore.WaitAsync();
            try
            {
                Queue.Add(file);
                OnFileListUpdate?.Invoke(new List<MusicFile>(Queue));
            }
            finally
            {
                queueSemaphore.Release();
            }
        }

        public async Task Attach(Stream outputStream)
        {
            if (!OutputStreams.TryAdd(outputStream, DateTime.Now))
            {
                await Logger.SetErrorAsync("Failed to attach stream; it may already be attached.");
            }
            else
            {
                await Logger.SetErrorAsync($"Stream attached successfully. Total streams: {OutputStreams.Count}");
            }
        }

        public async Task Disattach(Stream outputStream)
        {
            outputStream.Close();
            OutputStreams.TryRemove(outputStream, out _);
        }

        public void Stop()
        {
            CancellationTokenSource.Cancel();
            foreach (var outputStream in OutputStreams.Keys)
            {
                outputStream.Close();
            }
            OutputStreams.Clear();
        }

        public async Task Player()
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                MusicFile file = null;

                lock (queueLock)
                {
                    if (Queue.Count > 0)
                    {
                        if (CurrentIndex >= Queue.Count)
                        {
                            CurrentIndex = 0;
                        }
                        file = Queue[CurrentIndex];
                    }
                }

                if (file == null)
                {
                    await Task.Delay(1000); // Ожидаем, если очередь пуста
                    continue;
                }

                // Уведомляем о начале воспроизведения файла
                await OnStatusUpdate?.Invoke(new MusicPlayerStatus
                {
                    CurrentFile = file,
                    Connections = OutputStreams.Count,
                    Queue = new List<MusicFile>(Queue)
                });

                try
                {
                    file.Open();
                    while (file.GetFrame() is { } frame && !CancellationToken.IsCancellationRequested)
                    {
                        var outputStreamsSnapshot = OutputStreams.Keys.ToList();
                        foreach (var output in outputStreamsSnapshot)
                        {
                            try
                            {
                                await output.WriteAsync(frame.RawData, 0, frame.RawData.Length, CancellationToken);
                                await output.FlushAsync(CancellationToken);
                            }
                            catch (Exception ex)
                            {
                                await Logger.SetErrorAsync($"Error writing to output stream. Stream will be closed. \n {ex}");
                                output.Close();
                                await Disattach(output);
                            }
                        }
                    }

                    lock (queueLock)
                    {
                        CurrentIndex = (CurrentIndex + 1) % Queue.Count;
                    }
                }
                catch (Exception ex)
                {
                    await Logger.SetErrorAsync($"MusicPlayer error: can't decode file [{file.Filename}] \n {ex}");
                }
                finally
                {
                    // Повторно уведомляем об обновлении статуса после завершения воспроизведения файла
                    await OnStatusUpdate?.Invoke(new MusicPlayerStatus
                    {
                        CurrentFile = file,
                        Connections = OutputStreams.Count,
                        Queue = new List<MusicFile>(Queue)
                    });
                }

                // Опционально, можно уведомить о изменении списка файлов после воспроизведения каждого файла
                OnFileListUpdate?.Invoke(new List<MusicFile>(Queue));
            }
        }




    }
}
