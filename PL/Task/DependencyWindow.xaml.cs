using PL.Engineer;
using System;
using System.Collections.Generic;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for DependencyWindow.xaml
    /// </summary>
    public partial class DependencyWindow : Window
    { 
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        /// <summary>
        /// ctor that gets id of a task 
        /// </summary>
        /// <param name="TaskId">id of the dependent current task</param>
        /// <param name="id">the id of the dependent on task</param>
        public DependencyWindow(int TaskId=0,int id = 0)
        {
            InitializeComponent();
            CurrentTaskDependency = s_bl.Task.Read(TaskId)!;
            if (id == 0)//we click  the add button
            {
                CurrentDependency = new BO.TaskInList();
                AddOrDel = true;
            }
            else//we click one of the dependency in the list on the window
            {
                AddOrDel = false;
                try
                {
                    CurrentDependency = CurrentTaskDependency.Dependencies!.FirstOrDefault(item => item.Id == id)!;
                }
                catch (BO.BlDoesNotExistException ch) { MessageBox.Show(ch.Message, "failed", MessageBoxButton.OK); }
            }
        }

        /// <summary>
        /// dependency property to know if we are on add(true) mode or delete mode. 
        /// </summary>
        public bool AddOrDel
        {
            get { return (bool)GetValue(AddOrDelProperty); }
            set { SetValue(AddOrDelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddOrDel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddOrDelProperty =
            DependencyProperty.Register("AddOrDel", typeof(bool), typeof(DependencyWindow), new PropertyMetadata(false));
        /// <summary>
        ///the dependent on task 
        /// </summary>

        public BO.TaskInList CurrentDependency
        {
            get { return (BO.TaskInList)GetValue(DependencyProperty); }
            set { SetValue(DependencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EngineerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DependencyProperty =
            DependencyProperty.Register("CurrentDependency", typeof(BO.TaskInList), typeof(DependencyWindow), new PropertyMetadata(null));
        /// <summary>
        ///the dependent  task
        /// </summary>
        public BO.Task CurrentTaskDependency
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTask.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTaskDependency", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
 /// <summary>
 /// add or delete a dependency
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>
        private void BtnAddDeleteDependency_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(AddOrDel)//add mode
                {
                    s_bl.Task.AddDependency(CurrentTaskDependency.Id,CurrentDependency.Id);
                    MessageBox.Show("successsfull create dependency", "succeeded", MessageBoxButton.OK);
                    this.Close();
                    new TaskListWindow().Show();

                }
                else//delete mode
                {
                    s_bl.Task.DeleteDependency(CurrentTaskDependency.Id,CurrentDependency.Id);
                    MessageBox.Show("successsfull delete dependency", "succeedes", MessageBoxButton.OK);
                    this.Close();
                    new TaskListWindow().Show();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }         
        }
        /// <summary>
        /// exit button-return to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
