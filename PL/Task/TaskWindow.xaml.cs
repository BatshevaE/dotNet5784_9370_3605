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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public TaskWindow(int id=0)
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
            DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));



        private void BtnDependencyClick(object sender, RoutedEventArgs e)
        {
            DependenciesListWindow dependency = new(CurrentTask.Id);
            dependency.Show();
            //dependency.ShowDialog();
            this.Close();
        }

        private void BtnAddOrUpdateTask_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (s_bl.Task.ReadAll().FirstOrDefault(item => item.Id == CurrentTask!.Id) == null)//if there is not an task with such an id-we are on add mode
                {
                    s_bl.Task.Create(CurrentTask!);
                    MessageBox.Show("successsfull create Task", "succeeded", MessageBoxButton.OK);
                    this.Close();
                    new TaskListWindow().Show();

                }
                else//there is  an task with such an id-we are on update mode
                {
                    s_bl.Task.Update(CurrentTask!);
                    MessageBox.Show("successsfull update task", "succeedes", MessageBoxButton.OK);
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
    }
}
