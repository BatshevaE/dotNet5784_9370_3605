
using DalApi;
using System.Data;
using System.Globalization;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
namespace PL;

/// <summary>
/// convert the id to the content of the button.if you send an id,the button will be called "Update".if tou dont send an id,the button will be called "Add"
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
/// check if the engineer has a task that he didn't finish yet-returns false
/// </summary>
class ConvertTaskInEngToBool: IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var p = value as Tuple<int, string>;
        if (p == null)
            return true;
        BO.Task t = s_bl.Task.Read(p!.Item1)!;
        if ((Tuple<int, string>)value == null||t.Status==BO.Status.Done)
           return true;
        return false;
        
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
/// <summary>
/// check id of dependent on task to know if we in add(0) or delete
/// </summary>
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
/// <summary>
/// if the id in not 0 returns true
/// </summary>
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
/// <summary>
/// we get tuple that in task- if it's null there is no engineer that assigned to the task
/// </summary>
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
/// <summary>
/// conver the status of tasks to colors
/// </summary>
class ConvertStatusToColur : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((string)value == "Schedeled")
            return new SolidColorBrush(Colors.Orange);
        else if ((string)value == "InJeopardy")
            return new SolidColorBrush(Colors.Red);
        else if ((string)value == "OnTrack")
            return new SolidColorBrush(Colors.Fuchsia);
        else if ((string)value == "Done")
            return new SolidColorBrush(Colors.Pink);
        return new SolidColorBrush(Colors.White);

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
/// <summary>
/// convert the words that written in the gunt that we dont want to be seen too be at the same color of the background
/// </summary>
class ConvertWordToColur : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((string)value == "Schedeled")
            return new SolidColorBrush(Colors.Orange);
        else if ((string)value == "InJeopardy")
            return new SolidColorBrush(Colors.Red);
        else if ((string)value == "OnTrack")
            return new SolidColorBrush(Colors.Fuchsia);
        else if ((string)value == "Done")
            return new SolidColorBrush(Colors.Pink);
        else if ((string)value == "None")
            return new SolidColorBrush(Colors.White);
        return new SolidColorBrush(Colors.Black);

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
/// <summary>
/// if the engineer has a tasks to do the name of the button is watch your tasks
/// </summary>
class ConvertTupleToText : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        return (IEnumerable<Tuple<int, string>?>?)value == null ? "Select a Task To Assign" : $@"Watch Details
Your Assigned Task";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
/// <summary>
/// the label in engineer window to see his tasks/don't have any
/// </summary>
class ConvertTupleToContextInEng : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((IEnumerable<Tuple<int, string>?>?)value == null)
            return "You Are Not Assigned To Any Task";
        String str="";
        foreach (var item in (IEnumerable<Tuple<int, string>?>?)value!)
        { str+=$"{item!.ToString()}"; }
        return str;
       // return value.ToString()!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}/// <summary>
/// if we have a date "create schedule"
/// </summary>
class ConvertDateToConext : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((DateTime?)value == null)
            return $@"Select Project 
Start Date";
        return "Create Schedule";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


