using System;
using System.Collections.Generic;
using System.Data;
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
            int counter = 0;
            for(;min<max; min=min.Value.AddDays(10))
            {
                Entries.Columns.Add(min.ToString(), typeof(string));
                counter++;
            }
            for (int i = 0; i < tasks.Count(); i++)
            {
                BO.Task task = tasks[i];
                List<BO.TaskInList> d = task.Dependencies!;
                string str = "(";
                for (int j = 0; j < d.Count(); j++)
                {
                    str += $"{d[j].Id},";
                }
                str.Remove(str.Length);
                str += ")";
                Entries.Rows.Add(task.Id, task.Name, str);
            }
            for (int i = 1, j = 3; i < tasks.Count()&&j < counter + 3; i++,j++)
            {
               
            }
        }
    }





}
