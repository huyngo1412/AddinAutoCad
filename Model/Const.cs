using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ETABSv1;
using System.Windows.Controls;


namespace AUTOREBAR.Model
{
    public class Const
    {
        public static Grid GridMain;
        public static bool isOpenBeamUC = false;
        public static bool isOpenColumnUC = false;
        public static bool isDataBeam = false;
        public static bool isDataColumn = false;
        public static bool isMaterialDataBeam = false;
        public static bool isMaterialDataColumn = false;
        public static int CoverForConfinementBar;
        public static List<string> choosecombobeam = new List<string>();
        public static List<string> choosecombocolumn = new List<string>();
        public static int ComboType = -1;
        public static int NumberResults = 0;
        public static string[] Obj = null;
        public static double[] ObjSta = null;
        public static string[] Elm = null;
        public static double[] ElmSta = null;
        public static string[] LoadCase = null;
        public static string[] StepType = null;
        public static double[] StepNum = null;
        public static double[] P = null;
        public static double[] V2 = null;
        public static double[] V3 = null;
        public static double[] T = null;
        public static double[] M2 = null;
        public static double[] M3 = null;
        public static double g;
        public static double v;
        public static int number = 0;
        public static double T3 = 0;
        public static double T2 = 0;
        public static List<string> nameloadbeam = new List<string>();
        public static List<string> nameloadcolumn = new List<string>();
        public static int Color = 1;
        public static string Notes = null;
        public static string GUID = null;
        public static string Mat = "";
        public static string filename;
        public static string[] NameMaterial = null;
        public static eMatType Material = 0;
        public static eFrameDesignOrientation TypeFrame = eFrameDesignOrientation.Null;
        public static double Fc = 0;
        public static bool IsLightweight = false;
        public static double FcsFactor = 0;
        public static int SSType = 0;
        public static int SSHysType = 0;
        public static double StrainAtFc = 0;
        public static double StrainUltimate = 0;
        public static double FrictionAngle = 0;
        public static double DilatationalAngle = 0;
        public static double Temp = 0;
        public static string[] frame = null;
        public static double FyLong = 0;
        public static double FyCon = 0;
        public static double FuLong = 0;
        public static double FuCon = 0;
        public static double EFy = 0;
        public static double EFu = 0;
        public static double StrainAtHardening = 0;
        public static bool UseCaltransSSDefaults = false;
        public static int NumberItems = 0;
        public static string[] FrameName = null;
        public static string[] LoadPat = null;
        public static int[] MyType = null;
        public static string[] CSys = null;
        public static int[] Dir = null;
        public static double[] RD1 = null;
        public static double[] RD2 = null;
        public static double[] Dist1 = null;
        public static double[] Dist2 = null;
        public static double[] Val1 = null;
        public static double[] Val2 = null;
        public static eItemType ItemType = eItemType.Objects;
        public static eLoadPatternType mytype = eLoadPatternType.Dead;
        public static double Rsw;
        public static int ConfinementBarDiameter;
        public static int Rbt;
        public static bool IsOpen = false;
        public static int n1;
        public static int d1;
        public static int n2;
        public static int d2;
        public static int n3;
        public static int d3;
        public static int n4;
        public static int d4;
        public static double Mtop;
        public static double Mbottom;
        public static double Mutop;
        public static double Mubottom;
        public static double M;
        public enum RbtConcrete
        {
            B10 = 560,
            B15 = 750,
            B20 = 900,
            B25 = 1050,
            B30 = 1150,
            B35 = 1300,
            B40 = 1400,
            B45 = 1500,
            B50 = 1600,
            B55 = 1700,
            B60 = 1800,
            B70 = 1900,
            B80 = 2100,
            B90 = 2150,
            B100 = 2200
        }
        public enum RbConcrete
        {
            B10 = 6000,
            B15 = 7500,
            B20 = 8500,
            B25 = 11500,
            B30 = 14500,
            B35 = 17000,
            B40 = 19500,
            B45 = 22000,
            B50 = 25000,
            B55 = 27500,
            B60 = 30000,
            B70 = 33000,
            B80 = 37000,
            B90 = 41000,
            B100 = 47500
        }
        public enum RsRebar
        {
            CB240T = 210000,
            CB300T = 260000,
            CB300V = 260000,
            CB400V = 350000,
            Cb500V = 430000,
            Cb600V = 520000
        }
        public static double SpacingConfinementBar;
        public static double MaxMu;
        public static Color ConcreateColor;
    }
}
