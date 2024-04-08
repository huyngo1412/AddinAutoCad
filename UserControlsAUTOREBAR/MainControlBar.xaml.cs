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

namespace AUTOREBAR.UserControls
{
    /// <summary>
    /// Interaction logic for MainControlBar.xaml
    /// </summary>
    public partial class MainControlBar : UserControl
    {
        public  ControlBarViewModel ViewModel { get; set; }
        public MainControlBar()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new ControlBarViewModel(); 
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement window = GetWindowParent(ucControlBar);
            var w = window as Window;
            if (w != null)
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
    }
}
