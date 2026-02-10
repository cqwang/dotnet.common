using Cqwang.BackEnd.CSharp.Algorithm.Combination;
using Cqwang.BackEnd.CSharp.Algorithm.Domain;
using Cqwang.BackEnd.CSharp.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TestCase
{
    public class CombinationTest
    {
        public static void DoTest()
        {
            var list = new List<Resource>();
            list.Add(new Resource() { ResourceID = 1, GroupID = 1001 });
            list.Add(new Resource() { ResourceID = 4, GroupID = 1001 });
            list.Add(new Resource() { ResourceID = 7, GroupID = 1001 });
            list.Add(new Resource() { ResourceID = 2, GroupID = 1002 });
            list.Add(new Resource() { ResourceID = 5, GroupID = 1002 });
            list.Add(new Resource() { ResourceID = 8, GroupID = 1002 });
            list.Add(new Resource() { ResourceID = 6, GroupID = 1003 });
            list.Add(new Resource() { ResourceID = 9, GroupID = 1003 });
            list.Add(new Resource() { ResourceID = 3, GroupID = 1004 });

            var groupMap = list.ToLookupMap(p => p.GroupID);
            var combinations = CombinationFactory.Create(groupMap);
            foreach (var combination in combinations)
            {
                Console.WriteLine(string.Join(",", combination.SelectedResources.Select(p => p.ResourceID.ToString())));
            }

            Console.ReadKey();
        }
    }
}
