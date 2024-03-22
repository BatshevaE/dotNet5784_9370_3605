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

        public DependencyWindow(int TaskId=0,int id = 0)
        {
            InitializeComponent();
            CurrentTaskDependency = s_bl.Task.Read(TaskId)!;
            if (id == 0)//we click  the add button
            {
                CurrentDependency = new BO.TaskInList();
            }
            else//we click one of the dependency in the list on the window
            {
                try
                {
                    CurrentDependency = CurrentTaskDependency.Dependencies!.FirstOrDefault(item => item.Id == id)!;

                }
                catch (BO.BlDoesNotExistException ch) { MessageBox.Show(ch.Message, "failed", MessageBoxButton.OK); }
            }
        }
        public BO.TaskInList CurrentDependency//the dependent task 
        {
            get { return (BO.TaskInList)GetValue(DependencyProperty); }
            set { SetValue(DependencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EngineerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DependencyProperty =
            DependencyProperty.Register("CurrentDependency", typeof(BO.TaskInList), typeof(DependencyWindow), new PropertyMetadata(null));
        public BO.Task CurrentTaskDependency//the dependent on task
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTask.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTaskDependency", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
 
        private void BtnAddDeleteDependency_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentTaskDependency.Dependencies!.FirstOrDefault(m => m.Id == CurrentDependency!.Id) == null)//if there is not a dependency with such an id-we are on add mode
                {

                    s_bl.Task.AddDependency(CurrentTaskDependency.Id,CurrentDependency.Id);
                    MessageBox.Show("successsfull create dependency", "succeeded", MessageBoxButton.OK);
                    new TaskListWindow().Show();
                    this.Close();

                }
                else//there is  an engineer with such an id-we are on update mode
                {
                    s_bl.Task.deleteDependency(CurrentTaskDependency.Id,CurrentDependency.Id);
                    MessageBox.Show("successsfull delete dependency", "succeedes", MessageBoxButton.OK);
                    new TaskListWindow().Show();
                    this.Close();


                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               this. Close();
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
