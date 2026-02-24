using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    class ChatTcpServer
    {
        private TcpClient tcpClient;
        private byte[] byteMessage;
        private string clientEndPoint;

        public ChatTcpServer(TcpClient tcpClient)
        {
            //显示客户端信息
            Console.WriteLine("Client's endpoint is " + tcpClient.Client.RemoteEndPoint.ToString());

            this.tcpClient = tcpClient;
            this.byteMessage = new byte[tcpClient.ReceiveBufferSize];

            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.BeginRead(byteMessage, 0, tcpClient.ReceiveBufferSize, new AsyncCallback(ReceiveAsyncCallback), null);//异步读取信息
        }

        /// <summary>
        /// 读取信息并返回响应到客户端
        /// </summary>
        /// <param name="iAsyncResult"></param>
        private void ReceiveAsyncCallback(IAsyncResult iAsyncResult)
        {
            //OnThreadActiveEvent("Message is receiving");

            int length = tcpClient.GetStream().EndRead(iAsyncResult);//结束异步读取
            if (length < 1)
            {
                //如果接收到的数据长度少于1则抛出异常
                tcpClient.GetStream().Close();
                throw new Exception("Disconnection!");
            }

            //显示接收信息
            string message = Encoding.UTF8.GetString(this.byteMessage, 0, length);
            Console.WriteLine("Message:" + message);

            byte[] sendMessage = Encoding.UTF8.GetBytes("Message is received!");
            tcpClient.GetStream().BeginWrite(sendMessage, 0, sendMessage.Length, new AsyncCallback(SendAsyncCallback), null);//异步发送信息
        }

        /// <summary>
        /// 返回响应到客户端
        /// </summary>
        /// <param name="iAsyncResult"></param>
        private void SendAsyncCallback(IAsyncResult iAsyncResult)
        {
            //OnThreadActiveEvent("Message is sending");

            tcpClient.GetStream().EndWrite(iAsyncResult); //结束异步发送
            tcpClient.GetStream().BeginRead(byteMessage, 0, tcpClient.ReceiveBufferSize, new AsyncCallback(ReceiveAsyncCallback), null); //重新监听
        }

        private static void StartService()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            TcpListener tcpListener = new TcpListener(ipAddress, 500);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                if (tcpClient != null)
                    new ChatTcpServer(tcpClient);//实现监听
            }
        }

        public static void Test()
        {
            StartService();
            Console.ReadKey();
        }
    }
}
