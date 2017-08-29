using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Xml.Serialization;

namespace Common
{
    /// <summary>
    /// 序列化对像类
    /// </summary>
    public class SerializeFiles
    {
        /// <summary>
        /// 将类序列化为二进制文件
        /// </summary>
        /// <param name="fileName">存储的文件名</param>
        /// <param name="data">要序列化的对像</param>
        public static void ObjectToBinarySerialize(string fileName, object data)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, data);
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
            }
        }

        /// <summary>
        /// 将类序列化为Xml文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public static void ObjectToXmlSerialize(string fileName,object data)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                XmlSerializer xs = new XmlSerializer(data.GetType());
                xs.Serialize(fs, data);
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
            }
        }

        /// <summary>
        /// 从二进制文件反序列化为类
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static object BinaryDeserializeToObject(string fileName)
        {
            object data = new object();
            if (File.Exists(fileName))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();
                    data = bf.Deserialize(fs);
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
                }
            }
            return data;
        }

        /// <summary>
        /// 从Xml文件反序列化为类
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object XmlDeserializeToObject(string fileName,Type type)
        {
            object data = new object();
            if (File.Exists(fileName))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    XmlSerializer xs = new XmlSerializer(type);
                    data = xs.Deserialize(fs);
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
                }
            }
            return data;
        }
    }
}