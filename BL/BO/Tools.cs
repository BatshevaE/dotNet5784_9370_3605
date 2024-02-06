
namespace BO;
internal static class Tools

{
    /// <summary>
    /// override of the function to string that print an object
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    /// <param name="obj">object</param>
    /// <returns></returns>
    public static string ToStringProperty<T>(this T obj)
    {
        var properties = typeof(T).GetProperties();
        string result = $"{typeof(T).Name} properties:\n";
        foreach (var item in properties)
        {
            if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                if (item.GetValue(obj, null) != null)
                {
                    var val = item.GetValue(obj) as IEnumerable<object>;
                    if (val != null)
                    {
                        if (val.Any())
                        {
                            result += $"{item.Name} ";
                            foreach (var item1 in val)
                            {
                                result += $"{item1}, ";
                            }
                            if (result.EndsWith(", "))
                            {
                                result = result.Remove(result.Length - 2);
                            }
                            result += "\n";
                        }
                    }
                }
            }
            else
           if (item.GetValue(obj, null) != null)
                result += $"{item.Name}:{item.GetValue(obj)}\n";
        }
        return result;
    }

}
