using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class BeginEndOperateFile
    {
        private const int BufferSize = 1024;

        /// <summary>
		/// 异步写入文件
		/// </summary>
		public static void WriteFileAsync(string filePathName)
        {
            string message = "An operating-system ThreadId has no fixed relationship........";
            var bytes = Encoding.Unicode.GetBytes(message);

            var stream = new FileStream(filePathName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, BufferSize, true);

            //启动异步写入
            stream.BeginWrite(bytes, 0, (int)bytes.Length, new AsyncCallback(CallbackWrite), stream);
            stream.Flush();
        }

        /// <summary>
        /// 回调函数，异步结束写文件操作
        /// </summary>
        /// <param name="result"></param>
        static void CallbackWrite(IAsyncResult result)
        {
            var stream = result.AsyncState as FileStream; //获取外部数据并转化为所需类型
            if (stream != null)
            {
                stream.EndWrite(result);//结束异步写入
                stream.Close();
            }
        }


        /// <summary>
		/// 异步读取文件
		/// </summary>
		public static void ReadFileAsync(string filePathName)
        {
            var stream = new FileStream(filePathName, FileMode.OpenOrCreate,  FileAccess.ReadWrite, FileShare.ReadWrite, BufferSize, true);
            var byteData = new byte[stream.Length];
            //把FileStream对象,byte[]对象，长度等有关数据绑定到FileData对象中，以附带属性方式送到回调函数
            var fileData = new FileData();
            fileData.Stream = stream;
            fileData.Length = (int)stream.Length;
            fileData.ByteData = byteData;

            //启动异步读取
            stream.BeginRead(byteData, 0, fileData.Length, new AsyncCallback(CallbackRead), fileData);
        }

        /// <summary>
        /// 回调函数，异步结束读文件操作
        /// </summary>
        /// <param name="result"></param>
        static void CallbackRead(IAsyncResult result)
        {
            //把AsyncResult.AsyncState转换为FileData对象，以FileStream.EndRead完成异步读取
            var fileData = result.AsyncState as FileData;
            if (fileData == null)
                return;

            int length = fileData.Stream.EndRead(result);
            fileData.Stream.Close();

            //如果读取到的长度与输入长度不一致，则抛出异常
            if (length != fileData.Length)
                throw new Exception("Stream is not complete!");

            string data = Encoding.Unicode.GetString(fileData.ByteData, 0, fileData.Length);
            Console.WriteLine(data);
        }
    }

    /// <summary>
    /// 传给回调函数的外部数据对象
    /// </summary>
    public class FileData
    {
        public FileStream Stream;
        public int Length;
        public byte[] ByteData;
    }
}
