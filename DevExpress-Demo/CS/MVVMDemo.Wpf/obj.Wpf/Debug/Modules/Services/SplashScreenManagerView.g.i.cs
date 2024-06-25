﻿#pragma checksum "..\..\..\..\Modules\Services\SplashScreenManagerView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E6C85A7233C50AFDFE80902DAF5CA73B9C3B6D8494CFBDECDDE09AD428719C79"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Core;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Mvvm.UI.Interactivity.TypedStyles;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.Mvvm.UI.TypedStyles;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.TypedStyles;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.DataSources.TypedStyles;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.Serialization.TypedStyles;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.Core.ServerMode.TypedStyles;
using DevExpress.Xpf.Core.TypedStyles;
using DevExpress.Xpf.DXBinding;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Data.TypedStyles;
using DevExpress.Xpf.LayoutControl;
using DevExpress.Xpf.LayoutControl.TypedStyles;
using DevExpress.Xpf.TypedStyles;
using MVVMDemo.Services;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.TypedStyles;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Primitives.TypedStyles;
using System.Windows.Controls.TypedStyles;
using System.Windows.Data;
using System.Windows.Data.TypedStyles;
using System.Windows.Documents;
using System.Windows.Documents.TypedStyles;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.TypedStyles;
using System.Windows.Markup;
using System.Windows.Markup.TypedStyles;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Animation.TypedStyles;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Media.TypedStyles;
using System.Windows.Navigation;
using System.Windows.Navigation.TypedStyles;
using System.Windows.Shapes;
using System.Windows.Shapes.TypedStyles;
using System.Windows.Shell;
using System.Windows.TypedStyles;


namespace MVVMDemo.Services {
    
    
    /// <summary>
    /// SplashScreenManagerView
    /// </summary>
    public partial class SplashScreenManagerView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Modules\Services\SplashScreenManagerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Core.SplashScreenManagerService ThemedSplashScreenService;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\Modules\Services\SplashScreenManagerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Core.SplashScreenManagerService FluentSplashScreenService;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\Modules\Services\SplashScreenManagerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Core.SplashScreenManagerService WaitIndicatorSplashScreenService;
        
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
            System.Uri resourceLocater = new System.Uri("/MVVMDemo;component/modules/services/splashscreenmanagerview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Modules\Services\SplashScreenManagerView.xaml"
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
            this.ThemedSplashScreenService = ((DevExpress.Xpf.Core.SplashScreenManagerService)(target));
            return;
            case 2:
            this.FluentSplashScreenService = ((DevExpress.Xpf.Core.SplashScreenManagerService)(target));
            return;
            case 3:
            this.WaitIndicatorSplashScreenService = ((DevExpress.Xpf.Core.SplashScreenManagerService)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

