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
using AUTOREBAR.Model;
using AUTOREBAR.ViewModel;
namespace AUTOREBAR.UserControlsAUTOREBAR
{
    /// <summary>
    /// Interaction logic for MaterialUserControls.xaml
    /// </summary>
    public partial class MaterialUserControls : UserControl
    {
        private CommandModel ViewModel { get; set; }
        public MaterialUserControls()
        {
            this.DataContext = ViewModel = new CommandModel();
            InitializeComponent();
        }

        private void GridControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement window = GetWindowParent(ucMaterial);
            var w = window as Window;
            if(w!=null)
            {
                w.DragMove();
            }
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

        private void Btn_material_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement window = GetWindowParent(ucMaterial);
            var w = window as Window;
            if (w != null)
            {
                w.WindowState = WindowState.Minimized;
            }
            Const.GridMain.Children.Clear();
            UserControl usc = new UserControlsAUTOREBAR.UserAutoReBarBeamOrColumn();
            Const.GridMain.Children.Add(usc);

        }
    }
}
