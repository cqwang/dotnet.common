using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class ComplexLinq
    {
        public static void Test()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student() { Name = "张一", Score = 80, Sex = "男" });
            students.Add(new Student() { Name = "张二", Score = 90, Sex = "女" });
            students.Add(new Student() { Name = "张三", Score = 88, Sex = "男" });
            students.Add(new Student() { Name = "张四", Score = 98, Sex = "女" });
            students.Add(new Student() { Name = "张五", Score = 100, Sex = "男" });

            var groupInfos = from student in students
                             group student by student.Sex into studentGroup //分组
                             select new { Sex = studentGroup.Key, Count = studentGroup.Count(), AvgScore = studentGroup.Average(p => p.Score) }; //匿名类型（也可显示定义后在这里使用），分组统计

            foreach (var groupInfo in groupInfos)
            {
                Console.WriteLine(string.Format("{0} {1} {2}", groupInfo.Sex, groupInfo.Count, groupInfo.AvgScore));
            }

            Console.Read();
        }
    }

    public class Student
    {
        public int Score { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
    }
}
