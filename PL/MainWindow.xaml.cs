using DalApi;
using PL.Engineer;
using PL.Task;
using System;
using System.Printing.IndexedProperties;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// constractor
        /// </summary>
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public MainWindow()
        {
          
            InitializeComponent();
            Date1 = s_bl.Clock.Date;//.ToLocalTime();
            DateTime? date2 = BlImplementation.Project.GetStartProject();
            CurrentTime = new Tuple<DateTime, DateTime?>(Date1, date2);

        }
        /// <summary>
        /// dependency property of tuple of the current time and the project start date
        /// </summary>
        public Tuple<DateTime,DateTime?> CurrentTime
        {
            get { return (Tuple<DateTime, DateTime?>)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(Tuple<DateTime, DateTime?>), typeof(MainWindow), new PropertyMetadata(null));
        /// <summary>
        /// dependency property to the clock
        /// </summary>
        public DateTime Date1
        {
            get { return (DateTime)GetValue(StartProjectProperty); }
            set { SetValue(StartProjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartProjectProperty =
            DependencyProperty.Register("Date1", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(null));


        /// <summary>
        /// button to open the list of all engineers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEngineers_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        /// <summary>
        /// button to initialize the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnIntilization_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Are you sure you want to initialize the data?", "Initialize Data", MessageBoxButton.YesNo);
            if (mbResult == MessageBoxResult.Yes)
            {
                BlApi.Factory.Get().Engineer.Clear();
                BlApi.Factory.Get().Task.Clear();
                BlApi.Factory.Get().User.Clear();
                BlImplementation.Project.ZeroStartProject();
                BlApi.Factory.Get().InitializeClock();
                DalTest.Initialization.Do();
            }
            new MainWindow().Show();
            this.Close();

        }
        /// <summary>
        /// open the list of all tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTaskClick(object sender, RoutedEventArgs e)
        {
            TaskListWindow task = new();
            task.Show();
        }

        private void BtnAddYear_Click(object sender, RoutedEventArgs e)
        {
            Date1 = s_bl.AddYear();
            CurrentTime=new Tuple<DateTime,DateTime?>(Date1, CurrentTime.Item2);
        }

        private void BtnAddMonth_Click(object sender, RoutedEventArgs e)
        {
            Date1 = s_bl.AddMonth();
            CurrentTime = new Tuple<DateTime, DateTime?>(Date1, CurrentTime.Item2);

        }

        private void BtnAddDay_Click(object sender, RoutedEventArgs e)
        {
            Date1 = s_bl.AddDay();
            CurrentTime = new Tuple<DateTime, DateTime?>(Date1, CurrentTime.Item2);

        }
        /// <summary>
        /// clear all data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult mbResult = MessageBox.Show("Are you sure you want to delete all data?", "Clear Data", MessageBoxButton.YesNo);
            if (mbResult == MessageBoxResult.Yes)
            {
                BlApi.Factory.Get().Engineer.Clear();
                BlApi.Factory.Get().Task.Clear();
                BlApi.Factory.Get().User.Clear();
                BlImplementation.Project.ZeroStartProject();
            }
        }
        /// <summary>
        /// chose a date to start the project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartProjectDate_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (BlImplementation.Project.GetStartProject() == null)//if there is no start project
            {
               MessageBoxResult msg= MessageBox.Show(@"After choosing a date to start the project you will not be able to add/update/delete tasks.
Are you sure you want to choose a start date for the project?","sart date",MessageBoxButton.YesNo);
                if (msg ==MessageBoxResult.Yes)
                {
                    new DatePickerWindow().ShowDialog();
                    new MainWindow().Show();
                    this.Close();
                }
            }
            else//if there is-crate automatic schedule only if we are in middle stage
            if (BlImplementation.Project.GetStage() == BO.Stage.MiddleStage)
            {
                try
                {
                    s_bl.Task.CreateAutomaticSchedule();
                    MessageBox.Show("The  creation of the schdule were successfully updated", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                    btn!.IsEnabled = false;
                }
                catch (BO.BlNotAtTheRightStageException ch)
                {
                    MessageBox.Show(ch.Message, "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
             
            }
               
        }

        private void BtnGunt_Click(object sender, RoutedEventArgs e)
        {
            GuntWindow t= new GuntWindow(); 
            t.ShowDialog();
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            new UserWindow().Show();
            this.Close();


        }

    }
}