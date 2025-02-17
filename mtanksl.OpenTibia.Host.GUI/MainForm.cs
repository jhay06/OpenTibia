﻿using OpenTibia.Game;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mtanksl.OpenTibia.Host.GUI
{
    public partial class MainForm : Form
    {
        private Server server;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", e.LinkText);
        }

        private async void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                MessageBox.Show("Server is already running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Enabled = false;

                await Task.Run( () =>
                {
                    server = new Server();
#if DEBUG
                    server.Logger = new Logger(new RichTextboxLoggerProvider(richTextBox1), LogLevel.Debug);
#else
                    server.Logger = new Logger(new RichTextboxLoggerProvider(richTextBox1), LogLevel.Information);
#endif
                    server.Start();
                } );
            }
            finally
            {
                Enabled = true;

                startToolStripMenuItem.Enabled = false;

                restartToolStripMenuItem.Enabled = true;

                stopToolStripMenuItem.Enabled = true;

                kickAllToolStripMenuItem.Enabled = true;

                saveToolStripMenuItem.Enabled = true;

                maintenanceToolStripMenuItem.Enabled = true;

                maintenanceToolStripMenuItem.Checked = false;
            }
        }

        private async void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                MessageBox.Show("Server is not running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Enabled = false;

                await Task.Run( () =>
                {
                    server.KickAll();

                    server.Save();

                    server.Stop();

                    server.Dispose();

                    server = null;

                    server = new Server();
#if DEBUG
                    server.Logger = new Logger(new RichTextboxLoggerProvider(richTextBox1), LogLevel.Debug);
#else
                    server.Logger = new Logger(new RichTextboxLoggerProvider(richTextBox1), LogLevel.Information);
#endif
                    server.Start();
                } );
            }
            finally
            {
                Enabled = true;

                maintenanceToolStripMenuItem.Checked = false;
            }
        }

        private async void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                MessageBox.Show("Server is not running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Enabled = false;

                await Task.Run( () =>
                {
                    server.KickAll();

                    server.Save();

                    server.Stop();

                    server.Dispose();

                    server = null;
                } );
            }
            finally
            {
                Enabled = true;

                startToolStripMenuItem.Enabled = true;

                restartToolStripMenuItem.Enabled = false;

                stopToolStripMenuItem.Enabled = false;

                kickAllToolStripMenuItem.Enabled = false;

                saveToolStripMenuItem.Enabled = false;

                maintenanceToolStripMenuItem.Enabled = false;

                maintenanceToolStripMenuItem.Checked = false;
            }
        }

        private async void kickAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                MessageBox.Show("Server is not running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Enabled = false;

                await Task.Run( () =>
                {
                    server.KickAll();
                } );
            }
            finally
            {
                Enabled = true;
            }
        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                MessageBox.Show("Server is not running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Enabled = false;

                await Task.Run( () =>
                {
                    server.Save();
                } );
            }
            finally
            {
                Enabled = true;
            }
        }

        private void maintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server.Status == ServerStatus.Running)
            {
                server.Pause();

                maintenanceToolStripMenuItem.Checked = true;
            }
            else if (server.Status == ServerStatus.Paused)
            {
                server.Continue();

                maintenanceToolStripMenuItem.Checked = false;
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                if (MessageBox.Show("Server is running, do you really want to shutdown?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Enabled = false;

                        await Task.Run( () =>
                        {
                            server.KickAll();

                            server.Save();

                            server.Stop();

                            server.Dispose();

                            server = null;
                        } );
                    }
                    finally
                    {
                        Enabled = true;

                        startToolStripMenuItem.Enabled = true;

                        restartToolStripMenuItem.Enabled = false;

                        stopToolStripMenuItem.Enabled = false;

                        kickAllToolStripMenuItem.Enabled = false;

                        saveToolStripMenuItem.Enabled = false;

                        maintenanceToolStripMenuItem.Enabled = false;

                        maintenanceToolStripMenuItem.Checked = false;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
