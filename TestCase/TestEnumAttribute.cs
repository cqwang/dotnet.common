using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TestCase
{
    class TestEnumAttribute
    {
        public static void Main(string[] args)
        {
            var map = ReflectionExt.LoadEnumAttributeMapFrom<MyEnumAttribute, DescriptionGroupAttribute>();
        }

    }



    enum MyEnumAttribute
    {
        [DescriptionGroup("酒店",1)]
        Hotel,

        [DescriptionGroup("机票", 1)]
        Flight
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class DescriptionGroupAttribute : Attribute
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        private int _group;
        public int Group
        {
            get
            {
                return _group;
            }
        }

        public DescriptionGroupAttribute(string name, int group)
        {
            _name = name;
            _group = group;
        }
    }
}
