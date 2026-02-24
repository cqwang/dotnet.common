using Cqwang.BackEnd.CSharp.Syntax.TestCase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.Syntax
{
    class Tracking
    {
        /// <summary>
        /// 关闭变更跟踪
        /// </summary>
        static void CloseDetectOp()
        {
            using (var context = new DbContext("ConnectionString"))
            {
                //第一种方式
                context.Configuration.AutoDetectChangesEnabled = false;//关闭变更跟踪
                var product = context.Set<ProductBaseEntity>().FirstOrDefault(p => p.ProductID == 1);//
                product.ProductName = "New Name";
                context.ChangeTracker.DetectChanges();//启用一次变更跟踪
                context.SaveChanges();//保存到数据库

                //第二种方式
                context.Configuration.AutoDetectChangesEnabled = false;
                product = context.Set<ProductBaseEntity>().FirstOrDefault(p => p.ProductID == 1);
                product.ProductName = "New Name";
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 关闭变更跟踪
        /// </summary>
        static void AsNoTrackingOp()
        {
            using (var context = new DbContext("ConnectionString"))
            {
                var product = context.Set<ProductBaseEntity>().AsNoTracking().FirstOrDefault(p => p.ProductID == 1);//直接和数据库交互

                product.ProductName = "New Name";
                context.Set<ProductBaseEntity>().Attach(product);//添加到缓存
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
