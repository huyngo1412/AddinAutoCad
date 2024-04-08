// (C) Copyright 2023 by  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows.Data;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System.Xml.Serialization;
using Autodesk.AutoCAD.Colors;
using AUTOREBAR.ViewModel;
using AUTOREBAR.Model;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(AUTOREBAR.MyCommands))]

namespace AUTOREBAR
{

    public class MyCommands
    {
        private static Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        private static string sLayerNameConcrete = "Concrete";
        private static string sLayerNameConfinementBar = "ConfinementBar";
        private static string sLayerNameLongitudinalBar = "LongitudinalBar";
        private static string sLayerNameTextNameSection = "Section";
        [CommandMethod("AUTOREBAR")]
        public void AUTOREBAR()
        {
            View.MainWindowsAUTOREBAR f = new View.MainWindowsAUTOREBAR();
            Application.ShowModalWindow(Application.MainWindow.Handle, f);
        }
        public static void AUTOREBARMODEL()
        {
            PromptPointOptions PointObject = new PromptPointOptions("Chọn 1 điểm bất kì");
            PromptPointResult ValuePoint = ed.GetPoint(PointObject);
            double ValuePointSection = ValuePoint.Value.X;
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;         
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;
                if (!acLyrTbl.Has(sLayerNameConcrete))
                {
                    using (LayerTableRecord acLyrTblRec = new LayerTableRecord())
                    {
                        acLyrTblRec.Name = sLayerNameConcrete;
                        acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByAci, 7); 
                        acLyrTbl.UpgradeOpen();
                        acLyrTbl.Add(acLyrTblRec);
                        acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                    }
                }
                if (acLyrTbl.Has(sLayerNameLongitudinalBar) == false)
                {
                    LayerTableRecord acLyrTblRec = new LayerTableRecord();
                    acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByAci, 6);
                    acLyrTblRec.Name = sLayerNameLongitudinalBar;
                    acLyrTbl.UpgradeOpen();
                    acLyrTbl.Add(acLyrTblRec);
                    acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                }
                if (acLyrTbl.Has(sLayerNameConfinementBar) == false)
                {
                    LayerTableRecord acLyrTblRec = new LayerTableRecord();
                    acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByAci, 1);
                    acLyrTblRec.Name = sLayerNameConfinementBar;
                    acLyrTbl.UpgradeOpen();
                    acLyrTbl.Add(acLyrTblRec);
                    acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                }
                if (acLyrTbl.Has(sLayerNameTextNameSection) == false)
                {
                    LayerTableRecord acLyrTblRec = new LayerTableRecord();
                    acLyrTblRec.Color = Color.FromColorIndex(ColorMethod.ByAci, 160);
                    acLyrTblRec.Name = sLayerNameTextNameSection;
                    acLyrTbl.UpgradeOpen();
                    acLyrTbl.Add(acLyrTblRec);
                    acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                }
                using (BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable)
                {
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    foreach (var item in CommandModel.SectionsBeam)
                    {
                        double bf = (item.Width + 2 * (item.Lenght / 6))*1000;
                        double bof = 1000*(item.Lenght / 6);
                        using (Polyline Concrete = new Polyline())
                        {
                            Concrete.SetDatabaseDefaults();
                            Concrete.AddVertexAt(0, new Point2d(ValuePointSection, ValuePoint.Value.Y), 0, 0, 0);
                            Concrete.AddVertexAt(1, new Point2d(ValuePointSection + bf, ValuePoint.Value.Y), 0, 0, 0);
                            Concrete.AddVertexAt(2, new Point2d(ValuePointSection + bf, ValuePoint.Value.Y - 100), 0, 0, 0);
                            Concrete.AddVertexAt(3, new Point2d(ValuePointSection + bf - bof, ValuePoint.Value.Y - 100), 0, 0, 0);
                            Concrete.AddVertexAt(4, new Point2d(ValuePointSection + bf - bof, ValuePoint.Value.Y - (item.Height * 1000)), 0, 0, 0);
                            Concrete.AddVertexAt(5, new Point2d(ValuePointSection + bof, ValuePoint.Value.Y - (item.Height * 1000)), 0, 0, 0);
                            Concrete.AddVertexAt(6, new Point2d(ValuePointSection + bof, ValuePoint.Value.Y - 100), 0, 0, 0);
                            Concrete.AddVertexAt(7, new Point2d(ValuePointSection, ValuePoint.Value.Y - 100), 0, 0, 0);
                            Concrete.AddVertexAt(8, new Point2d(ValuePointSection, ValuePoint.Value.Y), 0, 0, 0);
                            Concrete.Layer = sLayerNameConcrete;
                            acBlkTblRec.AppendEntity(Concrete);
                            acTrans.AddNewlyCreatedDBObject(Concrete, true);
                        }
                        using (Polyline Confinementbar = new Polyline())
                        {
                            Confinementbar.SetDatabaseDefaults();
                            Confinementbar.AddVertexAt(0, new Point2d(ValuePointSection + bof + Const.CoverForConfinementBar, ValuePoint.Value.Y - Const.CoverForConfinementBar), 0, 0, 0);
                            Confinementbar.AddVertexAt(1, new Point2d(ValuePointSection + bf - bof - Const.CoverForConfinementBar, ValuePoint.Value.Y - Const.CoverForConfinementBar), 0, 0, 0);
                            Confinementbar.AddVertexAt(2, new Point2d(ValuePointSection + bf - bof - Const.CoverForConfinementBar, ValuePoint.Value.Y - item.Height * 1000 + Const.CoverForConfinementBar), 0, 0, 0);
                            Confinementbar.AddVertexAt(3, new Point2d(ValuePointSection + bof + Const.CoverForConfinementBar, ValuePoint.Value.Y - item.Height * 1000 + Const.CoverForConfinementBar), 0, 0, 0);
                            Confinementbar.AddVertexAt(4, new Point2d(ValuePointSection + bof + Const.CoverForConfinementBar, ValuePoint.Value.Y - Const.CoverForConfinementBar), 0, 0, 0);
                            Confinementbar.Layer = sLayerNameConfinementBar;
                            acBlkTblRec.AppendEntity(Confinementbar);
                            acTrans.AddNewlyCreatedDBObject(Confinementbar, true);
                        }
                        using (RotatedDimension DimHeight = new RotatedDimension())
                        {
                            DimHeight.SetDatabaseDefaults();
                            DimHeight.XLine1Point = new Point3d(ValuePointSection, ValuePoint.Value.Y, 0);
                            DimHeight.XLine2Point = new Point3d(ValuePointSection, ValuePoint.Value.Y - item.Height*1000, 0);
                            DimHeight.DimLinePoint = new Point3d(ValuePointSection - 50, 0, 0);
                            DimHeight.DimensionStyle = acCurDb.Dimstyle;
                            acBlkTblRec.AppendEntity(DimHeight);
                            acTrans.AddNewlyCreatedDBObject(DimHeight, true);
                        }
                        using (RotatedDimension DimWidth = new RotatedDimension())
                        {
                            DimWidth.SetDatabaseDefaults();
                            DimWidth.XLine1Point = new Point3d(ValuePointSection + bof, ValuePoint.Value.Y - item.Height*1000, 0);
                            DimWidth.XLine2Point = new Point3d(ValuePointSection + bf - bof, ValuePoint.Value.Y - item.Height * 1000, 0);
                            DimWidth.DimLinePoint = new Point3d(0, -50, 0);
                            DimWidth.DimensionStyle = acCurDb.Dimstyle;
                            acBlkTblRec.AppendEntity(DimWidth);
                            acTrans.AddNewlyCreatedDBObject(DimWidth, true);
                        }
                        using (DBText NameSection = new DBText())
                        {
                            NameSection.SetDatabaseDefaults();
                            NameSection.Height = 70;
                            NameSection.Position = new Point3d(ValuePointSection + bf / 2 - NameSection.Height, ValuePoint.Value.Y - item.Height * 1000 - 100, 0);
                            NameSection.TextString = item.Section;
                            NameSection.Layer = sLayerNameTextNameSection;
                            acBlkTblRec.AppendEntity(NameSection);
                            acTrans.AddNewlyCreatedDBObject(NameSection, true);
                        }    
                        double RadiusTopSection = CommandModel.SectionsBeam.Where(x => x.FrameName == item.FrameName && x.Section == item.Section).Max(x => x.D1) / 2;
                        double spacingrebartopsection = (item.Width * 1000 - 2 * Const.CoverForConfinementBar - item.N1 * 2 * RadiusTopSection - item.N2*2*RadiusTopSection) / (item.N1 + item.N2 - 1);
                        for (int i = 0; i < item.N1 + item.N2; i++)
                        {
                            Circle acCirctop = new Circle();
                            acCirctop.SetDatabaseDefaults();
                            acCirctop.Center = new Point3d(ValuePointSection + bof + Const.CoverForConfinementBar + RadiusTopSection + i*spacingrebartopsection + i*RadiusTopSection*2, ValuePoint.Value.Y - Const.CoverForConfinementBar - RadiusTopSection, 0);
                            acCirctop.Radius = RadiusTopSection;
                            acCirctop.Layer = sLayerNameLongitudinalBar;
                            acBlkTblRec.AppendEntity(acCirctop);
                            acTrans.AddNewlyCreatedDBObject(acCirctop, true);
                            ObjectIdCollection acObjIdColl = new ObjectIdCollection();
                            acObjIdColl.Add(acCirctop.ObjectId);
                            Hatch acHatch = new Hatch();
                            acBlkTblRec.AppendEntity(acHatch);
                            acTrans.AddNewlyCreatedDBObject(acHatch, true);
                            acHatch.SetDatabaseDefaults();
                            acHatch.SetHatchPattern(HatchPatternType.PreDefined, "ANSI31");
                            acHatch.Associative = true;
                            acHatch.AppendLoop(HatchLoopTypes.Outermost, acObjIdColl);
                            acHatch.EvaluateHatch(true);
                            acHatch.PatternScale = 0.000001;
                            acHatch.Layer = sLayerNameConfinementBar;
                            acHatch.SetHatchPattern(acHatch.PatternType, acHatch.PatternName);
                            acHatch.EvaluateHatch(true);
                        }
                        double spacingrebarbottomsection = (item.Width * 1000 - 2 * Const.CoverForConfinementBar - item.N3 * 2 * RadiusTopSection - item.N4 * 2 * RadiusTopSection) / (item.N3 + item.N4 - 1);
                        for (int i = 0; i < item.N3 + item.N4; i++)
                        {
                            Circle acCirctop = new Circle();
                            acCirctop.SetDatabaseDefaults();
                            acCirctop.Center = new Point3d(ValuePointSection + bof + Const.CoverForConfinementBar + RadiusTopSection + i * spacingrebarbottomsection + i * RadiusTopSection * 2, ValuePoint.Value.Y - item.Height*1000 + Const.CoverForConfinementBar+ RadiusTopSection, 0);
                            acCirctop.Radius = RadiusTopSection;
                            acCirctop.Layer = sLayerNameLongitudinalBar;
                            acBlkTblRec.AppendEntity(acCirctop);
                            acTrans.AddNewlyCreatedDBObject(acCirctop, true);
                            ObjectIdCollection acObjIdColl = new ObjectIdCollection();
                            acObjIdColl.Add(acCirctop.ObjectId);
                            Hatch acHatch = new Hatch();
                            acBlkTblRec.AppendEntity(acHatch);
                            acTrans.AddNewlyCreatedDBObject(acHatch, true);
                            acHatch.SetDatabaseDefaults();
                            acHatch.SetHatchPattern(HatchPatternType.PreDefined, "ANSI31");
                            acHatch.Associative = true;
                            acHatch.AppendLoop(HatchLoopTypes.Outermost, acObjIdColl);
                            acHatch.EvaluateHatch(true);
                            acHatch.PatternScale = 0.000001;
                            acHatch.Layer = sLayerNameConfinementBar;
                            acHatch.SetHatchPattern(acHatch.PatternType, acHatch.PatternName);
                            acHatch.EvaluateHatch(true);
                        }
                        ValuePointSection = ValuePointSection + bf + 100;
                    }
                }
                acTrans.Commit();
            }
        }
        [CommandMethod("MyGroup", "MyCommand", "MyCommandLocal", CommandFlags.Modal)]
        public void MyCommand() // This method can have any name
        {
            // Put your command code here
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;
            if (doc != null)
            {
                ed = doc.Editor;
                ed.WriteMessage("Hello, this is your first command.");

            }
        }

        // Modal Command with pickfirst selection
        [CommandMethod("MyGroup", "MyPickFirst", "MyPickFirstLocal", CommandFlags.Modal | CommandFlags.UsePickSet)]
        public void MyPickFirst() // This method can have any name
        {
            PromptSelectionResult result = Application.DocumentManager.MdiActiveDocument.Editor.GetSelection();
            if (result.Status == PromptStatus.OK)
            {
                // There are selected entities
                // Put your command using pickfirst set code here
            }
            else
            {
                // There are no selected entities
                // Put your command code here
            }
        }

       

    }

}
