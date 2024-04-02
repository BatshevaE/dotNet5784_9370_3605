using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
    /// <summary>
    /// empty ctor
    /// </summary>
    public GuntWindow()
    {
        InitializeComponent();
        MyProperty=new();
     }
    /// <summary>
    /// all the gunt
    /// </summary>
    public SelectTaskToGunt MyProperty
    {
        get { return (SelectTaskToGunt)GetValue(MyPropertyProperty); }
        set { SetValue(MyPropertyProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register("MyProperty", typeof(SelectTaskToGunt), typeof(GuntWindow), new PropertyMetadata(null));
    /// <summary>
    /// class of the gunt that creats it 
    /// </summary>
    public class SelectTaskToGunt
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public DataTable Entries { get; set; }//property of the gunt
        public SelectTaskToGunt()//empty ctor
        {
            List<BO.Task> tasks = s_bl.Task.ReadAllBOTask().ToList();//list of all tasks

            Entries = new DataTable()//create 3 columns
            {
                Columns = {
                new DataColumn("Task Id", typeof(string)),
                new DataColumn("Task Name", typeof(string)),
                new DataColumn("Dependencies", typeof(string))

            }
            };
            DateTime? min = BlImplementation.Project.GetStartProject();//to save the minimal start date of tasks
            DateTime? max = BlImplementation.Project.GetStartProject();//to sava the maximal start date of task

            for (int i = 0; i < tasks.Count(); i++)//find the max and min
            {
                if (tasks[i].ScheduledDate < min)
                    min = tasks[i].ScheduledDate;
                if (tasks[i].ForecastDate > max)
                    max = tasks[i].ForecastDate;
            }
            if (min == null || max == null)
                MessageBox.Show("There is No Start Date for the project,can't open gant", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                for (DateTime d = min!.Value; d <= max; d = d.AddDays(1))//crates the rest columns
                {
                    string str = $"{d.Day}-{d.Month}-{d.Year}";//the column's format
                    DataColumn ro = new(str, typeof(string));
                    Entries.Columns.Add(ro);

                }
                for (int i = 0; i < tasks.Count(); i++)
                {

                    BO.Task task = tasks[i];
                    DataRow row = Entries.NewRow();
                    row[0] = task.Id;
                    row[1] = task.Name;
                    List<BO.TaskInList> d = task.Dependencies!;
                    string str = "(";
                    for (int j = 0; j < d.Count(); j++)
                    {
                        str += $"{d[j].Id},";
                    }
                    str += ")";
                    row[2] = str;
                    int x = 3;
                    for (DateTime day = min.Value; day <= max!.Value; day = day.AddDays(1), x++)
                    {
                        if (task.StartDate != null)
                        {
                            if ((day < task.StartDate) || (day > task.DeadlineDate))
                            {
                                string strToPut = "None";
                                row[x] = strToPut;

                            }

                            else
                                row[x] = task.Status.ToString();
                        }
                        else
                        {
                            if ((day < task.ScheduledDate) || (day > task.ForecastDate))
                            {
                                string strToPut = "None";
                                row[x] = strToPut;

                            }
                            else
                                row[x] = task.Status.ToString();
                        }
                    }
                    Entries.Rows.Add(row);
                }
            }
          
        }
    }





}
