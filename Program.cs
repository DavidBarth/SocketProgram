﻿using System;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(new IPEndPoint(IPAddress.Any, 23000));
            listenerSocket.Listen(5);

            Console.WriteLine("About to accept incoming connection.");
            
            //BP - using this var to receive data from the client
            Socket client = listenerSocket.Accept();


            Console.WriteLine("Client connected. " + client.ToString() + " - IP End Point: " + client.RemoteEndPoint.ToString());

            byte[] buffer = new byte[128];
            int numberOfReceivedBytes = 0;
            numberOfReceivedBytes = client.Receive(buffer);

            Console.WriteLine("Number of received bytes: " + numberOfReceivedBytes);

            Console.WriteLine("Data sent by client is: " + buffer);


        }
    }
}
