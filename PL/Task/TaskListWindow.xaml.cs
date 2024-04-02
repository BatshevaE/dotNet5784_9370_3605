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
       /// <summary>
       /// ctor,gets id to know if we are here from an engineer that want to watch his  tasks
       /// </summary>
       /// <param name="id"></param>
        public TaskListWindow(int id=0)
        {
            InitializeComponent();
            IfFromEng = (id == 0);//we are not from engineer-true
            if (id==0)//not from engineer
            {//show all tasks
                TaskList = (Level == BO.EngineerLevel.None && Status == BO.Status.None) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => (item.Copmlexity == Level) && (item.Status == Status))!;
            }
            else//if we are from engineer-show all tasks that the engineer is assign to 
            {
                TaskList=s_bl?.Task.ReadAllBOTask()!.Where(item=>(item.EngineerTask!=null)&&(item.EngineerTask.Item1==id)).Select(item=>new TaskInList(){ Id=item.Id, Name=item.Name, Copmlexity= item.Copmlexity, Description=item.Description, Status=item.Status })!;
            }
        }

        /// <summary>
        /// dependency property to know where we came from
        /// </summary>
        public bool IfFromEng
        {
            get { return (bool)GetValue(id1Property); }
            set { SetValue(id1Property, value); }
        }

        // Using a DependencyProperty as the backing store for id1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty id1Property =
            DependencyProperty.Register("IfFromEng", typeof(bool), typeof(TaskListWindow), new PropertyMetadata(null));

        /// <summary>
        /// dependency propery of all the tasks
        /// </summary>
        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        // Using a DependencyProperty as the backing store for .  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));
        /// <summary>
        /// combobox to sort the tasks by their level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbComplexity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Level == BO.EngineerLevel.None) ?
           s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => (item.Copmlexity == Level))!;
        }
        /// <summary>
        /// combobox to sort the tasks by their status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbStatusSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Status == BO.Status.None) ?
              s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => (item.Status == Status))!;  
        }
        /// <summary>
        /// button to add task to the list of tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
          
            TaskWindow task = new();
            task.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// a double click on a task will open a window with the task details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoubleClicTask(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? task=(sender as ListView)?.SelectedItem as BO.TaskInList;
            if (task == null) return;
            TaskWindow newTask = new(task!.Id);
            newTask.ShowDialog();
            this.Close();      
        }
        /// <summary>
        /// button to return
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


}
