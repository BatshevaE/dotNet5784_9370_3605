using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for GuntWindow.xaml
/// </summary>
public partial class GuntWindow : Window
{
    public GuntWindow()
    {
        InitializeComponent();
        MyProperty=new();
     }

    public SelectTaskToGunt MyProperty
    {
        get { return (SelectTaskToGunt)GetValue(MyPropertyProperty); }
        set { SetValue(MyPropertyProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register("MyProperty", typeof(SelectTaskToGunt), typeof(GuntWindow), new PropertyMetadata(null));

    public class SelectTaskToGunt
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public DataTable Entries { get; set; }
        public SelectTaskToGunt()
        {
            List<BO.Task> tasks = s_bl.Task.ReadAll2().ToList();
       
            Entries = new DataTable()
            {
                Columns = {
                new DataColumn("Task Id", typeof(string)),
                new DataColumn("Task Name", typeof(string)),
                new DataColumn("Dependencies", typeof(string)) 
            }
            };
            DateTime? min = BlImplementation.Project.getStartProject();
            DateTime? max = BlImplementation.Project.getStartProject();

            for (int i = 0; i < tasks.Count(); i++)
            {
                if (tasks[i].ScheduledDate < min)
                    min = tasks[i].ScheduledDate;
                if (tasks[i].ForecastDate>max)
                    max= tasks[i].ForecastDate; 
            }
            for (DateTime d=min!.Value;d<=max; d=d.AddDays(1))
            {
                string str =$"{d.Day}/{d.Month}/{d.Year}";
                DataColumn ro=new DataColumn(str, typeof(string));
                //Entries.Columns.Add(str, typeof(string));
                Entries.Columns.Add(ro);
                
            }
            //for (int i = 0; i < tasks.Count(); i++)
            //{
            //    BO.Task task = tasks[i];
            //    List<BO.TaskInList> d = task.Dependencies!;
            //    string str = "(";
            //    for (int j = 0; j < d.Count(); j++)
            //    {
            //        str += $"{d[j].Id},";
            //    }
            //    str.Remove(str.Length);
            //    str += ")";
            //    Entries.Rows.Add(task.Id, task.Name, str);
            //}
            
            for (int i = 0; i < tasks.Count(); i++)
            {

                BO.Task task = tasks[i];
                DataRow row = Entries.NewRow();
                row[0]=task.Id;
                row[1]=task.Name;
                List<BO.TaskInList> d = task.Dependencies!;
                string str = "(";
                for (int j = 0; j < d.Count(); j++)
                {
                    str += $"{d[j].Id},";
                }
                str += ")";
                row[2] = str;
                int x = 3;
                for (DateTime day = min.Value;  day<=max!.Value;day=day.AddDays(1),x++)
                {
                    if((day < task.ScheduledDate)||(day>task.ForecastDate))
                    {
                        string strToPut = "None";
                        row[x] =strToPut;
                    }
                    else
                        row[x]=task.Status;
                }
                Entries.Rows.Add(row);
            }
        }
    }





}
