using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace dotnet.dotnet.common.TestCase
{
    class TestImage
    {
        public static void Test()
        {
            var savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InvalidImages");
            if(Directory.Exists(savePath))
            {
                Directory.Delete(savePath, true);
            }
            Directory.CreateDirectory(savePath);

            ImageValidator.IsImage("http://img.i200.cn/barcode/934/3496/9343496000613.jpg", savePath);
            ImageValidator.IsImage("http://img.i200.cn/barcode/694/0352/6940352201660.jpg", savePath);
            ImageValidator.IsImage("http://img.i200.cn/barcode/690/8588/6908588106265.jpg", savePath);
            ImageValidator.IsImage("http://img.i200.cn/2013/2/6ffdb16ff91d9db3b93698bbf649122a.jpg", savePath);


        }
    }
}
