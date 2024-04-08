using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTOREBAR.Model
{
    public class SectionColumn
    {
        private string framename;
        public string FrameName
        {
            get { return framename; }
            set { framename = value; }
        }
        private string section;
        public string Section
        {
            get { return section; }
            set { section = value; }
        }
        private double height;
        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        private double width;
        public double Width
        {
            get { return width; }
            set { width = value; }
        }
        private double lenght;
        public double Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }
        private double mutop;
        public double MuTop
        {
            get { return mutop; }
            set { mutop = value; }
        }
        private double mubottom;
        public double MuBottom
        {
            get { return mubottom; }
            set { mubottom = value; }
        }
        private int steelquantityTopST1;
        public int SteelQuantityTopST1
        {
            get { return steelquantityTopST1; }
            set { steelquantityTopST1 = value; }
        }
        private int steelquantityTopST2;
        public int SteelQuantityTopST2
        {
            get { return steelquantityTopST2; }
            set { steelquantityTopST2 = value; }
        }
        private int steelquantityBottomST1;
        public int SteelQuantityBottomST1
        {
            get { return steelquantityBottomST1; }
            set { steelquantityBottomST1 = value; }
        }
        private int steelquantityBottomST2;
        public int SteelQuantityBottomST2
        {
            get { return steelquantityBottomST2; }
            set { steelquantityBottomST2 = value; }
        }
        private int confinementbar;
        public int Confinementbar
        {
            get { return confinementbar; }
            set { confinementbar = value; }
        }
        private int insidelongitudinarbarTopST;
        public int InsideLongitudinalBarTopST
        {
            get { return insidelongitudinarbarTopST; }
            set { insidelongitudinarbarTopST = value; }
        }
        private int outsidelongitudinarbarTopST;
        public int OutsideLongitudinalBarTopST
        {
            get { return outsidelongitudinarbarTopST; }
            set { outsidelongitudinarbarTopST = value; }
        }
        private int insidelongitudinarbarBottomST;
        public int InsideLongitudinalBarBottomST
        {
            get { return insidelongitudinarbarBottomST; }
            set { insidelongitudinarbarBottomST = value; }
        }
        private int outsidelongitudinarbarBottomST;
        public int OutsideLongitudinalBarBottomST
        {
            get { return outsidelongitudinarbarBottomST; }
            set { outsidelongitudinarbarBottomST = value; }
        }
        private double spacingconfinementbar;
        public double Spacingconfinementbar
        {
            get { return spacingconfinementbar; }
            set { spacingconfinementbar = value; }
        }
        private string top;
        public string Top
        {
            get { return top; }
            set { top = value; }
        }
        private string bottom;
        public string Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }
        private double mtop;
        public double Mtop
        {
            get { return mtop; }
            set
            {
                mtop = value;
            }
        }
        private double mbottom;
        public double Mbottom
        {
            get { return mbottom; }
            set { mbottom = value; }
        }
    }
}
