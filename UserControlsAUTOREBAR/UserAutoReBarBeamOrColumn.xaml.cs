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
    /// Interaction logic for UserAutoReBarBeamOrColumn.xaml
    /// </summary>
    public partial class UserAutoReBarBeamOrColumn : UserControl
    {
        private CommandModel ViewModel { get; set; }
        public UserAutoReBarBeamOrColumn()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new CommandModel();
        }



       
    }
}
