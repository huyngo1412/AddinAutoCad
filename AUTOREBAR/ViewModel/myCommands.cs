// (C) Copyright 2023 by  
//
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using AUTOREBAR.Model;
using AUTOREBAR.ViewModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;


// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(AUTOREBAR.MyCommands))]

namespace AUTOREBAR
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class Images
    {
        
        public static BitmapImage getBitmap(Bitmap image)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = stream;
            bmp.EndInit();
            return bmp;
        }
    }

    public class MyCommands
    {
        // The CommandMethod attribute can be applied to any public  member 
        // function of any public class.
        // The function should take no arguments and return nothing.
        // If the method is an intance member then the enclosing class is 
        // intantiated for each document. If the member is a static member then
        // the enclosing class is NOT intantiated.
        //
        // NOTE: CommandMethod has overloads where you can provide helpid and
        // context menu.

        // Modal Command with localized name
        private static Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        [CommandMethod("SendACommandToAutoCAD")]
        public static void SendACommandToAutoCAD()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            // Draws a circle and zooms to the extents or 
            // limits of the drawing
            acDoc.SendStringToExecute("._circle 2,2,0 4 ", true, false, false);
            acDoc.SendStringToExecute("._zoom _all ", true, false, false);
        }
        [CommandMethod("AUTOREBAR",CommandFlags.Transparent)]
        public void AutorebarOpen()
        {
            RibbonControl ribboncontrol = ComponentManager.Ribbon;

            //Tạo 1 RibbonTab

            RibbonTab ribbontab = new RibbonTab();
            //Thuộc tính của Ribbon
            ribbontab.Title = "Autorebar";
            ribbontab.Id = "AUTOREBAR";
            ribbontab.IsActive = true;

            ribboncontrol.Tabs.Add(ribbontab);
            ContentRibbon(ribbontab);
        }
        static void ContentRibbon(RibbonTab rbtab)
        {
            //Tạo 1 RibbonSource ETABS
            RibbonPanelSource ribbPanelETABS = new RibbonPanelSource();
            ribbPanelETABS.Title = "ETABS";
            //Tạo 1 RibbonButton Import File EDB
            RibbonButton ribbonbtnimportfileEDB = new RibbonButton();
            ribbonbtnimportfileEDB.Text = "OPEN ETABS";
            ribbonbtnimportfileEDB.Size = RibbonItemSize.Large;
            ribbonbtnimportfileEDB.CommandParameter = "RUNETABS";
            ribbonbtnimportfileEDB.ShowImage = true;
            ribbonbtnimportfileEDB.ShowText = true;
            ribbonbtnimportfileEDB.Orientation = System.Windows.Controls.Orientation.Vertical;
            ribbonbtnimportfileEDB.CommandHandler = new GetCommandAutoCad();
            ribbonbtnimportfileEDB.Image = Images.getBitmap(Properties.Resources.ETABSIcon);
            ribbonbtnimportfileEDB.LargeImage = Images.getBitmap(Properties.Resources.ETABSIcon);
            ribbPanelETABS.Items.Add(ribbonbtnimportfileEDB);

            RibbonPanel ribbonpanelimportfileEDB = new RibbonPanel();
            ribbonpanelimportfileEDB.Source = ribbPanelETABS;

            rbtab.Panels.Add(ribbonpanelimportfileEDB);

            //Tạo 1 RibbonSource Dầm 
            RibbonPanelSource ribbPanelBeam = new RibbonPanelSource();
            ribbPanelBeam.Title = "Dầm";
            //Tạo ribbonbutton Tính toán dầm 
            RibbonButton ribbBeam = new RibbonButton();
            ribbBeam.Text = "Tính toán dầm";
            ribbBeam.Size = RibbonItemSize.Large;
            ribbBeam.CommandParameter = "TINH_TOAN_DAM";
            ribbBeam.ShowImage = true;
            ribbBeam.ShowText = true;
            ribbBeam.Orientation = System.Windows.Controls.Orientation.Horizontal;
            ribbBeam.CommandHandler = new GetCommandAutoCad();
            ribbBeam.Image = Images.getBitmap(Properties.Resources.Beam);
            ribbBeam.LargeImage = Images.getBitmap(Properties.Resources.Beam);
            //Tạo ribbonbutton thống kê thép dầm
            RibbonButton ribbstatisticalbeam = new RibbonButton();
            ribbstatisticalbeam.Text = "Thống kê thép dầm";
            ribbstatisticalbeam.Size = RibbonItemSize.Large;
            ribbstatisticalbeam.CommandParameter = "THONG_KE_THEP_DAM";
            ribbstatisticalbeam.ShowImage = true;
            ribbstatisticalbeam.ShowText = true;
            ribbstatisticalbeam.Orientation = System.Windows.Controls.Orientation.Horizontal;
            ribbstatisticalbeam.CommandHandler = new GetCommandAutoCad();
            ribbstatisticalbeam.Image = Images.getBitmap(Properties.Resources.Statisticalbeam);
            ribbstatisticalbeam.LargeImage = Images.getBitmap(Properties.Resources.Statisticalbeam);
            //Tạo ribbonbutton vẽ tiết diện dầm
            RibbonButton ribbsectionbeam = new RibbonButton();
            ribbsectionbeam.Text = "Vẽ tiết diện dầm";
            ribbsectionbeam.Size = RibbonItemSize.Large;
            ribbsectionbeam.CommandParameter = "VE_TIET_DIEN_DAM";
            ribbsectionbeam.ShowImage = true;
            ribbsectionbeam.ShowText = true;
            ribbsectionbeam.Orientation = System.Windows.Controls.Orientation.Horizontal;
            ribbsectionbeam.CommandHandler = new GetCommandAutoCad();
            ribbsectionbeam.Image = Images.getBitmap(Properties.Resources.Statisticalbeam);
            ribbsectionbeam.LargeImage = Images.getBitmap(Properties.Resources.Statisticalbeam);
            //Tạo ribbonbutton vẽ biểu đồ bao vật liệu
            RibbonButton ribbmoment = new RibbonButton();
            ribbmoment.Text = "Vẽ biểu đồ bao dầm";
            ribbmoment.Size = RibbonItemSize.Large;
            ribbmoment.CommandParameter = "VE_BIEU_DO_BAO_DAM";
            ribbmoment.ShowImage = true;
            ribbmoment.ShowText = true;
            ribbmoment.Orientation = System.Windows.Controls.Orientation.Horizontal;
            ribbmoment.CommandHandler = new GetCommandAutoCad();
            ribbmoment.Image = Images.getBitmap(Properties.Resources.Statisticalbeam);
            ribbmoment.LargeImage = Images.getBitmap(Properties.Resources.Statisticalbeam);
            //Thêm vào RibbonRow
            RibbonRowPanel ribbonRow = new RibbonRowPanel();
            ribbonRow.Items.Add(ribbBeam);
            ribbonRow.Items.Add(new RibbonRowBreak());
            ribbonRow.Items.Add(ribbstatisticalbeam);
            ribbonRow.Items.Add(new RibbonRowBreak());
            ribbonRow.Items.Add(ribbsectionbeam);
            ribbonRow.Items.Add(new RibbonRowBreak());
            ribbonRow.Items.Add(ribbmoment);
            ribbonRow.Items.Add(new RibbonRowBreak());
            ribbPanelBeam.Items.Add(ribbonRow);
            RibbonPanel ribbonpanelBeam = new RibbonPanel();
            ribbonpanelBeam.Source = ribbPanelBeam;
            rbtab.Panels.Add(ribbonpanelBeam);
        }
        [CommandMethod("RUNETABS")]
        public void RunETABS()
        {
            CommandModel cmd = new CommandModel();
            cmd.OpenETABS();
            
        }
        [CommandMethod("TINH_TOAN_DAM")]
        public void Beam()
        {
            if(Const.isOpenETABS == false)
            {
                Application.ShowAlertDialog("Bạn chưa mở file ETABS");
            }    
        }
        [CommandMethod("THONG_KE_THEP_DAM")]
        public void Statisticalbeam()
        {

        }
        [CommandMethod("VE_TIET_DIEN_DAM")]
        public void Sectionbeam()
        {

        }
        [CommandMethod("VE_BIEU_DO_BAO_DAM")]
        public void DrawingMomenBeam()
        {

        }
    }

}
