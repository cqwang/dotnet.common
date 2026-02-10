
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TestCase
{
    class Program
    {

        static void Main(string[] args)
        {
            var value = 2;
            var unit = "days";
            var d = APIParamDateTypeAnalyzer.GetRegularLastTime(value, unit);

            //var aList = InitAList();
            //List<B> bList = Mapper.DynamicMap<List<B>>(aList);


            //Mapper.CreateNestedMapper(typeof(A), typeof(B));
            //var aList = InitAList();
            //var bList = Mapper.Map<List<B>>(aList);
        }
    }
}
