
using DO;
using System.Data.Common;
using System.Reflection;

namespace BO;

internal static class Tools

{
    public static string ToStringProperty<T>(this T obj)
    {
        string str = "";
        foreach (PropertyInfo item in typeof(T).GetProperties())
            str += "\n" + item.Name + ": " + item.GetValue(obj, null);
        return str;
    }
}
