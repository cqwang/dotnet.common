using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.TestCase
{
    class TestPropertyGetterSetter
    {
        static void Main(string[] args)
        {
            TryTestPropertyGetterSetter();

            Console.Read();
        }
        private static void TryTestPropertyGetterSetter()
        {
            var baseEntity = new Product();
            DoTestPropertyGetterSetter((entity, productID) =>
            {
                entity.ProductID = productID;
            }, baseEntity, "直接赋值");


            var propertyInfo = typeof(Product).GetProperty("ProductID");
            var emitSetter = propertyInfo.CreateSetter();
            DoTestPropertyGetterSetter((entity, productId) =>
            {
                emitSetter(entity, productId);

            }, baseEntity, "Emit创建弱类型委托赋值");

            var genericitySetter = new SetterWrapper<Product, int>(propertyInfo);
            DoTestPropertyGetterSetter((entity, productId) =>
            {
                genericitySetter.SetValue(entity, productId);
            }, baseEntity, "强类型委托赋值");

            var objectSetter = GetterSetterFactory.CreatePropertySetterWrapper(propertyInfo);
            DoTestPropertyGetterSetter((Product entity, int productId) =>
            {
                objectSetter.Set(entity, productId);
            }, baseEntity, "通用委托赋值");

            propertyInfo.CachedSetValue(baseEntity, 1);
            DoTestPropertyGetterSetter((Product entity, int productId) =>
            {
                propertyInfo.CachedSetValue(entity, productId);
            }, baseEntity, "通用缓存委托赋值");

            propertyInfo.CachedSetValue(baseEntity, 1);
            DoTestPropertyGetterSetter((Product entity, int productId) =>
            {
                propertyInfo.CachedSetValue2(entity, productId);
            }, baseEntity, "通用缓存2委托赋值");

            DoTestPropertyGetterSetter((entity, productId) =>
            {
                propertyInfo.SetValue(entity, productId, null);
            }, baseEntity, "纯反射赋值");
        }

        private static void DoTestPropertyGetterSetter(Action<Product, int> action, Product baseEntity, string title)
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                action(baseEntity, i);
            }
            sw.Stop();
            Console.WriteLine(title + ": " + sw.ElapsedMilliseconds);
        }
    }
}
