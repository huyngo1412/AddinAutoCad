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
using AUTOREBAR.ViewModel;
using AUTOREBAR.Model;
using System.Reflection;
using Autodesk.AutoCAD.DatabaseServices;

namespace AUTOREBAR.UserControlsAUTOREBAR
{
    /// <summary>
    /// Interaction logic for BeamUserControls.xaml
    /// </summary>
    public partial class BeamUserControls : UserControl
    {
        private CommandModel ViewModel { get; set; }
        public BeamUserControls()
        {
            this.DataContext = ViewModel = new CommandModel();            InitializeComponent();
        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;

            }
            return parent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (var item in ListboxCombo.SelectedItems)
            {
                Const.choosecombobeam.Add(item.ToString());
            }
            if(Const.choosecombobeam.Count > 0)
            {
                Const.isDataBeam = true;
            }    
        }


        private void ucBeam_Loaded(object sender, RoutedEventArgs e)
        {

            Cb_Color_Concrete.ItemsSource = typeof(Colors).GetProperties();
            Cb_Color_Rebar.ItemsSource = typeof(Colors).GetProperties();
            Cb_Color_TextDim.ItemsSource = typeof(Colors).GetProperties();
            Cb_Color_Dim.ItemsSource = typeof(Colors).GetProperties();
            foreach (var item in Enum.GetValues(typeof(LineWeight)))
            {
                Cb_LineWeight_Concrete.Items.Add(((double)item).ToString("0.00"));
                Cb_LineWeight_Rebar.Items.Add(((double)item).ToString("0.00"));
            }
        }

        private void GridControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement window = GetWindowParent(ucBeam);
            var w = window as Window;
            if(w!=null)
            {
                w.DragMove();
            }
        }
        private void Cb_Color_Concrete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color color = (Color)(Cb_Color_Concrete.SelectedItem as PropertyInfo).GetValue(1, null);
            Cb_Color_Concrete.Foreground = new SolidColorBrush(color);
            Cb_Concrete.Foreground = new SolidColorBrush(color);
            Cb_LineType_Concrete.Foreground  = new SolidColorBrush(color); ;
            Cb_LineWeight_Concrete.Foreground = new SolidColorBrush(color);
        }

        private void Cb_Color_Rebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color color = (Color)(Cb_Color_Rebar.SelectedItem as PropertyInfo).GetValue(1, null);
            Cb_Color_Rebar.Foreground = new SolidColorBrush(color);
            Cb_LongitudialBar.Foreground = new SolidColorBrush(color);
            Cb_ConfinementBar.Foreground = new SolidColorBrush(color);
            Cb_LineType_Rebar.Foreground = new SolidColorBrush(color);
            Cb_LineWeight_Rebar.Foreground = new SolidColorBrush(color);
        }

        private void Cb_Color_Dim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color color = (Color)(Cb_Color_Dim.SelectedItem as PropertyInfo).GetValue(1, null);
            Cb_Color_Dim.Foreground = new SolidColorBrush(color);
        }

        private void Cb_Color_TextDim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color color = (Color)(Cb_Color_TextDim.SelectedItem as PropertyInfo).GetValue(1, null);
            Cb_Color_TextDim.Foreground = new SolidColorBrush(color);
        }
    }
}
