﻿#pragma checksum "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6C9B205AA57A2AD768202591BA6A4A291284FBDC6053B44E0C6162BD975F53F2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AUTOREBAR.UserControlsAUTOREBAR;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AUTOREBAR.UserControlsAUTOREBAR {
    
    
    /// <summary>
    /// BeamUserControl
    /// </summary>
    public partial class BeamUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AUTOREBAR.UserControlsAUTOREBAR.BeamUserControl ucBeam;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridBeamUC;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView LsvDataBeam;
        
        #line default
        #line hidden
        
        
        #line 254 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListboxCombo;
        
        #line default
        #line hidden
        
        
        #line 318 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Define;
        
        #line default
        #line hidden
        
        
        #line 366 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridControl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AUTOREBAR;component/usercontrolsautorebar/beamusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ucBeam = ((AUTOREBAR.UserControlsAUTOREBAR.BeamUserControl)(target));
            
            #line 10 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
            this.ucBeam.Loaded += new System.Windows.RoutedEventHandler(this.ucBeam_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GridBeamUC = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.LsvDataBeam = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.ListboxCombo = ((System.Windows.Controls.ListBox)(target));
            
            #line 255 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
            this.ListboxCombo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListboxCombo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 274 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Btn_Define = ((System.Windows.Controls.Button)(target));
            
            #line 319 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
            this.Btn_Define.Click += new System.Windows.RoutedEventHandler(this.Btn_Define_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.GridControl = ((System.Windows.Controls.Grid)(target));
            
            #line 366 "..\..\..\UserControlsAUTOREBAR\BeamUserControl.xaml"
            this.GridControl.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.GridControl_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

