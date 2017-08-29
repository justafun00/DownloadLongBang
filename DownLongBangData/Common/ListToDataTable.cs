using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// 将List转换成DataSet、DataTable
    /// </summary>
    public static class ListToDataTable
    {
        /// <summary>
        /// 将List转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            return ToDataTable<T>(data, null);
        }

        public static DataTable ToDataTable<T>(this IList<T> data,string dtName)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            //System.Windows.Forms.MessageBox.Show(typeof(T).ToString());
            DataTable dt = new DataTable(dtName!=null?dtName:"");
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, GetPropertyType(property.PropertyType));
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }

        /// <summary>
        /// 将可为空的值类型转换为基础基元类型
        /// </summary>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        public static Type GetPropertyType(Type propertyType)
        {
            //属性可能为可为null值类型的变量(int? DateTime?)，即Nullable<T>，所以必须转换为基础基元类型
            //判断是否为泛型类型 并且 是否为Nullable<>类型
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                //如果propertyType为nullable<>类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                NullableConverter nullableConverter = new NullableConverter(propertyType);
                //将propertyType转换为nullable对的基础基元类型
                return nullableConverter.UnderlyingType;
            }
            else
            {
                return propertyType;
            }
        }

        /// <summary>
        /// 将List转换成DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataSet ToDataSet<T>(this IList<T> data,string dtName)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(ToDataTable<T>(data,dtName));
            return ds;
        }
    }
}
