using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Common
{
    public class MsgHelper
    {
        public static string msgLogPath;
        public static void WriteLogToFile(string message)
        {
            WriteLogToFile(message, null);
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteLogToFile(string message, Exception ex)
        {
            string directoryPath = Path.GetDirectoryName(msgLogPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            FileStream fs = new FileStream(msgLogPath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            string msg = @"[" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"]" + message;
            if (ex != null)
            {
                msg += "错误信息:" + ex.Message + " 出错方法" + ex.TargetSite + sw.NewLine;
            }
            else
            {
                msg += sw.NewLine;
            }
            sw.Write(msg);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
