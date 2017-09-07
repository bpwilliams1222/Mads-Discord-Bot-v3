using Discord;
using MadBot_3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MadBot_3
{
    public partial class frmDiscordBot : Form
    {
        public frmDiscordBot()
        {
            InitializeComponent();
        }

        public static StorageModel Storage;
        public static MadBot Bot;
        Thread botThread;

        private void Form1_Load(object sender, EventArgs e)
        {
            Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void btnStopBot_Click(object sender, EventArgs e)
        {
            btnStartBot.Enabled = true;
            btnStopBot.Enabled = false;
            Stop();
        }

        private void btnStartBot_Click(object sender, EventArgs e)
        {
            btnStopBot.Enabled = true;
            btnStartBot.Enabled = false;
            Start();
        }

        private void Stop()
        {
            Storage.StopProcesses();
            botThread.Abort();
            Storage = null;
            Bot = null;
        }

        private void Start()
        {
            Storage = new StorageModel();
            Bot = new MadBot();
            botThread = new Thread(() => Bot.Main());
            botThread.Start();
            Thread.Sleep(5000);

            foreach (var channel in Bot.channels.LoCServer.TextChannels)
            {
                ddChannelSelection.Items.Add(channel.Name);
            }

            foreach (var user in Bot.channels.LoCServer.Users)
            {
                if (!user.Name.Contains("MadBot"))
                {
                    ddUserSelection.Items.Add(user.Name);
                }
            }

            btnStartBot.Enabled = false;
        }

        private async void btnSendChannelMessage_Click(object sender, EventArgs e)
        {
            if(ddChannelSelection.SelectedItem != null && (txtChanMessage.Text != "" || txtChanMessage.Text != null))
            {
                var channel = Bot.channels.LoCServer.TextChannels.FirstOrDefault(c => c.Name == (string)ddChannelSelection.SelectedItem);
                if(channel != null)
                {
                    await channel.SendMessage(txtChanMessage.Text);
                }
            }
        }

        private async void btnSendUserMessage_Click(object sender, EventArgs e)
        {
            if (ddUserSelection.SelectedItem != null && (txtUserMessage.Text != "" || txtUserMessage.Text != null))
            {
                var user = Bot.channels.LoCServer.Users.FirstOrDefault(c => c.Name == (string)ddUserSelection.SelectedItem);
                if (user != null)
                {
                    await user.SendMessage(txtUserMessage.Text);
                }
            }
        }
    }
}
