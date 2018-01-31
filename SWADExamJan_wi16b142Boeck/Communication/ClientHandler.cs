using GalaSoft.MvvmLight;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SWADExamJan_wi16b142Boeck.Communication
{
    public class ClientHandler : ViewModelBase
    {
        private Socket clientSocket;
        private Action<string> guiUdpate;
        byte[] buffer = new byte[512];
        Thread receivingThread;
        string username = "";


        public ClientHandler(Socket socket, Action<string> guiUdpate)
        {
            clientSocket = socket;
            this.guiUdpate = guiUdpate;

            StartReceiving();
        }

        private void StartReceiving()
        {
            receivingThread = new Thread(Receive);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        private void Receive()
        {
            while (receivingThread.IsAlive)
            {
                int length = clientSocket.Receive(buffer);
                string msg = Encoding.UTF8.GetString(buffer, 0, length);

                if(username == "")
                {
                    username = msg.Split(':')[0]; //store ship id from the very first message as username - for future purposes
                }

                guiUdpate(msg); //tell gui about the new message

            }
        }
    }
}
