using BO;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for DependenciesListWindow.xaml
    /// </summary>
    public partial class DependenciesListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public DependenciesListWindow(int id)
        {
            InitializeComponent();
            Dependencies = s_bl.Task.Read(id)!.Dependencies!;
        }


        public IEnumerable<TaskInList> Dependencies
        {
            get { return (IEnumerable<TaskInList>)GetValue(dependenciesProperty); }
            set { SetValue(dependenciesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty dependenciesProperty =
            DependencyProperty.Register("Dependencies", typeof(IEnumerable<TaskInList>), typeof(DependenciesListWindow), new PropertyMetadata(null));

        private void BtnAddDependency_Click(object sender, RoutedEventArgs e)
        {
            DependencyWindow dependency = new();
            dependency.ShowDialog();
            this.Close();
        }

        private void UpdateDependency_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? dependency = (sender as ListView)?.SelectedItem as BO.TaskInList;
            DependencyWindow newDependency = new(dependency!.Id);
            newDependency.ShowDialog();
            this.Close();
        }
    }

}
