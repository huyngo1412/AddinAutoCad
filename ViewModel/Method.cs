using AUTOREBAR.Model;
using AUTOREBAR.ViewModel;
using ETABSv1;
using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup.Localizer;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace AUTOREBAR.ViewModel
{
        public class Method
        {
            public static List<SectionBeam> beams = new List<SectionBeam>();
            public static void SectionA(string FrameName,string Section, double Width, double Height,double Length, double Mtop,double Mbottom,double Q,double Q1)
            {
                Const.n1 = 0;Const.d1 = 0;Const.n2 = 0;Const.d2 = 0; Const.n3 = 0; Const.d3 = 0; Const.n4 = 0; Const.d4 = 0;
                if (Math.Abs(Mtop) < Momen(Width, Height, 2 * Math.PI * Math.Pow(CommandModel.Rebar[3], 2) / 4, Const.CoverForConfinementBar - CommandModel.Rebar[3] / 2,Width))
                {
                    Const.n1 = 2;
                    Const.d1 = CommandModel.Rebar[3];
                    Const.Mtop = Mtop;
                    Const.Mutop = Momen(Width, Height, 2 * Math.PI * Math.Pow(CommandModel.Rebar[3], 2) / 4, Const.CoverForConfinementBar - CommandModel.Rebar[3] / 2,Width);
                    SectionC(FrameName, Section, Width, Height, Length, Mtop, Mbottom, Q, Q1);
                    return;
                }
                else
                {
                    for (int i = 3; i < CommandModel.Rebar.Length; i++)
                    {
                        for (int j = 2; j <= 5; j++)
                        {
                            double S = j * Math.PI * Math.Pow(CommandModel.Rebar[i], 2) / 4;
                            double a = Const.CoverForConfinementBar + CommandModel.Rebar[i] / 2;
                            double t = (Width * 1000 - Const.CoverForConfinementBar * 2 - CommandModel.Rebar[i] * j) / (j - 1);
                            Const.Mutop = Momen(Width, Height, S, a,Width);
                            if (t > 25 && Const.Mutop - Math.Abs(Mtop) < 0.25 * Math.Abs(Mtop) && Const.Mutop > Math.Abs(Mtop))
                            {
                                Const.n1 = j;
                                Const.d1 = CommandModel.Rebar[i];
                                Const.Mtop = Mtop;
                                SectionC(FrameName, Section, Width, Height, Length, Mtop, Mbottom, Q, Q1);
                            }
                            if (j == 2 && Const.Mutop < Math.Abs(Mtop))
                            {
                                SectionB(FrameName, Section, Width, Height, Length, CommandModel.Rebar[i - 1], CommandModel.Rebar[i], Mtop, Mbottom, Q, Q1);
                            }
                        }
                    }
                }
            }
            private static void SectionB(string FrameName,string Section,double Width,double Height,double Length,int InSideRebar,int OutSideRebar,double Mtop,double Mbottom,double Q,double Q1)
            {
                for (int i = 1; i < 5; i++)
                {
                    double S1 = 2 * Math.PI * Math.Pow(OutSideRebar, 2) / 4;
                    double S2 = i * Math.PI * Math.Pow(InSideRebar, 2) / 4;
                    double a = (S1 * (Const.CoverForConfinementBar + OutSideRebar / 2) + S2 * (Const.CoverForConfinementBar + InSideRebar / 2)) / (S1+S2);
                    double t = (Width * 1000 - 2 * Const.CoverForConfinementBar - 2 * OutSideRebar - i * InSideRebar) / (i + 1);
                    Const.Mutop = Momen(Width, Height, S1 + S2, a,Width);
                    if(Const.Mutop - Math.Abs(Mtop) < 0.25 * Math.Abs(Mtop) && t >25 && Const.Mutop > Math.Abs(Mtop))
                    {
                        Const.n1 = 2;
                        Const.d1 = OutSideRebar;
                        Const.n2 = i;
                        Const.d2 = InSideRebar;
                        Const.Mtop = Mtop;
                        SectionC(FrameName, Section, Width, Height, Length, Mtop, Mbottom, Q, Q1);
                    }
                }
            }
        private static void SectionC(string FrameName, string Section, double Width, double Height, double Length, double Mtop, double Mbottom, double Q, double Q1)
        {
            double bf = Width + ((2*Length) / 6);
            double S = 2 * Math.PI * Math.Pow(CommandModel.Rebar[3], 2) / 4;
            double a = Const.CoverForConfinementBar + (CommandModel.Rebar[3] / 2);
            double t = Width * 1000 - 2 * Const.CoverForConfinementBar - 2 * CommandModel.Rebar[3];
            if (Const.FyLong * (S / 1E6) < Const.Fc * bf * (Height - (a / 1000)))
            {
                Const.Mubottom = Momen(bf, Height, S, a,Width);
                if (Const.Mubottom > Math.Abs(Mbottom))
                {
                    Const.n3 = 2;
                    Const.d3 = CommandModel.Rebar[3];
                    Const.Mbottom = Mbottom;
                    Const.SpacingConfinementBar = SpacingConfinementBar(FrameName, Q, Width, Height, Q1, a/1000, Const.ConfinementBarDiameter);
                    ReturnFrame(FrameName, Section, Height, Width, Length, Const.n1, Const.d1, Const.n2, Const.d2, Const.n3, Const.d3, Const.n4, Const.d4, Const.Mtop, Const.Mbottom, Const.Mutop, Const.Mubottom, Const.SpacingConfinementBar);
                    return;
                }
            }
            else
            {
                Const.Mubottom = MomenF(Width, bf, Height, S, a);
                if (Const.Mubottom > Math.Abs(Mbottom))
                {
                    Const.n3 = 2;
                    Const.d3 = CommandModel.Rebar[3];
                    Const.Mbottom = Mbottom;
                    Const.SpacingConfinementBar = SpacingConfinementBar(FrameName, Q, Width, Height, Q1, a / 1000, Const.ConfinementBarDiameter);
                    ReturnFrame(FrameName, Section, Height, Width, Length, Const.n1, Const.d1, Const.n2, Const.d2, Const.n3, Const.d3, Const.n4, Const.d4, Const.Mtop, Const.Mbottom, Const.Mutop, Const.Mubottom, Const.SpacingConfinementBar);
                    return;
                }
            }
            for (int i = 3; i < CommandModel.Rebar.Length; i++)
            {
                for (int j = 2; j <= 5; j++)
                {
                    S = j * Math.PI * Math.Pow(CommandModel.Rebar[i], 2)/4;
                    a = Const.CoverForConfinementBar + (CommandModel.Rebar[i] / 2);
                    t = (Width*1000 - 2 * Const.CoverForConfinementBar - j * CommandModel.Rebar[i]) / (j - 1);

                    if (Const.FyLong * (S / 1E6) < Const.Fc * bf * (Height - (a / 1000)))
                    {
                        Const.Mubottom = Momen(bf, Height, S, a,Width);
                        if (t > 25 && i == 3 && j == 2 && Const.Mubottom > Math.Abs(Mbottom) && Const.Mubottom - Math.Abs(Mbottom) < 0.25 * Mbottom)
                        {
                            Const.n3 = j;
                            Const.d3 = CommandModel.Rebar[i];
                            Const.Mbottom = Mbottom;
                            Const.SpacingConfinementBar = SpacingConfinementBar(FrameName, Q, Width, Height, Q1, a / 1000, Const.ConfinementBarDiameter);
                            ReturnFrame(FrameName, Section, Height, Width, Length, Const.n1, Const.d1, Const.n2, Const.d2, Const.n3, Const.d3, Const.n4, Const.d4, Const.Mtop, Const.Mbottom, Const.Mutop, Const.Mubottom, Const.SpacingConfinementBar);
                        }
                        if (t > 25 && Const.Mubottom > Math.Abs(Mbottom) && Const.Mubottom - Math.Abs(Mbottom) < 0.25 * Mbottom)
                        {
                            Const.n3 = j;
                            Const.d3 = CommandModel.Rebar[i];
                            Const.Mbottom = Mbottom;
                            Const.SpacingConfinementBar = SpacingConfinementBar(FrameName, Q, Width, Height, Q1, a / 1000, Const.ConfinementBarDiameter);
                            ReturnFrame(FrameName, Section, Height, Width, Length, Const.n1, Const.d1, Const.n2, Const.d2, Const.n3, Const.d3, Const.n4, Const.d4, Const.Mtop, Const.Mbottom, Const.Mutop, Const.Mubottom, Const.SpacingConfinementBar);
                        }

                        if (Const.M < Math.Abs(Mbottom)&&j==2)
                        {
                            SectionD(FrameName,Section,Width,Height,Length,CommandModel.Rebar[i-1],CommandModel.Rebar[i],Mtop,Mbottom,Q,Q1);
                        }    
                    }
                    else
                    {
                        Const.Mubottom = MomenF(Width,bf,Height,S,a);
                        if (t > 25 && i == 3 && j == 2 && Const.Mubottom > Math.Abs(Mbottom) && Const.Mubottom - Math.Abs(Mbottom) < 0.25 * Mbottom)
                        {
                            Const.n3 = j;
                            Const.d3 = CommandModel.Rebar[i];
                            Const.Mbottom = Mbottom;
                            Const.SpacingConfinementBar = SpacingConfinementBar(FrameName, Q, Width, Height, Q1, a / 1000, Const.ConfinementBarDiameter);
                            ReturnFrame(FrameName, Section, Height, Width, Length, Const.n1, Const.d1, Const.n2, Const.d2, Const.n3, Const.d3, Const.n4, Const.d4, Const.Mtop, Const.Mbottom, Const.Mutop, Const.Mubottom, Const.SpacingConfinementBar);
                        }
                        Const.M = MomenF(Width, bf, Height, S, a);
                        if (t > 25 && Const.Mubottom > Math.Abs(Mbottom) && Const.Mubottom - Math.Abs(Mbottom) < 0.25 * Mbottom)
                        {
                            Const.n3 = j;
                            Const.d3 = CommandModel.Rebar[i];
                            Const.Mbottom = Mbottom;
                            Const.SpacingConfinementBar = SpacingConfinementBar(FrameName, Q, Width, Height, Q1, a / 1000, Const.ConfinementBarDiameter);
                            ReturnFrame(FrameName, Section, Height, Width, Length, Const.n1, Const.d1, Const.n2, Const.d2, Const.n3, Const.d3, Const.n4, Const.d4, Const.Mtop, Const.Mbottom, Const.Mutop, Const.Mubottom, Const.SpacingConfinementBar);
                        }

                        if (Const.M < Math.Abs(Mbottom) && j == 2)
                        {
                            SectionD(FrameName,Section,Width,Height,Length,CommandModel.Rebar[i-1],CommandModel.Rebar[i-1],Mtop,Mbottom,Q,Q1);
                        }
                    }
                }
            }
        }
        private static void SectionD(string FrameName, string Section, double Width, double Height, double Length, int InSideRebar, int OutSideRebar, double Mtop, double Mbottom, double Q, double Q1)
        {
            double bf = Width + ((2 * Length) / 6);
            for (int i = 1; i <= 5; i++)
            {
                double S1 = 2 * Math.PI * Math.Pow(OutSideRebar, 2) / 4;
                double S2 = i * Math.PI * Math.Pow(InSideRebar, 2) / 4;
                double a = ((Const.CoverForConfinementBar + OutSideRebar / 2) * S1 + (Const.CoverForConfinementBar + InSideRebar / 2) * S2) / (S1 + S2);
                double t = (Width*1000 - 2 * Const.CoverForConfinementBar - 2 * OutSideRebar - i * InSideRebar) / (i + 1);
                if (Const.FyLong * ((S1+S2) / 1E6) < Const.Fc * bf * (Height - (a / 1000)))
                {
                    Const.Mubottom = Momen(bf, Height, S1 + S2, a,Width);
                    if(t > 25 && Const.Mubottom > Math.Abs(Mbottom) && Const.Mubottom - Math.Abs(Mbottom) < 0.25 * Mbottom)
                    {
                        Const.n3 = 2;
                        Const.d3 = OutSideRebar;
                        Const.n4 = i;
                        Const.d4 = InSideRebar;
                        Const.Mbottom = Mbottom;
                        Const.SpacingConfinementBar = SpacingConfinementBar(FrameName, Q, Width, Height, Q1, a / 1000, Const.ConfinementBarDiameter);
                        ReturnFrame(FrameName, Section, Height, Width, Length, Const.n1, Const.d1, Const.n2, Const.d2, Const.n3, Const.d3, Const.n4, Const.d4, Const.Mtop, Const.Mbottom, Const.Mutop, Const.Mubottom, Const.SpacingConfinementBar);
                    }
                }
                else
                {
                    Const.Mubottom= MomenF(Width,bf,Height,S1+S2,a);
                    if(t > 25 && Const.Mubottom > Math.Abs(Mbottom) && Const.Mubottom - Math.Abs(Mbottom) < 0.25 * Mbottom)
                    {
                        Const.n3 = 2;
                        Const.d3 = OutSideRebar;
                        Const.n4 = i;
                        Const.d4 = InSideRebar;
                        Const.Mbottom = Mbottom;
                        Const.SpacingConfinementBar = SpacingConfinementBar(FrameName, Q, Width, Height, Q1, a / 1000, Const.ConfinementBarDiameter);
                        ReturnFrame(FrameName, Section, Height, Width, Length, Const.n1, Const.d1, Const.n2, Const.d2, Const.n3, Const.d3, Const.n4, Const.d4, Const.Mtop, Const.Mbottom, Const.Mutop, Const.Mubottom, Const.SpacingConfinementBar);
                    }
                }
            }
        }
        private static void ReturnFrame(string FrameName,string Section,double Height,double Width,double Lenght,int N1,int D1,int N2,int D2,int N3,int D3,int N4,int D4,double Mtop,double Mbottom,double Mutop,double Mubottom,double SpacingConfinementBar)
        {
            SectionBeam section = new SectionBeam()
            {
                FrameName = FrameName,
                Section = Section,
                Width = Width,
                Height = Height,
                Lenght = Lenght,
                N1 = N1,
                D1 = D1,
                N2 = N2,
                D2 = D2,
                N3 = N3,
                D3 = D3,
                N4 = N4,
                D4 = D4,
                Mtop = Mtop,
                Mbottom = Mbottom,
                Mutop = Mutop,
                Mubottom = Mubottom,
                Spacingconfinementbar = SpacingConfinementBar,
                Confinementbar = Const.ConfinementBarDiameter
            };
            beams.Add(section);
        }
        private static double Momen(double Widthf, double Height, double S, double a,double Width)
        {
            double h = Height - (a / 1000);
            double E = (Const.FyLong * (S / 1E6)) / (Const.Fc * Widthf * h);
            double alpha = E * (1 - 0.5 * E);
            double muy = (S / (Width * h * (1E6))) * 100;
            double mu = 0;
            if (muy > 0.15 && muy < 5)
            {
                mu = alpha * Const.Fc * Width * Math.Pow(h, 2);
            }
            return mu;
        }
        private static double MomenF(double Width, double Widthf, double Height, double S, double a)
        {
            double h = Height - (a / 1000);
            double E = (Const.FyLong * (S / 1E6) - Const.Fc * (Widthf - Width) * 0.1) / (Const.Fc * Width * h);
            double alpha = E * (1 - 0.5 * E);
            double mu = 0;
            double muy = (S / (Width * h * (1E6))) * 100;
            if (muy > 0.15 && muy < 5)
            {
                mu = alpha * Const.Fc * Width * Math.Pow(h, 2) + Const.Fc * (Widthf - Width) * 0.1 * (h - 0.5 * h);
            }
            return mu;
        }
        private static double RoudingNumbers(double number)
        {
            double RoudedNumber = Math.Floor(number / 10) * 10;
            return RoudedNumber;
        }
        public static void ReturnSection(string FrameName,string Section,double Width,double Height,double Length)
        {
            double maxmutop = beams.OrderByDescending(x => x.Mutop).Where(x => x.FrameName == FrameName && x.Section == Section).Select(x => x.Mutop).FirstOrDefault();
            double maxmubottom = beams.OrderByDescending(x => x.Mubottom).Where(x => x.FrameName == FrameName && x.Section == Section).Select(x => x.Mubottom).FirstOrDefault();
            beams.RemoveAll(x=>x.FrameName == FrameName && x.Section == Section && x.Mubottom < maxmubottom && x.Mutop < maxmutop);
            int n1 = beams.Where(x => x.FrameName == FrameName && x.Section == Section && x.Mtop == maxmutop).Select(x=>x.N1).FirstOrDefault();
            int d1 = beams.Where(x => x.FrameName == FrameName && x.Section == Section && x.Mtop == maxmutop).Select(x => x.D1).FirstOrDefault();
            int n2 = beams.Where(x => x.FrameName == FrameName && x.Section == Section && x.Mtop == maxmutop).Select(x => x.N2).FirstOrDefault();
            int d2 = beams.Where(x => x.FrameName == FrameName && x.Section == Section && x.Mtop == maxmutop).Select(x => x.D2).FirstOrDefault();
            List<SectionBeam> beam = beams.Where(x => x.FrameName == FrameName).ToList();
            foreach (var item in beam)
            {
                if(item.Section != Section)
                {
                    var updatesection = beams.Select(x => 
                    {
                        if(x.Section == item.Section && item.FrameName == FrameName)
                        {
                            if(n1>2)
                            {
                                for (int i = n1; i >= 2; i--)
                                {
                                    Const.M = Momen(Width, Height, i * Math.PI * Math.Pow(d1, 2) / 4, Const.CoverForConfinementBar - d1 / 2,Width);
                                    if (Const.M >= x.Mtop)
                                    {
                                        x.N1 = i;
                                        x.D1 = d1;
                                        x.Mutop = Const.M;
                                    }
                                }
                            }
                            if (n1 == 2 && n2 == 0)
                            {
                                Const.M = Momen(Width, Height, 2 * Math.PI * Math.Pow(d1, 2) / 4, Const.CoverForConfinementBar -  d1/ 2,Width);
                                if (Const.M >= x.Mtop)
                                {
                                    x.N1 = 2;
                                    x.D1 = d1;
                                    x.Mutop = Const.M;
                                }
                            }
                            if (n1 == 2 && n2 > 0)
                            {
                                for (int i = n2; i >= 0; i--)
                                {
                                    double S1 = 2 * Math.PI * Math.Pow(d1, 2) / 4;
                                    double S2 = i * Math.PI * Math.Pow(d2, 2) / 4;
                                    double a = (S1 * (Const.CoverForConfinementBar + d1 / 2) + S2 * (Const.CoverForConfinementBar + d2 / 2)) / (S1 + S2);
                                    Const.M = Momen(Width, Height, S1 + S2, a,Width);
                                    if (Const.M >= x.Mtop)
                                    {
                                        if (i == 0)
                                        {
                                            x.D1 = d1;
                                            x.N1 = 2;
                                            x.N2 = 0;
                                            x.D2 = 0;
                                            x.Mutop = Const.M;
                                        }
                                        else
                                        {
                                            x.D1 = d1;
                                            x.N1 = 2;
                                            x.N2 = i;
                                            x.D2 = d2;
                                            x.Mutop = Const.M;
                                        }
                                    }
                                }
                            }
                        }
                        return x; }).ToList();
                }
            }
        }
        private static void ReturnTopSection(List<SectionBeam> beam, string section1, double Width, double Height, double MaxMutop)
        {
            
        }
        private static void ReturnBottomSection(List<SectionBeam> beam, string section2, double Width, double Height, double Lenght, double MaxMuBottom)
        {
            int n3 = beam.Where(x => x.Section == section2 && x.Mubottom == MaxMuBottom).Select(x => x.N3).FirstOrDefault();
            int rebar3 = beam.Where(x => x.Section == section2 && x.Mubottom == MaxMuBottom).Select(x => x.D3).FirstOrDefault();
            int n4 = beam.Where(x => x.Section == section2 && x.Mubottom == MaxMuBottom).Select(x => x.N4).FirstOrDefault();
            int rebar4 = beam.Where(x => x.Section == section2 && x.Mubottom == MaxMuBottom).Select(x => x.D4).FirstOrDefault();
            double a;
            double bf = Width + (2 * Lenght / 6);
            foreach (var item in beam)
            {
                if (item.Section != section2)
                {
                    var updatesection = beams.Select(x =>
                    {
                        if (x.FrameName == item.FrameName && x.Section == item.Section)
                        {
                            if (n3 > 2)
                            {
                                for (int i = n3; i >= 2; i--)
                                {
                                    a = Const.CoverForConfinementBar - rebar3 / 2;
                                    if (Math.Abs(x.Mbottom) < Const.Fc * bf * 0.1 * ((Height - a) - 0.5 * 0.1))
                                    {
                                        Const.M = Momen(bf, Height, i * Math.PI * Math.Pow(rebar3, 2) / 4, a,Width);
                                        if (Const.M >= Math.Abs(x.Mbottom))
                                        {
                                            x.N3 = i;
                                            x.D3 = rebar3;
                                            x.Mubottom = Const.M;
                                        }
                                    }
                                    else
                                    {
                                        Const.M = MomenF(Width, bf, Height, i * Math.PI * Math.Pow(rebar3, 2) / 4,a);
                                        if (Const.M >= Math.Abs(x.Mbottom))
                                        {
                                            x.N3 = i;
                                            x.D3 = rebar3;
                                            x.Mubottom = Const.M;
                                        }
                                    }
                                }
                            }
                            if (n3 == 2 && n4 == 0)
                            {
                                a = Const.CoverForConfinementBar - rebar3 / 2;
                                if (Math.Abs(x.Mbottom) < Const.Fc * bf * 0.1 * ((Height - a) - 0.5 * 0.1))
                                {
                                    Const.M = Momen(bf, Height, 2 * Math.PI * Math.Pow(rebar3, 2) / 4, a,Width);
                                    if (Const.M >= Math.Abs(x.Mbottom))
                                    {
                                        x.N3= 2;
                                        x.D3 = rebar3;
                                        x.Mubottom = Const.M;
                                    }
                                }
                                else
                                {
                                    Const.M = MomenF(Width, bf, Height, 2 * Math.PI * Math.Pow(rebar3, 2) / 4, a);
                                    if (Const.M >= Math.Abs(x.Mbottom))
                                    {
                                        x.N3 = 2;
                                        x.D3 = rebar3;
                                        x.Mubottom = Const.M;
                                    }
                                }
                            }
                            if (n3 == 2 && n4 > 0)
                            {
                                for (int i = n4; i >= 0; i--)
                                {
                                    double S1 = 2 * Math.PI * Math.Pow(rebar3, 2) / 4;
                                    double S2 = i * Math.PI * Math.Pow(rebar4, 2) / 4;
                                    a = (S1 * (Const.CoverForConfinementBar + rebar3 / 2) + S2 * (Const.CoverForConfinementBar + rebar4 / 2)) / (S1 + S2);
                                    if (Math.Abs(x.Mbottom) < Const.Fc * bf * 0.1 * ((Height - a) - 0.5 * 0.1))
                                    {
                                        Const.M = Momen(bf, Height, S1 + S2, a,Width);
                                        if (Const.M >= Math.Abs(x.Mbottom))
                                        {
                                            if (i == 0)
                                            {
                                                x.N3 = 2;
                                                x.D3 = rebar3;
                                                x.Mubottom = Const.M;
                                            }
                                            else
                                            {
                                                x.N3 = 2;
                                                x.D3 = rebar3;
                                                x.N4 = i;
                                                x.D4 = rebar4;
                                                x.Mubottom = Const.M;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Const.M = MomenF(Width, bf, Height, S1 + S2, a);
                                        if (Const.M >= Math.Abs(x.Mbottom))
                                        {
                                            if (i == 0)
                                            {
                                                x.N3 = 2;
                                                x.D3 = rebar3;
                                                x.Mubottom = Const.M;
                                            }
                                            else
                                            {
                                                x.N3 = 2;
                                                x.D3 = rebar3;
                                                x.N4 = i;
                                                x.D4 = rebar4;
                                                x.Mubottom = Const.M;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        return x;
                    }).ToList();
                }
            }
        }
        public static void SectionBeam(string FrameName, string NameSection)
        {

        }
        private static double SpacingConfinementBar(string FrameName, double Q, double Width, double Height, double q1, double a, int ConfinementBarDiameter)
        {
            double Spacing = 0;
            double S = ((Math.PI * Math.Pow(ConfinementBarDiameter, 2)) / 4) / 1E6;
            double h0 = Height - a;
            double sct = 0;
            if (!CompressiveStress(Q, Width, Height, a))
            {
                MessageBox.Show("The beam " + FrameName + "is not capable of withstanding the compressive stress load");
            }
            if (Height <= 0.45)
            {
                sct = Math.Min(0.15, Height / 2) * 1000;
            }
            else
            {
                sct = Math.Min(0.5, Height / 3) * 1000;
            }
            if (!ShearStrength(Q, Width, Height, q1, a))
            {
                double Mb = Math.Min(2 * Const.Rbt * Width * Math.Pow(h0, 2), 3 * Const.Rbt * Width * Math.Pow(h0, 2));
                double Qb1 = 2 * Math.Sqrt(Mb * q1);
                double qsw = 0;
                if (Q <= Qb1 / 0.6)
                {
                    qsw = Math.Min((Math.Pow(Q, 2) - Math.Pow(Qb1, 2)) / (4 * Mb), (Q - Qb1) / (2 * h0));
                }
                if (Q > Qb1 / 0.6 && Q < Qb1 + (Mb / h0))
                {
                    qsw = Math.Min(Math.Pow(Q - Qb1, 2) / Mb, (Q - Qb1) / (2 * h0));
                }
                if (Q > Qb1 + (Mb / h0))
                {
                    qsw = (Q - Qb1) / h0;
                }
                if (Math.Abs(qsw) < (0.6 * Const.Rbt * Width * h0) / 2)
                {
                    qsw = (Q / (2 * h0)) + q1 * (2 / 0.6) - Math.Sqrt(Math.Pow((Q / (2 * h0)) + ((2 / 0.6) * q1), 2) - Math.Pow((Q / (2 * h0)), 2));
                }
                double stt = 1000 * ((Const.Rsw * 2 * S) / Math.Abs(qsw));
                double smax = ((1.5 * Const.Rbt * Width * Math.Pow(h0, 2)) / Q) * 1000;
                double temp1 = Math.Min(stt, smax);
                double temp2 = Math.Min(temp1, sct);
                Spacing = RoudingNumbers(temp2);
            }
            else
            {
                Spacing = RoudingNumbers(sct);
            }
            return Spacing;
        }
        private static bool CompressiveStress(double Q, double Width, double Height, double a)
        {
            double Q1 = 0.3 * Const.Fc * Width * (Height - a);
            if (Q < Q1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool ShearStrength(double Q, double Width, double Height, double q1, double a)
        {
            double Qbmin = 0.6 * Const.Rbt * Width * (Height - a);
            if (Q < Qbmin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static double ReturnRsw(string Rebar)
        {
            double Rsw = 0;
            switch (Rebar)
            {
                case "CB240T":
                    Rsw = 170000;
                    break;
                case "CB300T":
                    Rsw = 210000;
                    break;
                case "CB300V":
                    Rsw = 210000;
                    break;
                case "CB400V":
                    Rsw = 280000;
                    break;
                case "CB500V":
                    Rsw = 300000;
                    break;
                default:
                    break;
            }
            return Rsw;
        }
    }
}

