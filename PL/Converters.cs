
using DalApi;
using System.Data;
using System.Globalization;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
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
class ConvertTaskInEngToBool: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((Tuple<int, string>)value == null)
           return true;
        return false;
        //IEnumerable<DO.Task> taskList =
        //  from DO.Task item in _dal.Task.ReadAll()
        //  where item.Engineerid == idEngineer//the engineer is already assigned to another task
        //  select item;
        //if (taskList.Any())
        //    return false;
        //return true;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
class ConvertIdToContentDependency : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Delete";
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
    class ConvertIdToBoolOposite : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((int)value != 0)
              return true;
        return false;
    }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
class ConvertTupleToContext : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((Tuple<int?,string>?)value == null)
            return "No Engineer Assigned Yet";
        return value.ToString()!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
class ConvertStatusToColur : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((string)value == "Schedeled")
            return new SolidColorBrush(Colors.Aquamarine);
        else if ((string)value == "InJeopardy")
            return new SolidColorBrush(Colors.Red);
        else if ((string)value == "OnTrack")
            return new SolidColorBrush(Colors.Lavender);
        else if ((string)value == "Done")
            return new SolidColorBrush(Colors.RoyalBlue);
        return new SolidColorBrush(Colors.White);

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
class ConvertTupleToText : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        return (Tuple<int,string>?)value == null ? "Select a Task To Assign" : "Watch Details of Your Assigned Task";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
class ConvertTupleToContextInEng : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((Tuple<int, string>?)value == null)
            return "You Are Not Assigned To Any Task";
        return value.ToString()!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


