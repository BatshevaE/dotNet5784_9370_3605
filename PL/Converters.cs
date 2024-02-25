
using System.Globalization;
using System.Windows.Data;
namespace PL;

/// <summary>
/// covert the id to the content of the button.if you send an id,the button will be called "Update".if tou dont send an id,the button will be called "Add"
/// </summary>
class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    
}
/// <summary>
/// convert the id to bool.if you send an id you are in update mode and you cant change the id. if  you dont send an id you are in add mode and you can change the id. 
/// </summary>
class ConvertIdToBool : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isEnabel = (int)value == 0;
        return isEnabel;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
