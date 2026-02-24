using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax.Serialization
{
    public class Person2 : ISerializable
    {
        public string name;
        public string sex;

        [NonSerialized]
        public int Age;

        public Person2(SerializationInfo info, StreamingContext context)
        {
            this.name = info.GetString("name");
            //this.sex = info.GetString("sex");
            //age = (int)info.GetValue("age", typeof(int)); 
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", name);
            //info.AddValue("sex", sex);
        }
    }
}
