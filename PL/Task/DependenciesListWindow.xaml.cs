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
    /// Interaction logic for DependenciesListWindow.xaml
    /// </summary>
    public partial class DependenciesListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        /// <summary>
        /// ctor,get id of task that the user want to watch it's dependecies(which tasks the currrent task dependent on)
        /// </summary>
        /// <param name="id"></param>
        public DependenciesListWindow(int id)
        {
            InitializeComponent();
            CurrentTaskDependency1 = s_bl.Task.Read(id)!;
            if (s_bl.Task.Read(id)!.Dependencies!=null)
               Dependencies = s_bl.Task.Read(id)!.Dependencies!;
            else
                MessageBox.Show("The task doesn't have dependencies,you can add.", "", MessageBoxButton.OK);
        }

        /// <summary>
        /// dependency property of the dependecies
        /// </summary>
        public IEnumerable<TaskInList> Dependencies
        {
            get { return (IEnumerable<TaskInList>)GetValue(dependenciesProperty); }
            set { SetValue(dependenciesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty dependenciesProperty =
            DependencyProperty.Register("Dependencies", typeof(IEnumerable<TaskInList>), typeof(DependenciesListWindow), new PropertyMetadata(null));
        /// <summary>
        /// dependency property of the current(dependent) task
        /// </summary>
        public BO.Task CurrentTaskDependency1
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTask.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTaskDependency1", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
        /// <summary>
        /// add a task that the current task dependent on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddDependency_Click(object sender, RoutedEventArgs e)
        {
            DependencyWindow dependency = new(CurrentTaskDependency1.Id,0);
            dependency.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// show the dependency details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsDependency_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? dependency = (sender as ListView)?.SelectedItem as BO.TaskInList;
            DependencyWindow newDependency = new(CurrentTaskDependency1.Id,dependency!.Id);
            newDependency.ShowDialog();
            this.Close();
   
        }
    }

}
