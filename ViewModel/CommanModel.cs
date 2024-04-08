
using AUTOREBAR.Model;
using ETABSv1;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using AUTOREBAR.UserControlsAUTOREBAR;

namespace AUTOREBAR.ViewModel
{
    public class CommandModel
    {
        public static string chooseOrientation;
        public static bool isApply = false;
        private bool isOpen = false;
        private bool isBeamModel = false;
        private static List<string> nameMaterialConcrete = new List<string>();
        public static List<string> NameMaterialConcrete
        {
            get { return nameMaterialConcrete; }
            set { nameMaterialConcrete = value; }
        }
        private static List<string> nameMaterialRebar = new List<string>();
        public static List<string> NameMaterialRebar
        {
            get { return nameMaterialRebar; }
            set { nameMaterialRebar = value; }
        }
        private static List<SectionBeam> sectionsbeam = new List<SectionBeam>();
        public static List<SectionBeam> SectionsBeam
        {
            get { return sectionsbeam; }
            set { sectionsbeam = value; }
        }
        private CollectionView myCollectionViewbeam;
        private CollectionView myCollectionViewcolumn;
        private List<SectionColumn> sectionscolumn = new List<SectionColumn>();
        public List<SectionColumn> SectionColumn
        {
            get { return sectionscolumn; }
            set { sectionscolumn = value; }
        }
        private static List<ColumnProperties> column = new List<ColumnProperties>();
        public static List<ColumnProperties> Column
        {
            get { return column; }
            set { column = value; }
        }
        private static List<BeamProperties> beam = new List<BeamProperties>();
        public static List<BeamProperties> Beam
        {
            get { return beam; }
            set { beam = value; }
        }

        private static int[] rebar = new int[] { 6, 8, 12, 14, 16, 18, 20, 22, 25, 28, 30, 32, 36, 40 };
        public static int[] Rebar
        {
            get { return rebar; }
            set { rebar = value; }
        }
        private static ObservableCollection<string> nameBeam = new ObservableCollection<string>();
        public static ObservableCollection<string> NameBeam
        {
            get { return nameBeam; }
            set { nameBeam = value; }

        }
        private List<String> ChooseCombo = new List<string>();
        private ObservableCollection<string> nameColumn = new ObservableCollection<string>();
        public ObservableCollection<string> NameColumn
        {
            get { return nameColumn; }
            set { nameColumn = value; }
        }
        private static string[] namecombo = new string[] { };
        public static string[] NameCombo
        {
            get { return namecombo; }
            set { namecombo = value; }
        }
        
        public ICommand OpenEDB { get; set; }
        public ICommand ApplyMaterial { get; set; }
        public ICommand BeamUserData { get; set; }
        public ICommand ColumnUserData { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand ImportAutoCad { get; set; }

        #region Parmeter ETABS
        private eFrameDesignOrientation[] myenumOrientation = (eFrameDesignOrientation[])Enum.GetValues(typeof(eFrameDesignOrientation));
        public eFrameDesignOrientation[] MyEnumOrientation
        {
            get { return myenumOrientation; }
            set { myenumOrientation = value; }
        }
        cSapModel SapModel;
        cOAPI EtabsObject;
        int ret = -1;

        #endregion
        public CommandModel()
        {
            myCollectionViewbeam = (CollectionView)CollectionViewSource.GetDefaultView(beam);
            BindingOperations.EnableCollectionSynchronization(beam, new object());
            myCollectionViewcolumn = (CollectionView)CollectionViewSource.GetDefaultView(column);
            BindingOperations.EnableCollectionSynchronization(column, new object());
            cHelper myHelper = new Helper();
            EtabsObject = myHelper.CreateObjectProgID("CSI.ETABS.API.ETABSObject");
            SapModel = EtabsObject.SapModel;
            CloseWindowCommand = new BaseCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
              {
                  FrameworkElement window = GetWindowParent(p);
                  var w = window as Window;
                  if (w != null)
                  {
                      w.Close();
                  }
              });
            OpenEDB = new BaseCommand<Window>((p) => true, (p) =>
            {

                OpenFileDialog OpenEDB = new OpenFileDialog();
                OpenEDB.InitialDirectory = "C:\\";
                OpenEDB.Filter = " (*.EDB)|*.EDB|All Files (*.*)|*.*";
                OpenEDB.Multiselect = true;
                var window = p as Window;
                window.WindowState = WindowState.Minimized;
                if (OpenEDB.ShowDialog() == true)
                {
                    isOpen = true;
                    Const.filename = OpenEDB.FileName;
                    METHOD();
                    Const.IsOpen = true;
                    window.WindowState = WindowState.Normal;
                }
                
            });
            BeamUserData = new BaseCommand<ListView>((p) => true, (p) =>
               {
                   beam.RemoveAll(beam => !Const.choosecombobeam.Contains(beam.nameLoadCombo));
                   p.ItemsSource = beam;
               });
            ColumnUserData = new BaseCommand<ListView>((p) => true, (p) =>
            {
                column.RemoveAll(column => !Const.choosecombocolumn.Contains(column.nameLoadCombo));
                p.ItemsSource = column;
            });
            ApplyMaterial = new BaseCommand<UIElementCollection>((p) => true, (p) =>
               {
                   foreach (var item in p)
                   {
                       ComboBox combobox = item as ComboBox;
                       if (combobox == null)
                       {
                           continue;
                       }
                       else
                       {
                           switch (combobox.Name)
                           {
                               case "Cb_LongitudinalBars":
                                   foreach (Const.RsRebar RsRebar in Enum.GetValues(typeof(Const.RsRebar)))
                                   {
                                       if (combobox.Text == RsRebar.ToString())
                                       {
                                           Const.FyLong = (int)RsRebar;
                                       }
                                   }
                                   continue;
                               case "Cb_ConfinementBars":
                                   Const.Rsw = Method.ReturnRsw(combobox.Text);
                                   continue;
                               case "Cb_Concrete":
                                   foreach (Const.RbtConcrete RbtConcrete in Enum.GetValues(typeof(Const.RbtConcrete)))
                                   {
                                       if (combobox.Text == RbtConcrete.ToString())
                                       {
                                           Const.Rbt = (int)RbtConcrete;
                                       }
                                   }
                                   foreach (Const.RbConcrete RbConcrete in Enum.GetValues(typeof(Const.RbConcrete)))
                                   {
                                       if (combobox.Text == RbConcrete.ToString())
                                       {
                                           Const.Fc = (int)RbConcrete;
                                       }
                                   }
                                   continue;
                               case "Cb_CoverRebar":
                                   Const.CoverForConfinementBar = int.Parse(combobox.Text);
                                   continue;
                               case "CbConfinementRebarDiameter":
                                   Const.ConfinementBarDiameter = int.Parse(combobox.Text);
                                   continue;
                               default:
                                   break;
                           }
                       }
                   }
                   AutoRebar();
                   ReturnSectionBeam();
               });
            ImportAutoCad = new BaseCommand<object>((p) => true, (p) => 
            {
                MyCommands.AUTOREBARMODEL();
            });
        }
        private void METHOD()
        {
            if (isOpen == true)
            {
                double g = 0, v = 0;
                ret = EtabsObject.ApplicationStart();
                ret = SapModel.InitializeNewModel();
                ret = SapModel.File.OpenFile(Const.filename);
                ret = SapModel.SetPresentUnits(eUnits.kN_m_C);
                ret = SapModel.RespCombo.GetNameList(ref Const.number, ref namecombo);
                ret = SapModel.SetModelIsLocked(false);
                ret = SapModel.FrameObj.GetNameList(ref Const.number, ref Const.frame);
                for (int j = 0; j < Const.frame.Length; j++)
                {
                    ret = SapModel.FrameObj.GetDesignOrientation(Const.frame[j], ref Const.TypeFrame);
                    if (Const.TypeFrame == eFrameDesignOrientation.Beam)
                    {
                        nameBeam.Add(Const.frame[j]);

                    }
                    if (Const.TypeFrame == eFrameDesignOrientation.Column)
                    {
                        nameColumn.Add(Const.frame[j]);
                    }
                }
                for (int i = 0; i < namecombo.Length; i++)
                {
                    ret = SapModel.Results.Setup.SetComboSelectedForOutput(namecombo[i]);
                    ret = SapModel.Analyze.RunAnalysis();
                    ret = SapModel.FrameObj.GetNameList(ref Const.number, ref Const.frame);
                    chooseOrientation = "Beam";
                    for (int j = 0; j < Const.frame.Length; j++)
                    {
                        ret = SapModel.FrameObj.GetLoadDistributed(Const.frame[j], ref Const.NumberItems, ref Const.FrameName, ref Const.LoadPat, ref Const.MyType, ref Const.CSys, ref Const.Dir, ref Const.RD1, ref Const.RD2, ref Const.Dist1, ref Const.Dist2, ref Const.Val1, ref Const.Val2, Const.ItemType);
                        for (int e = 0; e < Const.Val1.Length; e++)
                        {
                            ret = SapModel.LoadPatterns.GetLoadType(Const.LoadPat[e], ref Const.mytype);
                            if (Const.mytype == eLoadPatternType.Dead)
                            {
                                g = Math.Abs(Const.Val1[e]);
                            }
                            if (Const.mytype == eLoadPatternType.Live)
                            {
                                v = Math.Abs(Const.Val1[e]);
                            }
                        }
                        ret = SapModel.FrameObj.GetDesignOrientation(Const.frame[j], ref Const.TypeFrame);
                        ret = SapModel.PropFrame.GetRectangle(Const.frame[j], ref Const.filename, ref Const.Mat, ref Const.T3, ref Const.T2, ref Const.Color, ref Const.Notes, ref Const.GUID);
                        ret = SapModel.Results.FrameForce(Const.frame[j], eItemTypeElm.ObjectElm, ref Const.NumberResults, ref Const.Obj, ref Const.ObjSta, ref Const.Elm, ref Const.ElmSta, ref Const.LoadCase, ref Const.StepType, ref Const.StepNum, ref Const.P, ref Const.V2, ref Const.V3, ref Const.T, ref Const.M2, ref Const.M3);
                        for (int z = 0; z < Const.V2.Length; z++)
                        {
                            if (Const.TypeFrame.ToString() == "Beam")
                            {
                                BeamProperties beamItem = new BeamProperties() { nameLoadCombo = Const.LoadCase[z], frameName = Const.frame[j], height = Const.T3, width = Const.T2, stepType = Const.StepType[z], station = Const.ElmSta[z], p = Const.P[z], m = Const.M3[z], q = Const.V2[z] ,g = g,v=v};
                                beam.Add(beamItem);
                            }
                            if (Const.TypeFrame.ToString() == "Column")
                            {
                                ColumnProperties columnItem = new ColumnProperties() { nameLoadCombo = Const.LoadCase[z], frameName = Const.frame[j], height = Const.T3, width = Const.T2, stepType = Const.StepType[z], station = Const.ElmSta[z], p = Const.P[z], m = Const.M3[z], q = Const.V2[z] };
                                column.Add(columnItem);
                            }
                        }
                    }
                }
               
            }        
        }
        private void ReturnSectionBeam()
        {
            //foreach (var item in nameBeam)
            //{
            //    double Width = beam.Where(x => x.frameName == item).Select(x => x.width).FirstOrDefault();
            //    double Height = beam.Where(x => x.frameName == item).Select(x => x.height).FirstOrDefault();
            //    double Length = beam.Where(x => x.frameName == item).Max(x => x.station);
            //    Method.ReturnSection(item, "MC1-1", Width, Height, Length);
            //    Method.ReturnSection(item, "MC2-2", Width, Height, Length);
            //    Method.ReturnSection(item, "MC3-3", Width, Height, Length);
            //}
            foreach (var item in Method.beams)
            {
                sectionsbeam.Add(item);
            }
            MessageBox.Show(sectionsbeam.Count.ToString());
            
        }
        private void AutoRebar()
        {
            int IDSection = 0;
            foreach (var item in nameBeam)
            {

                double firstpoint = beam.Where(x => x.frameName == item).Min(x => x.station);
                double lastpoint = beam.Where(x => x.frameName == item).Max(x => x.station);
                double MomenFirstPointTopST = beam.Where(x => x.frameName == item && x.station == firstpoint).Min(x => x.m);
                double MomenFirstPointBottomST = beam.Where(x => x.frameName == item && x.station == firstpoint).Max(x => x.m);
                double MomenLastPointTopST = beam.Where(x => x.frameName == item && x.station == lastpoint).Min(x => x.m);
                double MomenLastPointBottomST = beam.Where(x => x.frameName == item && x.station == lastpoint).Max(x => x.m);
                double MomenCenterPointTopST = beam.Where(x => x.frameName == item && x.station == lastpoint / 2).Min(x => x.m);
                double MomenCenterPointBottomST = beam.Where(x => x.frameName == item && x.station == lastpoint / 2).Max(x => x.m);
                double QFirstPoint = beam.Where(x => x.frameName == item && x.station == firstpoint).Max(x => Math.Abs(x.q));
                double QLastPoint = beam.Where(x => x.frameName == item && x.station == lastpoint).Max(x => Math.Abs(x.q));
                double QCenterPoint = beam.Where(x => x.frameName == item && x.station == lastpoint / 2).Max(x => Math.Abs(x.q));
                double v = beam.Where(x => x.frameName == item).Max(x => x.v);
                double g = beam.Where(x => x.frameName == item).Max(x => x.g);
                double Q1 = g + (v / 2);
                double Width = beam.Where(x => x.frameName == item).Select(x => x.width).FirstOrDefault();
                double Height = beam.Where(x => x.frameName == item).Select(x => x.height).FirstOrDefault();
                //FirstPoint
                if (MomenFirstPointTopST < 0 && MomenLastPointBottomST < 0)
                {
                    Method.SectionA(item, "MC1-1", Width, Height, lastpoint, Math.Min(MomenCenterPointBottomST, MomenCenterPointTopST), 0, QFirstPoint, Q1);
                }
                else if (MomenFirstPointTopST < 0 && MomenLastPointBottomST > 0)
                {
                    Method.SectionA(item, "MC1-1", Width, Height, lastpoint, MomenFirstPointTopST, MomenLastPointBottomST, QFirstPoint, Q1);
                }
                else
                {
                    Method.SectionA(item, "MC1-1", Width, Height, lastpoint, 0, Math.Max(MomenCenterPointBottomST, MomenCenterPointTopST), QFirstPoint, Q1);
                }
                //CenterPoint
                if (MomenCenterPointTopST < 0 && MomenCenterPointBottomST < 0)
                {
                    Method.SectionA(item, "MC2-2", Width, Height, lastpoint, Math.Min(MomenCenterPointBottomST, MomenCenterPointTopST), 0, QCenterPoint, Q1);
                }
                else if (MomenCenterPointTopST < 0 && MomenCenterPointBottomST > 0)
                {
                    Method.SectionA(item, "MC2-2", Width, Height, lastpoint, MomenCenterPointTopST, MomenCenterPointBottomST, QFirstPoint, Q1);
                }
                else
                {
                    Method.SectionA(item, "MC2-2", Width, Height, lastpoint, 0, Math.Max(MomenCenterPointBottomST, MomenCenterPointTopST), QCenterPoint, Q1);
                }
                //LastPoint
                if (MomenLastPointTopST < 0 && MomenLastPointBottomST < 0)
                {
                    Method.SectionA(item, "MC3-3", Width, Height, lastpoint, Math.Min(MomenLastPointBottomST, MomenLastPointTopST), 0, QLastPoint, Q1);
                }
                else if (MomenCenterPointTopST < 0 && MomenCenterPointBottomST > 0)
                {
                    Method.SectionA(item, "MC3-3", Width, Height, lastpoint, MomenLastPointTopST, MomenLastPointBottomST, QLastPoint, Q1);
                }
                else
                {
                    Method.SectionA(item, "MC3-3", Width, Height, lastpoint, 0, Math.Max(MomenLastPointBottomST, MomenLastPointTopST), QLastPoint, Q1);
                }
                
            }
        }

        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            try
            {
                while (parent.Parent != null)
                {
                    parent = parent.Parent as FrameworkElement;

                }
                return parent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                throw ex;
            }
            
        }
       
    }
}
