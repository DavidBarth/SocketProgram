﻿using System;
using System.Net;
using System.Net.Sockets;

namespace BelfastAsync
{
    class BelfastSocketClient
    {
        IPAddress myServerIPAddress;
        int serverPort;
        TcpClient myClient;

        public IPAddress MyServerIPAddress
        {
            get
            {
                return myServerIPAddress;
            }
        }

        public bool SetServerIPAddress(string ipAddress)
        {
            IPAddress ip = null;
            if(!IPAddress.TryParse(ipAddress, out ip))
            {
                Console.WriteLine("Invalid server IP supplied.");
                return false;
            }
            myServerIPAddress = ip;
            return true;
        }

        public int ServerPort
        {
            get
            {
                return serverPort;
            }
        }

        public bool SetPortNumber(string portNumber)
        {
            int portNr = 0;
            if (!int.TryParse(portNumber, out portNr))
            {
                Console.WriteLine("Invalid port number supplied.");
                return false;
            }
            if(portNr <= 0 || portNr > 65535)
            {
                Console.WriteLine("Port number must be between 0 and 65535");
            }
            serverPort = portNr;
            return true;
        }

        public TcpClient MyClient
        {
            get
            {
                return myClient;
            }
        }

        public BelfastSocketClient()
        {
            myClient = null;
            serverPort = -1;
            myServerIPAddress = null;
        }
    }
}