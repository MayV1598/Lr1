using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Lr1A
{
    class Program
    {
        const int ECHO_PORT = 8080;

        static void Main(string[] args)
        {
            Console.WriteLine("Your Name:");
            string name = Console.ReadLine();
            Console.WriteLine("---Logged In---");

            try
            {
                TcpClient eClient = new TcpClient("127.0.0.1",ECHO_PORT);

                StreamReader readerSteam = new StreamReader(eClient.GetStream());
                NetworkStream writerStream = eClient.GetStream();

                string dataToSend;

                dataToSend = name;
                dataToSend += "\r\n";

                byte[] data = Encoding.ASCII.GetBytes(dataToSend);

                writerStream.Write(data, 0, data.Length);

                while(true)
                {
                    Console.WriteLine(name + ":");

                    dataToSend = Console.ReadLine();
                    dataToSend += "\r\n";

                    data = Encoding.ASCII.GetBytes(dataToSend);
                    writerStream.Write(data,0,data.Length);

                    if (dataToSend.IndexOf("QUIT") > 1)
                    {
                        break;
                    }

                    string returnData;

                    returnData = readerSteam.ReadLine();

                    Console.WriteLine("Server: " + returnData);
                }

                eClient.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception: " + exp.Message);
            }
        }
    }
}
