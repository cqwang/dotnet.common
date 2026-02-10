
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace dotnet.common.DataBase
{
    [Obsolete("不建议使用，反射性能问题。建议用datareader dataadapter或EF自带的映射机制")]
    public static class DataTableExtension
    {
        public static List<T> ToObjectList<T>(this DataTable dataTable) where T : new()
        {
            if (dataTable == null)
            {
                return null;
            }

            var records = new List<T>();
            var columnNames = dataTable.GetColumnNames();
            foreach (DataRow row in dataTable.Rows)
            {
                records.Add(row.ToObject<T>(columnNames));
            }
            return records;
        }

        public static T ToObject<T>(this DataRow row, List<string> columnNames = null) where T : new()
        {
            if (row == null)
            {
                return default(T);
            }

            if (columnNames == null)
            {
                columnNames = row.Table.GetColumnNames();
            }

            var instance = new T();
            var type = typeof(T);
            foreach (var columnName in columnNames)
            {
                var property = type.GetProperty(columnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase); //性能稍差，且多次请求会存在重复计算
                if (property != null && row[columnName] != DBNull.Value)
                {
                    var propertyType = property.PropertyType;
                    if (propertyType.IsGenericType)
                    {
                        propertyType = propertyType.GetGenericArguments()[0];//特殊处理 可空类型获取原始类型，避免直接转换出错
                    }
                    if (typeof(IConvertible).IsAssignableFrom(propertyType))
                    {
                        var value = Convert.ChangeType(row[columnName], propertyType);
                        property.SetValue(instance, value, null);//性能较差，可以考虑用基于字段类型的委托，缓存下来，并考虑兼容性
                    }
                }
            }
            return instance;
        }

        public static List<string> GetColumnNames(this DataTable dataTable)
        {
            if (dataTable == null)
            {
                return null;
            }

            var columnNames = new List<string>(dataTable.Columns.Count);
            foreach (DataColumn column in dataTable.Columns)
            {
                columnNames.Add(column.ColumnName);
            }
            return columnNames;
        }

        /// <summary>
        /// 自定义转换规则，这样类型不匹配也可以特殊处理
        /// </summary>
        /// <param name="valueStr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object TryParseValue(object columnValue, Type type)
        {
            var value = columnValue.ToString();
            switch (type.Name)
            {
                case "Decimal":
                    decimal decimalValue;
                    decimal.TryParse(value, out decimalValue);
                    return decimalValue;
                case "Double":
                    double doubleValue;
                    double.TryParse(value, out doubleValue);
                    return doubleValue;
                case "Int64":
                    long longValue;
                    long.TryParse(value, out longValue);
                    return longValue;
                case "UInt64":
                    ulong ulongValue;
                    ulong.TryParse(value, out ulongValue);
                    return ulongValue;
                case "Int32":
                    int intValue;
                    int.TryParse(value, out intValue);
                    return intValue;
                case "UInt32":
                    uint uintValue;
                    uint.TryParse(value, out uintValue);
                    return uintValue;
                case "Single":
                    Single singleValue;
                    Single.TryParse(value, out singleValue);
                    return singleValue;
                case "Int16":
                    short shortValue;
                    short.TryParse(value, out shortValue);
                    return shortValue;
                case "UInt16":
                    ushort ushortValue;
                    ushort.TryParse(value, out ushortValue);
                    return ushortValue;
                case "Byte":
                    byte byteValue;
                    byte.TryParse(value, out byteValue);
                    return byteValue;
                case "Byte[]":
                    return Encoding.UTF8.GetBytes(value);
                case "Boolean":
                    bool boolValue;
                    bool.TryParse(value, out boolValue);
                    return boolValue;
                case "DateTime":
                    DateTime dateTimeValue;
                    DateTime.TryParse(value, out dateTimeValue);
                    return dateTimeValue;
                case "DateTimeOffset":
                    DateTimeOffset dateTimeOffsetValue;
                    DateTimeOffset.TryParse(value, out dateTimeOffsetValue);
                    return dateTimeOffsetValue;
                case "TimeSpan":
                    TimeSpan timeSpanValue;
                    TimeSpan.TryParse(value, out timeSpanValue);
                    return timeSpanValue;
                case "Guid":
                    Guid guidValue;
                    Guid.TryParse(value, out guidValue);
                    return guidValue;
                default:
                    return columnValue;
            }
        }


        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            if (list == null || !list.Any())
            {
                return null;
            }

            var dt = new DataTable(typeof(T).Name);
            var myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var item in list)
            {
                if (item == null)
                {
                    continue;
                }

                var row = dt.NewRow();
                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    var pi = myPropertyInfo[i];
                    var name = pi.Name;
                    if (dt.Columns[name] == null)
                    {
                        var column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(item, null);
                }

                dt.Rows.Add(row);
            }
            return dt;
        }
        

    }
}
