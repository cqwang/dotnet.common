using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.MathExt
{
    public class Resource
    {
        public int ResourceID;

        public long GroupID;
    }

    public class ResourceCombination
    {
        public List<Resource> SelectedResources;
    }
}
