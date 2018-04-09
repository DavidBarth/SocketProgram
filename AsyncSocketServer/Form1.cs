using BelfastSocketAsync;
using System;
using System.Windows.Forms;

namespace WinFormApp
{
    public partial class Form1 : Form
    {
        BelfastSocketServer server;
        public Form1()
        {
            InitializeComponent();
            server = new BelfastSocketServer();
        }

        private void buttonAccceptAsync_Click(object sender, EventArgs e)
        {            
            server.StartListeningFoIncommingConnection();
        }

        private void sendAllButton_Click(object sender, EventArgs e)
        {
            server.SendToAllClients(messageTextBox.Text);
        }

        private void stopServerButton_Click(object sender, EventArgs e)
        {
            server.StopServer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.StopServer();
        }
    }
}
