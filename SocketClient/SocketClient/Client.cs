using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SocketClient
{
    public class Client : INotifyPropertyChanged
    {
        public Client()
        {
            serverIP = string.Empty;
            serverPort = string.Empty;
            requestedFile = "test.txt";
            filePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            message = string.Empty;
        }

        string serverIP;
        string serverPort;
        string requestedFile;
        string filePath;
        string message;

        public string ServerIP
        {
            set
            {
                serverIP = value;
                NotifyPropertyChanged("ServerIP");
            }
            get { return serverIP; }
        }

        public string ServerPort
        {
            set
            {
                serverPort = value;
                NotifyPropertyChanged("ServerPort");
            }
            get { return serverPort; }
        }

        public string RequestedFile
        {
            set
            {
                requestedFile = value;
                NotifyPropertyChanged("RequestedFile");
            }
            get { return requestedFile; }
        }

        public string FilePath
        {
            set
            {
                filePath = value;
                NotifyPropertyChanged("FilePath");
            }
            get { return filePath; }
        }

        public string Message
        {
            set
            {
                message = value;
                NotifyPropertyChanged("Message");
            }
            get { return message; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RequestDownloadFile()
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                string requestedFile = string.Empty;
                string fileSavePath = string.Empty;
                byte[] bytes = new byte[1024];
                try
                {
                    IPHostEntry host = Dns.GetHostEntry("localhost");
                    IPAddress ipAddress = host.AddressList.Length > 0 ? host.AddressList[0] : IPAddress.Parse("127.0.0.1");
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    Socket socketSender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                    socketSender.Connect(remoteEP);
                    var serverInfo = socketSender.RemoteEndPoint as IPEndPoint;

                    ServerIP = serverInfo.Address.ToString();
                    ServerPort = serverInfo.Port.ToString();
                    requestedFile = RequestedFile;
                    fileSavePath = FilePath;

                    byte[] msg = Encoding.ASCII.GetBytes($"{requestedFile}");
                    int bytesSent = socketSender.Send(msg);
                    int bytesRec = socketSender.Receive(bytes);

                    var filePath = FilePath;
                    var fileName = RequestedFile;

                    var receivedData = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    string pattern = @"SUCCESS";
                    Match match = Regex.Match(receivedData, pattern);
                    if (match.Success)
                    {
                        SaveFileToTargetDirectory(filePath, fileName, socketSender);
                        Message += $"File download success !!\n";
                    }
                    else
                    {
                        Message += $"{receivedData}";
                        Message += $"Download file : {requestedFile} fail.\nPlease try again.\n";
                    }

                    socketSender.Shutdown(SocketShutdown.Both);
                    socketSender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Message += $"ArgumentNullException : {ane.ToString()}";
                }
                catch (SocketException se)
                {
                    Message += $"SocketException : {se.ToString()}";
                }
                catch (Exception ex)
                {
                    Message += $"Unexpected exception : {ex.ToString()}";
                }
            }));

            thread.Start();
        }

        private void SaveFileToTargetDirectory(string filePath, string fileName, Socket socket)
        {
            try
            {
                var validFilePath = Path.GetFullPath(filePath);

                if (!Directory.Exists(validFilePath))
                {
                    Directory.CreateDirectory(validFilePath);
                }

                var targetFile = File.Exists($@"{validFilePath}\\{fileName}")
                                ? $@"{filePath}\\{DateTime.Now.ToString("yyyyMMddhhmmss")}_{fileName}"
                                : $@"{validFilePath}\\{fileName}";
                FileStream fout = new FileStream(targetFile, FileMode.Create, FileAccess.Write);
                var buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = socket.Receive(buffer)) > 0)
                {

                    fout.Write(buffer, 0, bytesRead);
                }
                fout.Close();
            }
            catch (Exception e)
            {
                Message += $"A Exception occured in file saving: {e.ToString()}";
            }
        }
    }
}
