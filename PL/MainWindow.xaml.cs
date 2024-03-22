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
            //  UserWindow us=new();
            // us.ShowDialog();
            // this.Close();
            InitializeComponent();
            Date1 = s_bl.Clock.ToLocalTime();
            DateTime? date2 = BlImplementation.Project.getStartProject();
            CurrentTime = new Tuple<DateTime, DateTime?>(Date1, date2);
            
            //CurrentTime=DateTime.Now;

        }
        public Tuple<DateTime,DateTime?> CurrentTime
        {
            get { return (Tuple<DateTime, DateTime?>)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(Tuple<DateTime, DateTime?>), typeof(MainWindow), new PropertyMetadata(null));

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
                BlApi.Factory.Get().Engineer.clear();
                BlApi.Factory.Get().Task.clear();
                BlApi.Factory.Get().User.clear();
                BlImplementation.Project.zeroStartProject();
                DalTest.Initialization.Do();
            }
            new MainWindow().Show();
            this.Close();

        }

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

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult mbResult = MessageBox.Show("Are you sure you want to delete all data?", "Clear Data", MessageBoxButton.YesNo);
            if (mbResult == MessageBoxResult.Yes)
            {
                BlApi.Factory.Get().Engineer.clear();
                BlApi.Factory.Get().Task.clear();
                BlApi.Factory.Get().User.clear();
                BlImplementation.Project.zeroStartProject();
            }
        }

        private void BtnStartProjectDate_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (BlImplementation.Project.getStartProject() == null)
            {
                new DatePickerWindow().ShowDialog();
                new MainWindow().Show();
                this.Close();
            }
            else
            if (BlImplementation.Project.getStage() == BO.Stage.MiddleStage)
            {
                try
                {
                    s_bl.Task.createAutomaticLuz();
                    MessageBox.Show("The  creation of the schdule were successfully updated", "success", MessageBoxButton.OK, MessageBoxImage.Information);
                    btn!.IsEnabled = false;
                }
                catch (BO.BlNotAtTheRightStageException ch)
                {
                    MessageBox.Show(ch.Message, "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
             
            }
               
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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