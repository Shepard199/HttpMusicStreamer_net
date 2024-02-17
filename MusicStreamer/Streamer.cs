using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicStreamer
{
    internal class Streamer : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public MusicPlayer Player { get; }
        private readonly HttpListener _listener;

        public Streamer(int port)
        {
            Player = new MusicPlayer(); // Запуск происходит внутри конструктора MusicPlayer

            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://+:{port}/");
            _listener.Start();

            Task.Run(async () => await WaitForConnectionsAsync()).ConfigureAwait(false);
        }

        private async Task WaitForConnectionsAsync()
        {
            try
            {
                while (_listener.IsListening)
                {
                    Console.WriteLine("Ожидание нового подключения...");
                    var context = await _listener.GetContextAsync();
                    await ProcessAsync(context, _cancellationTokenSource.Token);
                }
            }
            catch (HttpListenerException)
            {
                // Исключение будет возникать при остановке HttpListener
            }
            catch (Exception e)
            {
                await Logger.SetErrorAsync($"Ошибка Streamer: [{e.Message}]");
            }
        }

        private async Task ProcessAsync(HttpListenerContext context, CancellationToken cancellationToken)
        {
            var req = context.Request;
            var res = context.Response;

            try
            {
                if (req.HttpMethod is "GET" or "OPTIONS") // Добавлено условие для обработки OPTIONS запросов
                {
                    if (req.RawUrl != "/stream")
                    {
                        var responseString = "<HTML><BODY> Stream доступен по адресу /stream </BODY></HTML>";
                        var buffer = Encoding.UTF8.GetBytes(responseString);
                        res.ContentLength64 = buffer.Length;
                        await res.OutputStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
                    }
                    else
                    {
                        res.AddHeader("content-type", "audio/mp3");
                        res.AddHeader("Accept-Ranges", "bytes");
                        await Player.Attach(res.OutputStream);
                    }
                }
            }
            finally
            {
                //res.Close(); // Убедитесь, что ответ всегда закрывается
            }
        }


        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            _listener.Stop();
            _listener.Close();
        }
    }
}
