using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;
using System.Collections;
using DevExpress.Xpf.Core;
using DevExpress.Utils;
using System.Data;
using DevExpress.Xpf.Editors;
using DevExpress.DemoData;
using System.Collections.Generic;
using DevExpress.DemoData.Models;
using EditorsDemo;
using DevExpress.Data.Filtering;
using System.Windows.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Core.Native;

namespace EditorsDemo {
    
    
    
    [CodeFile("ModuleResources/LookUpEditTemplates.xaml")]
    [CodeFile("ViewModels/LookUpEditorDemoViewModel.cs")]
    public partial class LookUpEditModule : EditorsDemoModule {

        public LookUpEditModule() {
            DataContext = ViewModel = new LookUpEditorDemoViewModel();
            Keyboard.AddGotKeyboardFocusHandler(this, new KeyboardFocusChangedEventHandler(OnKeyBoardFocusChanged));
            InitializeComponent();
        }

        LookUpEditorDemoViewModel ViewModel { get; set; }

        void OnKeyBoardFocusChanged(object sender, KeyboardFocusChangedEventArgs e) {
            var focused = e.NewFocus as DependencyObject;
            ViewModel.FocusedEditor = focused is LookUpEdit ? focused as LookUpEdit : LayoutHelper.FindParentObject<LookUpEdit>(focused);
        }
    }
    public class LookUpEditOptionsTemplateSelector : DataTemplateSelector {

        public DataTemplate LookUpTemplate { get; set; }
        public DataTemplate SearchLookUpTemplate { get; set; }
        public DataTemplate MultiSelectLookUpTemplate { get; set; }
        public DataTemplate PlaceHolderTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var edit = item as LookUpEditBase;
            if(edit != null) {
                switch(edit.Name) {
                    case "defaultLookUpEdit": return LookUpTemplate;
                    case "searchLookUpEdit": return SearchLookUpTemplate;
                    case "multiSelectLookUpEdit": return MultiSelectLookUpTemplate;
                }
            }
            return PlaceHolderTemplate;
        }
    }
}
