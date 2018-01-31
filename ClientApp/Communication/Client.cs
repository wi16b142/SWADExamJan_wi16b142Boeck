using GalaSoft.MvvmLight;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientApp.Communication
{
    public class Client : ViewModelBase
    {
        const int port = 10100;
        Socket clientSocket;

        public Client()
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Loopback, port);
            clientSocket = client.Client;

        }
        public void Send(string msg)
        {
            clientSocket.Send(Encoding.UTF8.GetBytes(msg));
        }
    }
}
