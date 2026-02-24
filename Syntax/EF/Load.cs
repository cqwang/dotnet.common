using Cqwang.BackEnd.CSharp.Syntax.TestCase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    public class Load
    {

        /// <summary>
        /// 延迟加载
        /// </summary>
        static void DefaultDelayLoadOp()
        {
            using (var context = new DbContext("ConnectionString"))
            {
                var product = context.Set<ProductBaseEntity>().FirstOrDefault(p => p.ProductID == 1);//不会加载Image信息
                var uri = product.Image.ImageURI;//使用到，会加载Image信息
            }
        }

        /// <summary>
        /// 手动加载
        /// </summary>
        static void ManualLoadOp()
        {
            using (var context = new DbContext("ConnectionString"))
            {
                context.Configuration.LazyLoadingEnabled = false;//或者Image属性的virtual去掉，不启动默认延迟加载

                var product = context.Set<ProductBaseEntity>().FirstOrDefault(p => p.ProductID == 1);//不会加载Image信息
                var uri = product.Image.ImageURI;//空引用异常

                context.Entry(product).Reference(p => p.Image).Load();//手工加载，集合类型使用Collection方法
                uri = product.Image.ImageURI;//OK
            }
        }

        /// <summary>
        /// 预加载
        /// </summary>
        static void PreLoadOp()
        {
            using (var context = new DbContext("ConnectionString"))
            {
                //第一种方式
                var product = context.Set<ProductBaseEntity>().Include(p => p.Image.ImageHeader).FirstOrDefault(p => p.ProductID == 1);

                //第二种方式
                product = context.Set<ProductBaseEntity>().Include("Image").FirstOrDefault(p => p.ProductID == 1);
            }
        }
    }
}
