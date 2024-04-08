using AUTOREBAR.Model;
using ETABSv1;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTOREBAR.ViewModel
{
    public class CommandModel
    {
        cSapModel SapModel;
        cOAPI EtabsObject;
        int ret = -1;
        cHelper myHelper = new Helper();

        public CommandModel()
        {
            cHelper myHelper = new Helper();
            EtabsObject = myHelper.CreateObjectProgID("CSI.ETABS.API.ETABSObject");
            SapModel = EtabsObject.SapModel;
        }
        public void OpenETABS()
        {
            cHelper myHelper = new Helper();
            EtabsObject = myHelper.CreateObjectProgID("CSI.ETABS.API.ETABSObject");
            SapModel = EtabsObject.SapModel;
            OpenFileDialog OpenEDB = new OpenFileDialog();
            OpenEDB.InitialDirectory = "C:\\";
            OpenEDB.Filter = " (*.EDB)|*.EDB|All Files (*.*)|*.*";
            OpenEDB.Multiselect = true;
            if (OpenEDB.ShowDialog() == true)
            {
                ret = EtabsObject.ApplicationStart();
                ret = SapModel.InitializeNewModel();
                ret = SapModel.File.OpenFile(OpenEDB.FileName);
                ret = SapModel.SetPresentUnits(eUnits.kN_m_C);
                Const.isOpenETABS = true;
            }
        }
    }
}
