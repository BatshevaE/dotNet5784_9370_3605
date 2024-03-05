﻿using PL.Task;
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

        public UserMainWindow(int id)
        {
            InitializeComponent();
            eng = s_bl.Engineer.Read(id)!;
        }
        public BO.Engineer eng
        {
            get { return (BO.Engineer)GetValue(engProperty); }
            set { SetValue(engProperty, value); }
        }

        // Using a DependencyProperty as the backing store for eng.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty engProperty =
            DependencyProperty.Register("eng", typeof(BO.Engineer), typeof(UserMainWindow), new PropertyMetadata(null));

        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow(eng.Id).ShowDialog();    
        }

        private void BtnAssignOrWatch_Click(object sender, RoutedEventArgs e)
        {
            if(eng.Task==null)
            { 
                new taskToEnginner(eng).ShowDialog();
            }
            else 
            { 
                new TaskWindow(eng.Task.Item1).ShowDialog(); 
            }   
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            UserWindow s = new();
            s.Show();
        }
    }


  


}
