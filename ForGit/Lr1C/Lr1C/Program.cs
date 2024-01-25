using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Lr1C
{
    class Program
    {

        static void Main(string[] args)
        {
            TcpListener clientListener = new TcpListener(IPAddress.Loopback, 8080);

            clientListener.Start();

            TcpClient tcpClient = clientListener.AcceptTcpClient();

            StreamReader readerStream = new StreamReader(tcpClient.GetStream());
            NetworkStream writerStream = tcpClient.GetStream();

            string returnData = readerStream.ReadLine();

            string data = "HTTP/1.1 200 OK\nDate: Wed, 11 Feb 2009 11:20:59 GMT\nServer: Apache\nLast-Modified: Wed, 11 Feb 2021 11:20:59 GMT\nContent-Type: text/html; charset=utf-8\nContent-Length: 1234 \n\n<!DOCTYPE html>\n<html>\n<body>\n<h1>My First Heading</h1>\n<p>My first paragraph.</p>\r\n</body>\r\n</html>";
            byte[] dataWrite = Encoding.UTF8.GetBytes(data);
            writerStream.Write(dataWrite,0,dataWrite.Length);
            
            tcpClient.Close();
            clientListener.Stop();
            Console.ReadKey();
        }
    }
}
