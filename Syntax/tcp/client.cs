using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    class ChatTcpclient
    {
        static void StartClient()
        {
            //连接服务端
            TcpClient tcpClient = new TcpClient("127.0.0.1", 500);

            while (true)
            {
                Console.WriteLine("Input Request Message:");
                //发送信息
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] sendMessage = Encoding.UTF8.GetBytes(Console.ReadLine());
                networkStream.Write(sendMessage, 0, sendMessage.Length);
                networkStream.Flush();

                //接收信息
                int bufferSize = 1024;
                byte[] receiveMessage = new byte[bufferSize];
                int count = networkStream.Read(receiveMessage, 0, bufferSize);
                Console.WriteLine(Encoding.UTF8.GetString(receiveMessage).Trim());
            }
        }

        static void Test()
        {
            StartClient();
            Console.ReadKey();
        }
    }
}
