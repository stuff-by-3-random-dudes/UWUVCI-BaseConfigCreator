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
        private string ImportFileName = null;
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
                var fileName = "config";
                mvm.SaveBasesToFile(fileName);
                MessageBox.Show("New file saved to " + fileName + "\\" + ImportFileName);
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
            var ofd = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult res = ofd.ShowDialog();
             
            ImportFileName = System.IO.Path.GetFileName(ofd.FileName);

            if (res == System.Windows.Forms.DialogResult.OK)
                try
                {
                    mvm.ReadBases(ofd.FileName);
                } catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mvm.moveSelBaseUp();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mvm.moveSelBaseDown();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            var loopCount = -1;
            for (var i = 0; i < mvm.LGameBases.Count - 1; i++)
                if (mvm.SelectedGameBase.KeyHash == mvm.LGameBases[i].KeyHash)
                    loopCount = mvm.LGameBases.Count - 1 - i;

            mvm.TempBase = new GameBaseClassLibrary.GameBases()
            {
                Name = GameNameBox.Text,
                Tid = GameIdBox.Text
            };

            mvm.RemoveSelectedGameBase();
            mvm.AddBaseToList();

            mvm.SelectedGameBase = mvm.LGameBases[mvm.LGameBases.Count - 1];

            for (var i = 0; i < loopCount; i++)
                mvm.moveSelBaseUp();

            mvm.TempBase = new GameBaseClassLibrary.GameBases();

            Add.Visibility = Visibility.Visible;
            Update.Visibility = Visibility.Hidden;
            CancelUpdate.Visibility = Visibility.Hidden;
        }

        private void EditSelectedClick(object sender, RoutedEventArgs e)
        {
            var gameBase = mvm.SelectedGameBase;

            if (gameBase == null)
                return;

            GameNameBox.Text = gameBase.Name;
            GameIdBox.Text = gameBase.Tid;
            GameRegionBox.Text = gameBase.Region.ToString();

            Add.Visibility = Visibility.Hidden;
            Update.Visibility = Visibility.Visible;
            CancelUpdate.Visibility = Visibility.Visible;
        }

        private void CancelUpdateClick(object sender, RoutedEventArgs e)
        {
            mvm.TempBase = new GameBaseClassLibrary.GameBases();

            Add.Visibility = Visibility.Visible;
            Update.Visibility = Visibility.Hidden;
            CancelUpdate.Visibility = Visibility.Hidden;
        }
    }
}
