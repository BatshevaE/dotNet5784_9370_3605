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

        public DependencyWindow(int id = 0)
        {
            InitializeComponent();
            if (id == 0)//we click  the add button
            {
                CurrentDependency = new BO.TaskInList();
            }
            else//we click one of the dependency in the list on the window
            {
                try
                {
                    CurrentDependency = s_bl.Task.ReadAll()!.FirstOrDefault(item => item.Id == id)!;

                }
                catch (BO.BlDoesNotExistException ch) { MessageBox.Show(ch.Message, "failed", MessageBoxButton.OK); }
            }
        }
        public BO.TaskInList CurrentDependency
        {
            get { return (BO.TaskInList)GetValue(DependencyProperty); }
            set { SetValue(DependencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EngineerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DependencyProperty =
            DependencyProperty.Register("CurrentDependency", typeof(BO.TaskInList), typeof(DependencyWindow), new PropertyMetadata(null));

        private void BtnAddOrUpdateDependency_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (s_bl.Task.ReadAll().FirstOrDefault(item => item.Id == CurrentDependency!.Id) == null)//if there is not an task with such an id-we are on add mode
            //    {
            //        s_bl.Task.(CurrentDependency!);
            //        MessageBox.Show("successsfull create Task", "succeeded", MessageBoxButton.OK);
            //        this.Close();
            //        new TaskListWindow().Show();

            //    }
            //    else//there is  an task with such an id-we are on update mode
            //    {
            //        s_bl.Task.AddDependency(0,0);
            //        MessageBox.Show("successsfull update dependency", "succeedes", MessageBoxButton.OK);
            //        this.Close();
            //        new TaskListWindow().Show();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    Close();
            //}
        }
    }
}
