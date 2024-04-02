using PL.Engineer;
using PL.Task;
using System.Printing.IndexedProperties;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>

    public partial class UserWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        /// <summary>
        /// empty ctor
        /// </summary>
        public UserWindow()
        {
            InitializeComponent();
            CurrentUser = new BO.User();
        }

        /// <summary>
        /// dependency property of the current user
        /// </summary>
        public BO.User CurrentUser
        {
            get { return (BO.User)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentUserProperty =
            DependencyProperty.Register("CurrentUser", typeof(BO.User), typeof(UserWindow), new PropertyMetadata(null));
        /// <summary>
        /// a button to enter the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSignInClick(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentUser = s_bl.User.Read(CurrentUser!)!;
                if (CurrentUser != null)//if the user exist
                {
                    if (CurrentUser.IsManager)//if it's a manager
                    {
                        MainWindow main = new();
                        main.Show();
                        this.Close();
                    }
                    else//the user is a regulat engineer
                    {
                        new UserMainWindow(CurrentUser.Id).Show();
                        this.Close();
                    }
                }
                else//the user doesn't exist
                {
                    MessageBox.Show("User doesn't exist", "try again", MessageBoxButton.OK);
                    new UserWindow().Show();
                    this.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                new UserWindow().Show();
                this.Close();
            };
        }
        /// <summary>
        /// creating a new user by openning a window that the engineer will fill the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new NewUserWindow().Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); };

        }
        /// <summary>
        /// if we did clear this button if for the next running
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInitialize_Click(object sender, RoutedEventArgs e)
        {
            BlApi.Factory.Get().Engineer.Clear();
            BlApi.Factory.Get().Task.Clear();
            BlApi.Factory.Get().User.Clear();
            BlImplementation.Project.ZeroStartProject();
            DalTest.Initialization.Do();

        }


    }
}
