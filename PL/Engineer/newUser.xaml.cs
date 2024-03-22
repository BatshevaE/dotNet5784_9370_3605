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
    public partial class newUser : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public newUser(int pasword)
        {
            InitializeComponent();
            //
            CurrentUser = new();
            pas= pasword;
            //CurrentUser.Password = pasword;
        }
        public BO.User CurrentUser
        {
            get { return (BO.User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EngineerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("CurrentUser", typeof(BO.User), typeof(newUser), new PropertyMetadata(null));
        public int pas
        {
            get { return (int)GetValue(UseProperty); }
            set { SetValue(UseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EngineerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseProperty =
            DependencyProperty.Register("pas", typeof(int), typeof(newUser), new PropertyMetadata(null));

        private void BtnAddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.User.Create(CurrentUser!);
                MessageBox.Show("successsfull create user", "succeeded", MessageBoxButton.OK);
                new MainWindow().ShowDialog();
                this.Close();
            }
            catch
            {
                MessageBox.Show("you canot add youtself to the system,please talk to the manager","",MessageBoxButton.OK);

            }
        }
    }
   
}
