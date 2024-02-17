namespace MusicStreamer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            lbPlayingNow = new System.Windows.Forms.Label();
            lbConnections = new System.Windows.Forms.Label();
            lbCurrentTrack = new System.Windows.Forms.Label();
            lbConnectionsCount = new System.Windows.Forms.Label();
            lvFiles = new System.Windows.Forms.ListView();
            chTitle = new System.Windows.Forms.ColumnHeader();
            chArtist = new System.Windows.Forms.ColumnHeader();
            chFile = new System.Windows.Forms.ColumnHeader();
            lbTracksQueue = new System.Windows.Forms.ListBox();
            lbTrackQueue = new System.Windows.Forms.Label();
            btnOpen = new System.Windows.Forms.Button();
            fbdOpen = new System.Windows.Forms.FolderBrowserDialog();
            btnClearQueue = new System.Windows.Forms.Button();
            btnAddSong = new System.Windows.Forms.Button();
            btnRemoveSong = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lbPlayingNow
            // 
            lbPlayingNow.AutoSize = true;
            lbPlayingNow.Location = new System.Drawing.Point(9, 7);
            lbPlayingNow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbPlayingNow.Name = "lbPlayingNow";
            lbPlayingNow.Size = new System.Drawing.Size(77, 15);
            lbPlayingNow.TabIndex = 0;
            lbPlayingNow.Text = "Now playing:";
            // 
            // lbConnections
            // 
            lbConnections.AutoSize = true;
            lbConnections.Location = new System.Drawing.Point(9, 38);
            lbConnections.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbConnections.Name = "lbConnections";
            lbConnections.Size = new System.Drawing.Size(80, 15);
            lbConnections.TabIndex = 1;
            lbConnections.Text = "Connections: ";
            // 
            // lbCurrentTrack
            // 
            lbCurrentTrack.AutoSize = true;
            lbCurrentTrack.Location = new System.Drawing.Point(113, 7);
            lbCurrentTrack.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbCurrentTrack.Name = "lbCurrentTrack";
            lbCurrentTrack.Size = new System.Drawing.Size(17, 15);
            lbCurrentTrack.TabIndex = 2;
            lbCurrentTrack.Text = "--";
            // 
            // lbConnectionsCount
            // 
            lbConnectionsCount.AutoSize = true;
            lbConnectionsCount.Location = new System.Drawing.Point(113, 38);
            lbConnectionsCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbConnectionsCount.Name = "lbConnectionsCount";
            lbConnectionsCount.Size = new System.Drawing.Size(17, 15);
            lbConnectionsCount.TabIndex = 3;
            lbConnectionsCount.Text = "--";
            // 
            // lvFiles
            // 
            lvFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { chTitle, chArtist, chFile });
            lvFiles.Location = new System.Drawing.Point(306, 70);
            lvFiles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            lvFiles.MultiSelect = false;
            lvFiles.Name = "lvFiles";
            lvFiles.Size = new System.Drawing.Size(637, 261);
            lvFiles.TabIndex = 5;
            lvFiles.UseCompatibleStateImageBehavior = false;
            lvFiles.View = System.Windows.Forms.View.Details;
            lvFiles.DoubleClick += LvFiles_DoubleClick;
            lvFiles.MouseClick += LvFiles_MouseClick;
            // 
            // chTitle
            // 
            chTitle.Text = "Title";
            chTitle.Width = 210;
            // 
            // chArtist
            // 
            chArtist.Text = "Artist";
            chArtist.Width = 126;
            // 
            // chFile
            // 
            chFile.Text = "File";
            chFile.Width = 121;
            // 
            // lbTracksQueue
            // 
            lbTracksQueue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lbTracksQueue.FormattingEnabled = true;
            lbTracksQueue.ItemHeight = 15;
            lbTracksQueue.Items.AddRange(new object[] { " " });
            lbTracksQueue.Location = new System.Drawing.Point(12, 104);
            lbTracksQueue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            lbTracksQueue.Name = "lbTracksQueue";
            lbTracksQueue.Size = new System.Drawing.Size(290, 227);
            lbTracksQueue.TabIndex = 6;
            // 
            // lbTrackQueue
            // 
            lbTrackQueue.AutoSize = true;
            lbTrackQueue.Location = new System.Drawing.Point(9, 76);
            lbTrackQueue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lbTrackQueue.Name = "lbTrackQueue";
            lbTrackQueue.Size = new System.Drawing.Size(80, 15);
            lbTrackQueue.TabIndex = 7;
            lbTrackQueue.Text = "Tracks Queue:";
            // 
            // btnOpen
            // 
            btnOpen.BackColor = System.Drawing.Color.GhostWhite;
            btnOpen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnOpen.Location = new System.Drawing.Point(797, 7);
            btnOpen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new System.Drawing.Size(145, 46);
            btnOpen.TabIndex = 8;
            btnOpen.Text = "Open Music Directory";
            btnOpen.UseVisualStyleBackColor = false;
            btnOpen.Click += BtnOpen_Click;
            // 
            // btnClearQueue
            // 
            btnClearQueue.BackColor = System.Drawing.Color.GhostWhite;
            btnClearQueue.Location = new System.Drawing.Point(211, 70);
            btnClearQueue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnClearQueue.Name = "btnClearQueue";
            btnClearQueue.Size = new System.Drawing.Size(91, 29);
            btnClearQueue.TabIndex = 9;
            btnClearQueue.Text = "Clear queue";
            btnClearQueue.UseVisualStyleBackColor = false;
            btnClearQueue.Click += BtnClearQueue_Click;
            // 
            // btnAddSong
            // 
            btnAddSong.Location = new System.Drawing.Point(306, 9);
            btnAddSong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnAddSong.Name = "btnAddSong";
            btnAddSong.Size = new System.Drawing.Size(81, 42);
            btnAddSong.TabIndex = 0;
            btnAddSong.Text = "Add Song";
            btnAddSong.UseVisualStyleBackColor = true;
            btnAddSong.Click += btnAddSong_Click;
            // 
            // btnRemoveSong
            // 
            btnRemoveSong.Location = new System.Drawing.Point(391, 11);
            btnRemoveSong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            btnRemoveSong.Name = "btnRemoveSong";
            btnRemoveSong.Size = new System.Drawing.Size(81, 42);
            btnRemoveSong.TabIndex = 1;
            btnRemoveSong.Text = "Remove Song";
            btnRemoveSong.UseVisualStyleBackColor = true;
            btnRemoveSong.Click += btnRemoveSong_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.GhostWhite;
            ClientSize = new System.Drawing.Size(954, 338);
            Controls.Add(btnAddSong);
            Controls.Add(btnRemoveSong);
            Controls.Add(btnClearQueue);
            Controls.Add(btnOpen);
            Controls.Add(lbTrackQueue);
            Controls.Add(lbTracksQueue);
            Controls.Add(lvFiles);
            Controls.Add(lbConnectionsCount);
            Controls.Add(lbCurrentTrack);
            Controls.Add(lbConnections);
            Controls.Add(lbPlayingNow);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            Name = "MainForm";
            Text = "Music Streamer";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lbPlayingNow;
        private System.Windows.Forms.Label lbConnections;
        private System.Windows.Forms.Label lbCurrentTrack;
        private System.Windows.Forms.Label lbConnectionsCount;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ListBox lbTracksQueue;
        private System.Windows.Forms.Label lbTrackQueue;
        private System.Windows.Forms.ColumnHeader chTitle;
        private System.Windows.Forms.ColumnHeader chArtist;
        private System.Windows.Forms.ColumnHeader chFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.FolderBrowserDialog fbdOpen;
        private System.Windows.Forms.Button btnClearQueue;

        // Кнопка для добавления песен
        private System.Windows.Forms.Button btnAddSong;
        // Кнопка для удаления выбранной песни
        private System.Windows.Forms.Button btnRemoveSong;
    }
}

