
using DalApi;
using DO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace BO;

internal static class Tools

{
    public static string ToStringProperty<T>(this T obj)
    {
        string str = "";
        foreach (PropertyInfo item in typeof(T).GetProperties())
        {
             if (item.PropertyType.IsGenericType&&item.PropertyType.GetGenericTypeDefinition()==typeof(List<>))
            {
                var value = item.GetValue(obj); 
                if (value != null) 
                {
                    str += "\n" + item.Name+":";
                    foreach (var item1 in value as IEnumerable<object>)
                        str += item1;
                }

                
            }
               else if (item is ITuple tuple)
            {
                // Handle Tuple
                IEnumerable<string> stringRepresentations = Enumerable.Range(0, tuple.Length)
                    .Select(i => tuple[i]?.ToString() ?? "null");
                str += string.Join(", ", stringRepresentations);
            }


            else if (item.GetValue(obj, null) != null)
                    str += "\n" + item.Name + ": " + item.GetValue(obj, null);
            
        }
        return str;
    }
    
}
