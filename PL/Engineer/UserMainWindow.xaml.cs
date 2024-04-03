using PL.Task;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        /// <summary>
        /// ctor that gets id of user-engineer 
        /// </summary>
        /// <param name="id">engineer's id</param>
        public UserMainWindow(int id)
        {
            InitializeComponent();
            Eng = s_bl.Engineer.Read(id)!;
        }
        /// <summary>
        /// dependency property of the user
        /// </summary>
        public BO.Engineer Eng
        {
            get { return (BO.Engineer)GetValue(engProperty); }
            set { SetValue(engProperty, value); }
        }

        // Using a DependencyProperty as the backing store for eng.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty engProperty =
            DependencyProperty.Register("Eng", typeof(BO.Engineer), typeof(UserMainWindow), new PropertyMetadata(null));
        /// <summary>
        /// button to see the details of the current user-engineer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Engineer.Read(Eng.Id);
                new EngineerWindow(Eng.Id).ShowDialog();//opens a window of a single engineer
                try { s_bl.Engineer.Read(Eng.Id); }
                catch { new UserWindow().Show(); this.Close();  };
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                new UserWindow().Show();
                this.Close();
            }
        }
        /// <summary>
        /// button to watch the task you need to do now or assign to a task if you don't have one/finished your task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAssignOrWatch_Click(object sender, RoutedEventArgs e)
        {
            if(Eng.Task==null)//don't assigned to any task
            { 
                new taskToEnginner(Eng).ShowDialog();
               
                new UserMainWindow(Eng.Id).Show();
                this.Close();
            } 
            else 
            {
                new TaskListWindow(Eng.Id).ShowDialog();
                new UserMainWindow(Eng.Id).Show();
                this.Close();
            }
        }
        /// <summary>
        /// a button to sign in to the systen as another user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            UserWindow s = new();
            s.Show();
            this.Close();
        }
    }


  


}
