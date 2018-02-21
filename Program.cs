using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

            while (true)
            {
                numberOfReceivedBytes = client.Receive(buffer);

                Console.WriteLine("Number of received bytes: " + numberOfReceivedBytes);

                string receivedText = Encoding.ASCII.GetString(buffer, 0, buffer.Length);

                Console.WriteLine("Data send by client is: " + receivedText);

                client.Send(buffer);

                Console.WriteLine("Data sent by Server is: " + receivedText);

                if (receivedText == "x") //end mean loop
                {
                    break;
                }

                Array.Clear(buffer,0,buffer.Length);
                numberOfReceivedBytes = 0;
            }
        }
    }
}
