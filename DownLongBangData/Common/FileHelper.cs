using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class FileHelper
    {
        public static byte[] GetFileFromFileName(string fileName)
        {
            FileStream fs = null;
            BinaryReader br = null;
            byte[] bs;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                bs = br.ReadBytes((int)fs.Length);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (br != null)
                {
                    br.Close();
                }
            }

            return bs;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static bool SaveFileByPath(string fullPath,byte[] buffer)
        {
            bool result = false;
            FileStream fs = null;
            BinaryWriter br = null;
            try
            {
                string directoryPath = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);//创建新文件，如果已存在则覆盖
                br = new BinaryWriter(fs);
                br.Write(buffer);
                br.Flush();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs !=null) {fs.Close();}
                if (br != null) { br.Close();}
            }
            return result;
        }
    }
}
