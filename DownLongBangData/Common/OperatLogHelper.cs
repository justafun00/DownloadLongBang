using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Common
{
    public class OperatLogHelper
    {
        /// <summary>
        /// 操作日志文件名（路径+文件名）
        /// </summary>
        public static string operateLogPath;
        /// <summary>
        /// 操作日志目录（仅路径）
        /// </summary>
        public static string operateLogRootPath;

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteOperatLog(string operateLogString)
        {
            string directoryPath = Path.GetDirectoryName(operateLogPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            FileStream fs = new FileStream(operateLogPath, FileMode.Append, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);

            string msg = @"[" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"]" + operateLogString + sw.NewLine;

            sw.Write(msg);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static void WriteOperatLog(string operateLogString, DataSet operateData, string operateName)
        {
            string directoryPath = Path.GetDirectoryName(operateLogPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            FileStream fs = new FileStream(operateLogPath, FileMode.Append, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);

            string msg = @"[" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"]" + operateLogString + sw.NewLine;

            sw.Write(msg);
            sw.Flush();
            sw.Close();
            fs.Close();

            WriteOperateData(operateData, operateName);
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        private static void WriteOperateData(DataSet operateData, string operateName)
        {
            string fileName = operateLogRootPath+operateName+@"_"+System.DateTime.Now.ToString("yyyyMMddHHmmss")+".xml";
            operateData.WriteXml(fileName, XmlWriteMode.WriteSchema);
        }
    }
}
