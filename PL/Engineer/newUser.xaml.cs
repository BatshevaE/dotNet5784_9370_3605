using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for newUser.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        /// <summary>
        /// ctor 
        /// </summary>
        public NewUserWindow()
        {
            InitializeComponent();
            CurrentUser = new();
        }
        public BO.User CurrentUser
        {
            get { return (BO.User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EngineerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("CurrentUser", typeof(BO.User), typeof(NewUserWindow), new PropertyMetadata(null));
        
        /// <summary>
        /// button to add user to the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (s_bl.User.Read(CurrentUser) != null) //check that the user doesn't exist
                {
                    MessageBox.Show("such user alresy assigned", "already assigned", MessageBoxButton.OK);
                    UserWindow s = new();
                    s.Show();
                    this.Close();
                }
  
                s_bl.User.Create(CurrentUser!);
                MessageBox.Show("successsfull create user", "succeeded", MessageBoxButton.OK);
                new UserWindow().ShowDialog();
                this.Close();
            }
            catch 
            {
                MessageBox.Show("such engineer does't exist in the system, in order to sign up you need to be siggned as member in the company","",MessageBoxButton.OK);
                UserWindow s = new();
                s.Show();
                this.Close();

            }
        }
    }
   
}
