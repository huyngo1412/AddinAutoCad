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

namespace AUTOREBAR.UserControlsAUTOREBAR
{
    /// <summary>
    /// Interaction logic for MainUser.xaml
    /// </summary>
    public partial class MainUser : UserControl
    {
        public MainUser()
        {
            InitializeComponent();
        }

        private void GridControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement window = GetWindowParent(ucMain);
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
    }
}
