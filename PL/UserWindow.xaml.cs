﻿using System;
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
               CurrentUser = (s_bl.User.Read(CurrentUser.Id)!);     
                MainWindow main=new MainWindow();   
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
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); };

        }
    }
}
