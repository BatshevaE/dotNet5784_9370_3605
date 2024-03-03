using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for taskToEnginner.xaml
    /// </summary>
    public partial class taskToEnginner : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerLevel Level { get; set; } = BO.EngineerLevel.None;
        public taskToEnginner(BO.Engineer eng)
        {
            InitializeComponent();
            TaskForEngList = (Level <= eng.Level) ?
            s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => (item.Copmlexity == Level))!;
        }
        public IEnumerable<BO.TaskInList> TaskForEngList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        // Using a DependencyProperty as the backing store for .  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskForEngList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));

    }
}
