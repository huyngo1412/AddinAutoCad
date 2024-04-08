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
using AUTOREBAR.Model;
using AUTOREBAR.UserControlsAUTOREBAR;
namespace AUTOREBAR.View
{
    /// <summary>
    /// Interaction logic for MainWindowsAUTOREBAR.xaml
    /// </summary>
    public partial class MainWindowsAUTOREBAR : Window
    {
        private int boolOpenBeamUC = -1;
        private int boolOpenColumnUC = -1;
        public MainWindowsAUTOREBAR()
        {
            InitializeComponent();
            ViewModel.CommandModel ViewModel = new ViewModel.CommandModel();
            this.DataContext = ViewModel;
           
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.WindowState = WindowState.Minimized;
            }    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            UserControl usc = new UserControlsAUTOREBAR.MainUser();
            MainGrid.Children.Add(usc);
            Const.GridMain = MainGrid;
        }
        private void Btn_ImportETABS_MouseMove(object sender, MouseEventArgs e)
        {
            if(Tg_btn.IsChecked == true)
            {
                tt_btnImportETABS.Visibility = Visibility.Collapsed;
            }    
            else
            {
                tt_btnImportETABS.Visibility = Visibility.Visible;
            }    
        }

        //private void Btn_Material_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (Tg_btn.IsChecked == true)
        //    {
        //        tt_btnMaterial.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        tt_btnMaterial.Visibility = Visibility.Visible;
        //    }
        //}

        private void Btn_Beam_MouseMove(object sender, MouseEventArgs e)
        {
            if (Tg_btn.IsChecked == true)
            {
                tt_btnBeam.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_btnBeam.Visibility = Visibility.Visible;
            }
        }

        private void Btn_Column_MouseMove(object sender, MouseEventArgs e)
        {
            if (Tg_btn.IsChecked == true)
            {
                tt_btnColumn.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_btnColumn.Visibility = Visibility.Visible;
            }
        }

        private void Btn_Slab_MouseMove(object sender, MouseEventArgs e)
        {
            if (Tg_btn.IsChecked == true)
            {
                tt_btnSlab.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_btnSlab.Visibility = Visibility.Visible;
            }
        }

        //private void Btn_Material_Click(object sender, RoutedEventArgs e)
        //{
        //    if(Const.IsOpen==true)
        //    {
        //        MainGrid.Children.Clear();
        //        UserControl usc = new UserControlsAUTOREBAR.MaterialUserControls();
        //        MainGrid.Children.Add(usc);
        //    }    
        //    else
        //    {
        //        return;
        //    }    
        //}

        private void Btn_Beam_Click(object sender, RoutedEventArgs e)
        {
           
            if (Const.IsOpen == true)
            {
                MainGrid.Children.Clear();
                UserControl usc = new UserControlsAUTOREBAR.BeamUserControls();
                MainGrid.Children.Add(usc);
            }
            else
            {
                return;
            }
            boolOpenBeamUC += 1;
            if (boolOpenBeamUC >=1)
            {
                Const.isOpenBeamUC = true;
            }    
        }

        private void Btn_Column_Click(object sender, RoutedEventArgs e)
        {
            if(Const.IsOpen == true)
            {
                MainGrid.Children.Clear();
                UserControl usc = new UserControlsAUTOREBAR.ColumnUserControls();
                MainGrid.Children.Add(usc);
            }    
            else
            {
                return;
            }
            boolOpenColumnUC += 1;
            if(boolOpenColumnUC >=1)
            {
                Const.isOpenColumnUC = true;
            }    

        }
    }
}
