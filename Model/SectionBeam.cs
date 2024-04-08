using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTOREBAR.Model
{
    public class SectionBeam
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
        public double Mutop
        {
            get { return mutop; }
            set { mutop = value; }
        }
        private double mubottom;
        public double Mubottom
        {
            get { return mubottom; }
            set { mubottom = value; }
        }
        private double mtop;
        public double Mtop
        {
            get { return mtop; }
            set { mtop = value; }
        }
        private double mbottom;
        public double Mbottom
        {
            get { return mbottom; }
            set { mbottom = value; }
        }
        private int confinementbar;
        public int Confinementbar
        {
            get { return confinementbar; }
            set { confinementbar = value; }
        }
        private double spacingconfinementbar;
        public double Spacingconfinementbar
        {
            get { return spacingconfinementbar; }
            set { spacingconfinementbar = value; }
        }
        private int n1;
        public int N1
        {
            get { return n1; }
            set { n1 = value; }
        }
        private int n2;
        public int N2
        {
            get { return n2; }
            set { n2 = value; }
        }
        private int n3;
        public int N3
        {
            get { return n3; }
            set { n3 = value; }
        }
        private int n4;
        public int N4
        {
            get { return n4; }
            set { n4 = value; }
        }
        private int d1;
        public int D1
        {
            get { return d1; }
            set { d1 = value; }
        }
        private int d2;
        public int D2
        {
            get { return d2; }
            set { d2 = value; }
        }
        private int d3;
        public int D3
        {
            get { return d3; }
            set { d3 = value; }
        }
        private int d4;
        public int D4
        {
            get { return d4; }
            set { d4 = value; }
        }
        public string Top
        {
            get
            {
                if(n2 ==0)
                {
                    return n1 + "d" + d1;
                }
                else
                {
                    return n1 + "d" + d1 +"+"+n2+"d"+d2;
                }
            }
        }
        public string Bottom
        {
            get
            {
                if (n4 == 0)
                {
                    return n3 + "d" + d3;
                }
                else
                {
                    return n3 + "d" + d3 + "+" + n4 + "d" + d4;
                }
            }
        }
    }
}
