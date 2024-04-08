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

namespace AUTOREBAR.UserControlsAUTOREBAR
{
    /// <summary>
    /// Interaction logic for ColumnUserControls.xaml
    /// </summary>
    public partial class ColumnUserControls : UserControl
    {
        private CommandModel ViewModel { get; set; }
        public ColumnUserControls()
        {
            this.DataContext = ViewModel = new CommandModel();
            InitializeComponent();
        }

      

        private void ListboxCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
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
                Const.choosecombocolumn.Add(item.ToString());
            }
            if(Const.choosecombocolumn.Count >0)
            {
                Const.isDataColumn = true;
            }    
        }

        private void ucColumn_Loaded(object sender, RoutedEventArgs e)
        {
            if(Const.isOpenColumnUC == true && Const.isDataColumn == true)
            {
                LsvDataColumn.Items.Clear();
                LsvDataColumn.ItemsSource = CommandModel.Column;
            }    
            else
            {
                return;
            }    
        }

        private void GridControls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement window = GetWindowParent(ucColumn);
            var w = window as Window;
            if(w!=null)
            {
                w.DragMove();
            }
        }
    }
}
