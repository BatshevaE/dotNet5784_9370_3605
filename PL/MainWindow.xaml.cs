using DalApi;
using PL.Engineer;
using PL.Task;
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
            CurrentTime = s_bl.Clock;
        }


        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(null));


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
           MessageBoxResult mbResult= MessageBox.Show("Are you sure you want to initialize the data?","Initialize Data",MessageBoxButton.YesNo);
            if(mbResult == MessageBoxResult.Yes)
            {
                BlApi.Factory.Get().Engineer.clear();
                DalTest.Initialization.Do(); }

            }

        private void BtnTaskClick(object sender, RoutedEventArgs e)
        {
            TaskListWindow task = new();
            task.Show();    
        }
    }
}