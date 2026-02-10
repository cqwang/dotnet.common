using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.CollectionExt{
    
    public class EntityListChange<T>
    {
        public List<T> ToDelete { get; set; }

        public List<T> ToUpdate { get; set; }

        public List<T> ToInsert { get; set; }

        public EntityListChange(){
            ToDelete = new List<T>();
            ToUpdate = new List<T>();
            ToInsert = new List<T>();
        }
    }
}