using SocketServer;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;


namespace SocketTestServer
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private Server server;
        public MainWindow()
        {
            InitializeComponent();
            server = new Server();
            this.DataContext = server;
        }

        private void BtnStartServer_Click(object sender, RoutedEventArgs e)
        {
            btnStartServer.IsEnabled = false;
            server.Start();
        }

        private void TbLogs_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            tbLogs.ScrollToEnd();
        }
    }
}
