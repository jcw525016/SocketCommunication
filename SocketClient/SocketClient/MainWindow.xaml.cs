using SocketClient;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SocketTestClient
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client client;

        public MainWindow()
        {
            InitializeComponent();
            client = new Client();
            this.DataContext = client;
        }

        private void FileDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (tbRequestedFile.Text == string.Empty)
            {
                tbRequestedFile.Focus();
                return;
            }
            
            if(tbFilePath.Text == string.Empty)
            {
                tbFilePath.Focus();
                return;
            }

            client.RequestDownloadFile();
        }


        private void TbMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbMessage.ScrollToEnd();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbFilePath.Text = dialog.SelectedPath;
            }
        }
    }
}
