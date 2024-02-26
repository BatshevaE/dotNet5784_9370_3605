using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerLevel Level { get; set; } = BO.EngineerLevel.None;
        public BO.Status Status { get; set; } = BO.Status.None;
        public TaskListWindow()
        {
            InitializeComponent();
            //TaskList = s_bl?.Task.ReadAll()!;
            TaskList = (Level == BO.EngineerLevel.None && Status == BO.Status.None) ?
            s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => (item.Copmlexity == Level)&&(item.Status==Status))!;
        }
        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        // Using a DependencyProperty as the backing store for .  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));

        private void CbComplexity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Level == BO.EngineerLevel.None) ?
           s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => (item.Copmlexity == Level))!;
        }

        private void CbStatusSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Status == BO.Status.None) ?
              s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => (item.Status == Status))!;  
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow task = new();
            task.ShowDialog();
            this.Close();
        }

        private void DoubleClicTask(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? task=(sender as ListView)?.SelectedItem as BO.TaskInList;    
            TaskWindow newTask = new(task!.Id);
            newTask.ShowDialog();
            this.Close();
           
        }
    }


}
