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

namespace PL
{
    /// <summary>
    /// Interaction logic for DatePickerWindow.xaml
    /// </summary>
    public partial class DatePickerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public DatePickerWindow(int id=0)
        {
            InitializeComponent();
            Clock=s_bl.Clock.Date;
            Id = id;
        }


        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(DatePickerWindow), new PropertyMetadata(0));


        public DateTime Clock
        {
            get { return (DateTime)GetValue(ClockProperty); }
            set { SetValue(ClockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Clock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClockProperty =
            DependencyProperty.Register("Clock", typeof(DateTime), typeof(DatePickerWindow), new PropertyMetadata(null));



        private void DatePicker_Select(object sender,SelectionChangedEventArgs e)
        {
            var picker=sender as DatePicker;
            DateTime? date= picker!.SelectedDate;
            if ((Id==0)&&(date!=null))
            {
                try
                {
                    BlImplementation.Project.CreateSchedele(date);
                    MessageBox.Show("The project start date has been successfully updated", "start project", MessageBoxButton.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            else 
            {
                try { s_bl.Task.UpdateActuallStartDate(date, Id); this.Close(); }
                catch { }
            }

        }
    }

}
