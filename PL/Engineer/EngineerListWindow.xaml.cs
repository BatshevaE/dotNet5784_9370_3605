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
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        /// <summary>
        /// ctor
        /// </summary>
        public EngineerListWindow()
        {
            InitializeComponent();
            EngineerList = (Level == BO.EngineerLevel.None) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == Level)!;

        }
        /// <summary>
        /// dependency property
        /// </summary>
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EngineerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));
        /// <summary>
        /// enum for the level of the engineer
        /// </summary>
        public BO.EngineerLevel Level { get; set; } = BO.EngineerLevel.None;
        /// <summary>
        /// combobox of the levels of the engineeer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbEngineerLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (Level == BO.EngineerLevel.None) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == Level)!;


        }
        /// <summary>
        /// the add button-here we dont send any id so the window of the single engineer will bw open with id=0 and will know that we are on add mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            EngineerWindow eng = new();
            eng.ShowDialog();
            this.Close();
        }
        /// the update -here we double click on the engineer we want to update,and we send the id (of the engineer we just click) to the window of the single engineer 
        private void DoubleClicItem(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;

            EngineerWindow eng = new(engineer!.Id);
            eng.ShowDialog();
            this.Close();
            
        }
    }
}
