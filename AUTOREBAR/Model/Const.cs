using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ETABSv1;

namespace AUTOREBAR.Model
{
    public class Const
    {
        public static bool isOpenETABS = false;
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
