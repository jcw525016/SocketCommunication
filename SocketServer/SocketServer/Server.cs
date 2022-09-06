using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace SocketServer
{
    public class Server : INotifyPropertyChanged
    {
        public Server()
        {
            logs = string.Empty;
            requestFile = string.Empty;
            clientIP = string.Empty;
            clientPort = string.Empty;
        }

        private Thread thread;

        string logs;
        public string Logs
        {
            set
            {
                logs = value;
                NotifyPropertyChanged("Logs");
            }
            get { return logs; }
        }

        string requestFile;
        public string RequestFile
        {
            set
            {
                requestFile = value;
                NotifyPropertyChanged("RequestFile");
            }
            get { return requestFile; }
        }

        string clientIP;
        public string ClientIP
        {
            set
            {
                clientIP = value;
                NotifyPropertyChanged("ClientIP");
            }
            get { return clientIP; }
        }

        string clientPort;
        public string ClientPort
        {
            set
            {
                clientPort = value;
                NotifyPropertyChanged("ClientPort");
            }
            get { return clientPort; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void Start()
        {
            thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    IPHostEntry host = Dns.GetHostEntry("localhost");
                    IPAddress ipAddress = host.AddressList.Length > 0 ? host.AddressList[0] : IPAddress.Parse("127.0.0.1");
                    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

                    Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    listener.Bind(localEndPoint);
                    listener.Listen(10);

                    Logs += ("Waiting for a connection...\n");

                    string data = null;
                    byte[] bytes = null;

                    while (true)
                    {
                        Socket handler = listener.Accept();
                        bytes = new byte[1024];

                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.Length > 0)
                        {
                            Logs += ($"Client requests for : {data}\n");
                            RequestFile = data;

                            var requestFilePath = GetRequestFilePath(data);

                            var incomingClient = handler.RemoteEndPoint as IPEndPoint;

                            ClientIP = incomingClient.Address.ToString();
                            ClientPort = incomingClient.Port.ToString();
                            Logs += (requestFilePath.Length > 0
                                    ? $"Server file path :{requestFilePath}\n"
                                    : $"Can not find {RequestFile} under project directory.\n");


                            if (requestFilePath == string.Empty)
                            {
                                byte[] msg = Encoding.ASCII.GetBytes($"ERROR : Can not find requested file.\n");
                                handler.Send(msg);
                            }
                            else
                            {
                                byte[] msg = Encoding.ASCII.GetBytes($"SUCCESS : Found reequested file.\n");
                                handler.Send(msg);
                                handler.SendFile(requestFilePath);
                            }
                            handler.Shutdown(SocketShutdown.Both);
                            handler.Close();
                            data = null;
                            bytes = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logs += (ex.ToString());
                }
            }))
            {
                IsBackground = true
            };

            thread.Start();
        }

        private string GetRequestFilePath(string fileName)
        {
            if (fileName != string.Empty)
            {
                var filePath = $@"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\{fileName}";
                if (File.Exists(filePath))
                {
                    return filePath;
                }
            }

            return string.Empty;
        }
    }
}
