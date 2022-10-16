using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二维码生成器
{
    public class FileHelper
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLog(string message)
        {
            string datetime = DateTime.Now.ToString("yyyyMMdd");
            string path = $"{Directory.GetCurrentDirectory()}/{datetime}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            // 生成单行文本
            File.WriteAllText(path + "/log.txt", message);
        }
    }
}
