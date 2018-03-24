using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        List<TcpClient> myClients;

        private bool isRunning;

        public BelfastSocketServer()
        {
            myClients = new List<TcpClient>(); //collection of client objects
        }
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

            Debug.WriteLine(string.Format("IP Adress: {0} - Port: {1}", myIP.ToString(), myPort.ToString()));

            myTCPListener = new TcpListener(myIP, myPort);
            try
            {
                myTCPListener.Start();

                isRunning = true;
                while (isRunning)
                {
                    var returnedByAccept = await myTCPListener.AcceptTcpClientAsync(); //returns a TCPClient (helper class)
                    myClients.Add(returnedByAccept);
                    Debug.WriteLine(string.Format("Client connected successfully, number of clients connected {0} - "
                        ,myClients.Count, returnedByAccept.Client.RemoteEndPoint));

                    TakeCareOfTcpClient(returnedByAccept);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        //make method async to be able to perfom async read op in the method
        private async void TakeCareOfTcpClient(TcpClient returnedByAccept)
        {
            NetworkStream networkStream;
            StreamReader networkStreamReader;

            try
            {
                networkStream = returnedByAccept.GetStream();
                networkStreamReader = new StreamReader(networkStream);

                char[] buff = new char[64];

                while (isRunning)
                {
                    Debug.WriteLine("-----Ready to Read");
                    int readReturn = await networkStreamReader.ReadAsync(buff, 0, buff.Length);
                    Debug.WriteLine("Returned: " + readReturn);

                    if(readReturn == 0)
                    {
                        Debug.WriteLine("Socket disconnected");
                        RemoveConnectedClient(returnedByAccept);
                        break;
                    }

                    string receivedText = new string(buff);
                    Debug.WriteLine("-----Received text: " + receivedText);

                    Array.Clear(buff, 0, buff.Length);

                }
            }
            catch(Exception e)
            {
                RemoveConnectedClient(returnedByAccept);
                Debug.WriteLine(e.ToString());
            }
        }

        private void RemoveConnectedClient(TcpClient returnedByAccept)
        {
            myClients.Remove(returnedByAccept);
        }
    }
}
