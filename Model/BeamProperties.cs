using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTOREBAR.Model
{
    public class BeamProperties
    {
        private string FrameName;
        public string frameName { get { return FrameName; } set { FrameName = value; } }
        private string NameLoadCombo;
        public string nameLoadCombo { get { return NameLoadCombo; } set { NameLoadCombo = value; } }
        private double Height;

        public double height { get { return Height; } set { Height = value; } }
        private double Width;
        public double width { get { return Width; } set { Width = value; } }
        private double Station;
        public double station { get { return Station; } set { Station = value; } }
        private string StepType;
        public string stepType { get { return StepType; } set { StepType = value; } }
        private double P;
        public double p { get { return P; } set { P = value; } }

        private double M;
        public double m { get { return M; } set { M = value; } }
        private double Q;
        public double q { get { return Q; } set { Q = value; } }
        private double G;
        public double g { get { return G; } set { G = value; } }
        private double V;
        public double v { get { return V; } set { V = value; } }
    }
}
