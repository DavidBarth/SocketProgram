using System.Net;
using System.Net.Sockets;

namespace BelfastAsync
{
    public class BelfastSocketServer
    {
        IPAddress myIP;
        int myPort;
        //helper class
        TcpListener myTCPListener;

        public async void StartListeningFoIncommingConnection(IPAddress ipAddress = null, int port = 23000)
        {
            //sanity check
            if(ipAddress == null)
            {
                ipAddress = IPAddress.Any;
            }

            if(port <= 0)
            {
                port = 23000;
            }

            myIP = ipAddress;
            myPort = port;

            System.Diagnostics.Debug.WriteLine(string.Format("IP Adress: {0} - Port: {1}", myIP.ToString(), myPort.ToString()));

            myTCPListener = new TcpListener(myIP, myPort);
            myTCPListener.Start();

            var returnedByAccept =  await myTCPListener.AcceptTcpClientAsync(); //returns a TCPClient (helper class)

            System.Diagnostics.Debug.WriteLine("Client connected successfully: " + returnedByAccept.ToString());
        }
    }
}
