using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace AUTOREBAR.ViewModel
{
    public class GetCommandAutoCad : ICommand
    {
        Editor editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {

            RibbonButton ribbonBtn = parameter as RibbonButton;
            if(ribbonBtn != null)
            {
                string commandstring = ribbonBtn.CommandParameter.ToString();
                Document acDoc = Application.DocumentManager.MdiActiveDocument;

                // Draws a circle and zooms to the extents or 
                // limits of the drawing
                acDoc.SendStringToExecute(commandstring, true, false, false);

            }
        }
        public event EventHandler CanExecuteChanged;
       

    }
}
