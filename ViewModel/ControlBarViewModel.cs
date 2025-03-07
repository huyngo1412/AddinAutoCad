﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AUTOREBAR.ViewModel;
namespace AUTOREBAR.ViewModel
{
    public class ControlBarViewModel : BaseCommandUC
    {
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        public ControlBarViewModel()
        {
            CloseWindowCommand = new BaseCommand<UserControl>((p)=> { return p == null ? false : true; },(p)=> 
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if(w!=null)
                {
                    w.Close();
                }    
            });
            MouseMoveWindowCommand = new BaseCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            });
        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while(parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
                 
            }
            return parent;
        }
    }
}
