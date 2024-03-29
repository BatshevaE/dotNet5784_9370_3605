using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for taskToEnginner.xaml
    /// </summary>
    public partial class taskToEnginner : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerLevel Level { get; set; } = BO.EngineerLevel.None;
        public taskToEnginner(BO.Engineer eng)
        {
            
            InitializeComponent();
            //TaskForEngList.ToList().Clear();
            EngToAssign = s_bl.Engineer.Read(eng.Id)!;
            TaskForEngList = s_bl?.Task.AllTasksToAssign(eng)!;
            //s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => (item.Copmlexity <= Level))!;
        }


        public BO.Engineer EngToAssign
        {
            get { return (BO.Engineer)GetValue(EngToAssignProperty); }
            set { SetValue(EngToAssignProperty, value); }
        }

        // Using a DependencyProperty as the backing store for engToAssign.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngToAssignProperty =
            DependencyProperty.Register("EngToAssign", typeof(BO.Engineer), typeof(taskToEnginner), new PropertyMetadata(null));


        public IEnumerable<BO.TaskInList> TaskForEngList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        // Using a DependencyProperty as the backing store for .  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskForEngList", typeof(IEnumerable<BO.TaskInList>), typeof(taskToEnginner), new PropertyMetadata(null));

        private void DoubleClicTask(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? task = (sender as ListView)?.SelectedItem as BO.TaskInList;
            try
            {
                s_bl.Task.updateEngineerToTask(EngToAssign.Id, task!.Id);
                MessageBoxResult result=MessageBox.Show($"You Assigned To Task With Id: {task.Id}. Please choose a date not earlier than:{s_bl.Task.DateToStart(task.Id)} to start the task? ", "Success", MessageBoxButton.OK);
                //i/*f (result == MessageBoxResult.Yes)*/
                { new TaskWindowForStartDate(task.Id).ShowDialog(); this.Close(); }
                //else 
                //{
                //    this.Close();
                //}
            }
            catch (Exception ch)
            {
                MessageBox.Show(ch.Message, "failed", MessageBoxButton.OK);
                this.Close();
            }
        }
    }
}
