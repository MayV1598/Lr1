using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lr1A_2_
{
    class Program
    {
        const int ECHO_Port = 8080;
        public static int nClients = 0;

        static void Main(string[] args)
        {
            try
            {
                TcpListener clientListener = new TcpListener(ECHO_Port);

                clientListener.Start();

                Console.WriteLine("Waiting for connections...");

                while(true)
                {
                    TcpClient client = clientListener.AcceptTcpClient();

                    ClientHandler cHandler = new ClientHandler();

                    cHandler.clientSocket = client;

                    Thread clientThread = new Thread(new ThreadStart(cHandler.RunClient));
                    clientThread.Start();
                    nClients++;
                }

                clientListener.Stop();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception : " + exp);
            }
        }
    }
}
