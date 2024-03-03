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
               CurrentUser = s_bl.User.Read(CurrentUser.Password)!;     
                MainWindow main=new ();   
                main.Show();    
                this.Close();   
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); } ; 
            
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.User.Create(CurrentUser);
                MainWindow main = new ();
                main.Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); };

        }

        private void BtnInitialize_Click(object sender, RoutedEventArgs e)
        {
            DalTest.Initialization.Do();

        }
    }
}
