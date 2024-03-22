using PL.Engineer;
using PL.Task;
using System.Printing.IndexedProperties;
using System.Text;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    
    public partial class UserWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public UserWindow()
        {
            InitializeComponent();
            CurrentUser = new BO.User();    
        }


        public BO.User CurrentUser
        {
            get { return (BO.User)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentUserProperty =
            DependencyProperty.Register("CurrentUser", typeof(BO.User), typeof(UserWindow), new PropertyMetadata(null));

        private void BtnSignInClick(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentUser = s_bl.User.Read(CurrentUser)!;
                if(CurrentUser == null) { MessageBox.Show("such user doen't exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error); UserWindow s = new();s.Show();
                    this.Close();
                    return;
                }
                if (CurrentUser!.IsManager)
                {
                    MainWindow main = new();
                    main.Show();
                    this.Close();
                }
                else
                {
                    new UserMainWindow(CurrentUser.Id).Show();
                    this.Close();
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); } ; 
            
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new newUser(CurrentUser.Password).Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); };

        }

        private void BtnInitialize_Click(object sender, RoutedEventArgs e)
        {
             BlApi.Factory.Get().Engineer.clear();
                BlApi.Factory.Get().Task.clear();
                BlApi.Factory.Get().User.clear();
                BlImplementation.Project.zeroStartProject();
            DalTest.Initialization.Do();

        }

      
    }
}
