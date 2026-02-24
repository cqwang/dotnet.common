using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqwang.BackEnd.CSharp.Syntax
{
    public class InOut
    {
        public static void DoTest()
        {
            Dog dog = new Dog() { Name = "Dog1" };
            List<Dog> dogList = new List<Dog>() { dog };

            //Dog继承自Animal，OK
            Animal animal = dog;

            //List<Dog>不继承自List<Animal>，ERROR：无法隐式转换
            //List<Animal> animalList = dogList;

            //List<Dog>不继承自List<Animal>，ERROR：无法强制转换
            //animalList = (List<Animal>)dogList;

            //Dog继承自Animal，使用Select投影，OK
            List<Animal> animalList = dogList.Select(d => (Animal)d).ToList();

            /*
			 * List<T>实现接口IEnumerable<T>,而IEnumerable<T>中的T用out标记，支持协变，OK
			 * 反编译查看：IEnumerable<Animal> animalEnum = (IEnumerable<Animal>)dogList;
			 * 说明IL并不知道协变和逆变，只能强制类型转换。
			 */
            IEnumerable<Animal> animalEnum = dogList;


            Action<Animal> animalAction = new Action<Animal>(a =>
            {
                Console.WriteLine("My Name is " + (string.IsNullOrEmpty(a.Name) ? string.Empty : a.Name));
            });
            //Action<T>中的占位符T用in修饰，支持逆变，OK
            Action<Dog> dogAction = animalAction;
            dogAction(dog);

            Console.Read();
        }
    }

    public abstract class Animal
    {
        public string Name { get; set; }
    }

    public class Dog : Animal
    {
    }
}
