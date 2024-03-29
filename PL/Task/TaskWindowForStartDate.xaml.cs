using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Task;

/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class TaskWindowForStartDate : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public TaskWindowForStartDate(int id = 0)
    {
        InitializeComponent();
        if (id == 0)//we click  the add button
        {
            CurrentTask = new BO.Task();
        }
        else//we click one of the engineers in the list on the window
        {
            try
            {
                CurrentTask = s_bl.Task.Read(id)!;

            }
            catch (BO.BlDoesNotExistException ch) { MessageBox.Show(ch.Message, "failed", MessageBoxButton.OK); }
        }

    }
    public BO.Task CurrentTask
    {
        get { return (BO.Task)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentTask.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentTaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindowForStartDate), new PropertyMetadata(null));
    private void updateStartTaskDate_Btn(object sender, SelectionChangedEventArgs e)
    {
        var picker = sender as Calendar;
        DateTime? date = picker!.SelectedDate;
        s_bl.Task.UpdateActuallStartDate(date, CurrentTask.Id);
        this.Close();
    }

    private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
    {
        MainWindow s = new MainWindow();
        s.ShowDialog();
        this.Close();

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MainWindow s = new MainWindow();
        s.ShowDialog();
        this.Close();
    }
}


