﻿#pragma checksum "..\..\..\SubTabs\ProcessTab.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "17DDA0A3EBD4D070F51367F1A0F6ACBC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace UASigner.WpfApp.SubTabs {
    
    
    /// <summary>
    /// ProcessTab
    /// </summary>
    public partial class ProcessTab : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\SubTabs\ProcessTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal UASigner.WpfApp.SubTabs.ProcessTab ConosleTab;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\SubTabs\ProcessTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_Console;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\SubTabs\ProcessTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_AddText;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\SubTabs\ProcessTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Start;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\SubTabs\ProcessTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Stop;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplicationTutorial;component/subtabs/processtab.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SubTabs\ProcessTab.xaml"
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
            this.ConosleTab = ((UASigner.WpfApp.SubTabs.ProcessTab)(target));
            return;
            case 2:
            this.textbox_Console = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.bt_AddText = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\SubTabs\ProcessTab.xaml"
            this.bt_AddText.Click += new System.Windows.RoutedEventHandler(this.bt_AddText_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.bt_Start = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\SubTabs\ProcessTab.xaml"
            this.bt_Start.Click += new System.Windows.RoutedEventHandler(this.bt_Start_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bt_Stop = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\SubTabs\ProcessTab.xaml"
            this.bt_Stop.Click += new System.Windows.RoutedEventHandler(this.bt_Stop_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

