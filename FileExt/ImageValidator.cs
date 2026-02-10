using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.common.FileExt
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageValidator
    {
        /// 判断文件是否为图片  
        /// </summary>  
        /// <param name="path">文件的完整路径</param>  
        /// <returns>返回结果</returns>  
        public static bool IsImage(string path)
        {
            try
            {
                var img = Image.FromFile(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static bool IsImage(string url, string savePath)
        {
            var isImage = true;
            var request = WebRequest.Create(url);
            try
            {
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        try
                        {
                            using (var img = Image.FromStream(stream))
                            {
                                isImage = true;
                            }
                        }
                        catch
                        {
                            isImage = false;
                        }
                    }
                }
            }
            catch
            {
                isImage = false;
            }

            return isImage;
        }


        /// <summary>
        /// 根据文件头判断上传的文件类型
        /// 255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
        /// 对常规修改的木马有效，也就是直接修改扩展名的，比如把.asp、带有脚本的html等改成.jpg这种。但是对于那种用工具生成的jpg木马没有效果。
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool IsPicture(string filePath)
        {
            try
            {
                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var reader = new BinaryReader(fs);
                string fileClass;
                byte buffer;
                buffer = reader.ReadByte();
                fileClass = buffer.ToString();
                buffer = reader.ReadByte();
                fileClass += buffer.ToString();
                reader.Close();
                fs.Close();
                if (fileClass == "255216" || fileClass == "7173" || fileClass == "13780" || fileClass == "6677")

                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 处理又拍云无法识别的例外的图片
        /// 有些图片格式本地无法打开，也无法识别，不过在浏览器中可以浏览
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsUpyunRiffImage(string filePath)
        {
            var isImage = false;
            try
            {

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader readder = new StreamReader(fileStream, Encoding.Default))
                    {
                        var content = readder.ReadToEnd();
                        if (content.StartsWith("RIFF") && !content.Contains("<script"))
                        {
                            isImage = true;
                        }
                    }
                }
            }
            catch
            {
                isImage = false;
            }
            return isImage;
        }
    }
}
