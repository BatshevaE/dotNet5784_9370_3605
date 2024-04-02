using PL.Task;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// ctor gets an id of engineer if we update an existing engineer and will be 0 if we want to create a new engineer
        /// </summary>
        /// <param name="id">id of engineer/0</param>
        public EngineerWindow(int id=0)
        {
            InitializeComponent();
            if (id == 0)//we click  the add button
            {
                CurrentEngineer = new BO.Engineer();
            }
            else//we click one of the engineers in the list on the window
            {
                try
                {
                   CurrentEngineer = s_bl.Engineer.Read(id)!;
                    
                }
                catch (BO.BlDoesNotExistException ch) { MessageBox.Show(ch.Message, "failed", MessageBoxButton.OK); }
            }

        }

        /// <summary>
        /// dependency property to the currrent engineer
        /// </summary>
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EngineerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
        /// <summary>
        /// the button of add/update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (s_bl.Engineer.ReadAll().FirstOrDefault(m => m.Id == CurrentEngineer!.Id) == null)//if there is not an engineer with such an id-we are on add mode
                {
                    s_bl.Engineer.Create(CurrentEngineer!);
                    MessageBox.Show("successsfull create engineer", "succeeded", MessageBoxButton.OK);
                    this.Close();
                   
                }
                else//there is  an engineer with such an id-we are on update mode
                {
                    s_bl.Engineer.Update(CurrentEngineer!);
                    MessageBox.Show("successsfull update engineer", "succeedes", MessageBoxButton.OK);
                    this.Close();

                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error); 
                Close();
            }

        }
        /// <summary>
        /// a button to delete an existing enginner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>     
        private void BtnDeleteEngineer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Engineer.Delete(CurrentEngineer.Id);
                MessageBox.Show($"Engineer with Id:{CurrentEngineer.Id} successfuly deleted", "Deletd", MessageBoxButton.OK);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        /// <summary>
        /// a button to a window with a list of all the task that the engineer can do
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAssignToTask_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
            new taskToEnginner(CurrentEngineer).ShowDialog();
             this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        /// <summary>
        /// a button to return to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {          
            this.Close();
        }
    }
}
