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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UWUVCI_BaseConfigCreator.Classes.Exceptions;

namespace UWUVCI_BaseConfigCreator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        MainViewModel mvm = null;
        public MainWindow()
        {
            mvm = (MainViewModel)FindResource("mvm");
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mvm.AddBaseToList();
            }
            catch (GameBaseException ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateGrid();
        }
        

        private void dgGameBases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                mvm.RemoveSelectedGameBase();
            }
            catch (GameBaseException ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateGrid();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mvm.SaveBasesToFile();
            }
            catch (GameBaseException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateGrid()
        {
            dgGameBases.ItemsSource = null;
            dgGameBases.ItemsSource = mvm.LGameBases;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mvm.ReadBases();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mvm.moveSelBaseUp();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mvm.moveSelBaseDown();
        }
    }
}
