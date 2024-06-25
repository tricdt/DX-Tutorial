using System;
using System.ComponentModel;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using GridDemo;
using GridDemo.Controls;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.Generic;
using DevExpress.Xpf.Core.ServerMode;

namespace EditorsDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InstantFeedbackModeViewModelBase.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/LookUpEditServerMode/LookUpInstantFeedbackModeViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/LookUpEditServerMode/PersonsDataContext.(cs)")]
    public partial class EditorsAsyncServerMode : EditorsDemoModule {
        public EditorsAsyncServerMode() {
            InitializeComponent();
        }
    }
}
