using System;
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

        private bool isRunning;

        //async will create code behing
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
            try
            {
                myTCPListener.Start();

                isRunning = true;
                while (isRunning)
                {
                    var returnedByAccept = await myTCPListener.AcceptTcpClientAsync(); //returns a TCPClient (helper class)
                    TakeCareOfTcpClient(returnedByAccept);
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        private void TakeCareOfTcpClient(TcpClient returnedByAccept)
        {
            System.Diagnostics.Debug.WriteLine("Client connected successfully - " + " Local Endpoint: " + returnedByAccept.Client.LocalEndPoint.ToString() + ", Remote Endpoint: " + returnedByAccept.Client.RemoteEndPoint.ToString());
        }
    }
}
