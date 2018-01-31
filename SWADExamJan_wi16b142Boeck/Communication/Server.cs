using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SWADExamJan_wi16b142Boeck.Communication
{
    public class Server : ViewModelBase
    {
        Socket serverSocket;
        List<ClientHandler> Clients = new List<ClientHandler>();
        Thread acceptingThread;
        Action<string> GuiUdpate;
        const int port = 10100;

        public Server(Action<string> guiUdpate)
        {
            GuiUdpate = guiUdpate;

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
            serverSocket.Listen(10);

            StartAccepting();

        }

        private void StartAccepting()
        {
            acceptingThread = new Thread(Accept);
            acceptingThread.IsBackground = true;
            acceptingThread.Start();
        }

        private void Accept()
        {
            while (acceptingThread.IsAlive)
            {
                Clients.Add(new ClientHandler(serverSocket.Accept(), GuiUdpate));
            }
        }



    }
}
