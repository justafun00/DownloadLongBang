using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Common
{
    public class Json
    {
        /// <summary>
        /// 将对像序列化为Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将Json字符串反序列化为对像
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string json)
        {
          return  JsonConvert.DeserializeObject<T>(json);
        }
    }
}
