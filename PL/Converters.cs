﻿
using DalApi;
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
            if(((int)value != 0)&& ((BO.Stage)BlImplementation.Project.getStage() == BO.Stage.Doing))
              return true;
        return false;
    }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



