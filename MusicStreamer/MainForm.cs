using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
#pragma warning disable CA1416
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace MusicStreamer;

public partial class MainForm : Form
{
    private Streamer Streamer { get; set; }

    public MainForm()
    {
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        try
        {
            Streamer = new Streamer(8000);
            Streamer.Player.OnStatusUpdate += Player_OnStatusUpdate;
            Streamer.Player.OnFileListUpdate += Player_OnFileListUpdate;
        }
        catch (Exception exeption)
        {
            MessageBox.Show("Error in starting application. Check log file.");
            Logger.SetErrorAsync($"Streamer: can't create HttpListener. Error [{exeption.Message}]");
            Environment.Exit(-1);
        }
    }

    private Task Player_OnFileListUpdate(List<MusicFile> queue)
    {
        Invoke((MethodInvoker)(() => { lbTracksQueue.DataSource = queue; }));
        return Task.CompletedTask;
    }

    private async Task Player_OnStatusUpdate(MusicPlayer.MusicPlayerStatus arg)
    {
        try
        {
            // Используйте Invoke, чтобы обновить UI в основном потоке
            Invoke((MethodInvoker)(() =>
            {
                lbCurrentTrack.Text = arg.CurrentFile.Title + " - " + arg.CurrentFile.Artist;
                lbConnectionsCount.Text = $"{arg.Connections}";
            }));
        }
        catch (Exception e)
        {
            await Logger.SetErrorAsync($"Player_OnStatusUpdate error: [{e.Message}]");
        }
    }


    private void BtnOpen_Click(object sender, EventArgs e)
    {
        if (fbdOpen.ShowDialog() == DialogResult.OK)
        {
            var filenames = Directory.GetFiles(fbdOpen.SelectedPath, "*.mp3");
            foreach (var filename in filenames)
            {
                var file = new MusicFile(filename);
                file.Title ??= "Unknown";
                var item = new ListViewItem([file.Title, file.Artist, file.Filename])
                {
                    Tag = file
                };
                lvFiles.Items.Add(item);
            }
        }
    }

    private void LvFiles_DoubleClick(object sender, EventArgs e)
    {
        if (lvFiles.SelectedItems.Count > 0) Streamer.Player.EnqueueMusic((MusicFile)lvFiles.SelectedItems[0].Tag);
    }

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        Streamer.Dispose();
    }

    private void LvFiles_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right) lvFiles.Items.Remove(lvFiles.SelectedItems[0]);
    }

    private void BtnClearQueue_Click(object sender, EventArgs e)
    {
        Streamer.Player.ClearQueue();
    }
}