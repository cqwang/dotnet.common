
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.SerialiserExt
{
    /// <summary>
    /// 自定义基本数据类型的序列化和反序列化方法
    /// 
    /// 一个字节就是八个二进制位：
    /// bool、byte  ：1个字节
    /// char、ushort：2个字节
    /// int         ：4个字节
    /// long、double：8个字节
    /// decimal     ：16字节，不遵守四舍五入的十进制数，有28个有效数字，常用于财务计算
    /// 
    /// 
    /// 待改进：加ref参数传引用、修改值
    /// </summary>
    public class BinarySerializerObj
    {
        /// <summary>
        /// 序列化布尔值
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="b"></param>
        public static void WriteBoolean(BinaryWriter writer, bool b)
        {
            writer.Write(b);
        }

        /// <summary>
        /// 反序列化布尔值
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static bool ReadBoolean(BinaryReader reader)
        {
            return reader.ReadBoolean();
        }

        /// <summary>
        /// 序列化字节
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="b"></param>
        public static void WriteByte(BinaryWriter writer, byte b)
        {
            writer.Write(b);
        }

        /// <summary>
        /// 反序列化字节
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static byte ReadByte(BinaryReader reader)
        {
            return reader.ReadByte();
        }

        /// <summary>
        /// 序列化字符
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="c"></param>
        public static void WriteChar(BinaryWriter writer, char c)
        {
            writer.Write(c);
        }

        /// <summary>
        /// 反序列化字符
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static char ReadChar(BinaryReader reader)
        {
            return reader.ReadChar();
        }

        /// <summary>
        /// 序列化无符号短整型
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="u"></param>
        public static void WriteUshort(BinaryWriter writer, ushort u)
        {
            writer.Write(u);
        }

        /// <summary>
        /// 反序列化无符号短整型
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static ushort ReadUshort(BinaryReader reader)
        {
            return reader.ReadUInt16();
        }

        /// <summary>
        /// 序列化有符号整型
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="i"></param>
        public static void WriteInt(BinaryWriter writer, int i)
        {
            writer.Write(i);
        }

        /// <summary>
        /// 反序列化有符号整型
        /// </summary>
        /// <param name="reader"></param>
        public static int ReadInt(BinaryReader reader)
        {
            return reader.ReadInt32();
        }

        /// <summary>
        /// 序列化长整型
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="l"></param>
        public static void WriteLong(BinaryWriter writer, long l)
        {
            writer.Write(l);
        }

        /// <summary>
        /// 反序列化整型
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static long ReadLong(BinaryReader reader)
        {
            return reader.ReadInt64();
        }

        /// <summary>
        /// 序列化浮点型
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="d"></param>
        public static void WriteDouble(BinaryWriter writer, double d)
        {
            writer.Write(d);
        }

        /// <summary>
        /// 反序列化浮点型
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static double ReadDouble(BinaryReader reader)
        {
            return reader.ReadDouble();
        }

        /// <summary>
        /// 序列化高精度十进制数
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="d"></param>
        public static void WriteDecimal(BinaryWriter writer, decimal d)
        {
            writer.Write(d);
        }

        /// <summary>
        /// 反序列化高精度十进制数
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static decimal ReadDecimal(BinaryReader reader)
        {
            return reader.ReadDecimal();
        }


        public static void WriteString(BinaryWriter writer, ref string str)
        {
            if (string.IsNullOrEmpty(str))
                writer.Write(0);
            else
            {
                writer.Write(str.Length);
                writer.Write(Encoding.Default.GetBytes(str));
            }
        }

        public static void ReadString(BinaryReader reader, ref string str)
        {
            int len = reader.ReadInt32();
            if (len == 0)
            {
                str = null;
                return;
            }

            byte[] bytes = reader.ReadBytes(len);
            str = string.Intern(Encoding.Default.GetString(bytes));
        }
    }
}
