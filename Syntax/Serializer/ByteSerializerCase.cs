using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax.Serializer
{
    /// <summary>
    /// 历史行情价格
    /// </summary>
    public struct HistoryQuotationPriceStruct
    {
        public double Open;
        public double High;
        public double Low;
        public double Close;

        public void Subtraction(double value)
        {
            Open -= value;
            High -= value;
            Low -= value;
            Close -= value;
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Open);
            writer.Write(High);
            writer.Write(Low);
            writer.Write(Close);
        }

        public void DeSerialize(BinaryReader reader)
        {
            Open = reader.ReadDouble();
            High = reader.ReadDouble();
            Low = reader.ReadDouble();
            Close = reader.ReadDouble();
        }
    }
}
