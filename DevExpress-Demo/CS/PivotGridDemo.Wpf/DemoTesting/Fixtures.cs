using System;
using System.Threading;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.Editors;
using System.Windows.Controls;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.PivotGrid;
using PivotGridDemo;
using DevExpress.Xpf.PivotGrid.Internal;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Core;

namespace PivotGridDemo.Tests {
    public class PivotGridCheckAllDemosFixture : CheckAllDemosFixture {

        protected override bool CanRunModule(Type moduleType) {
            return moduleType != typeof(ServerMode) && moduleType != typeof(OLAPBrowser) && moduleType != typeof(OLAPKPI);
        }

        protected override bool AllowCheckCodeFile(Type moduleType, CodeLanguage fileLanguage) {
            return base.AllowCheckCodeFile(moduleType, fileLanguage) && !(moduleType == typeof(ServerMode) && fileLanguage == CodeLanguage.VB);
        }

        protected override void CreateCheckOptionsAction() {
            AddCheck0();
            CreateCheck1();
        }

        private void CreateCheck1() {
            if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(ChartsIntegration))
                Dispatch(HideFilterAction);
            Dispatch(AssertTextEditsWidthB185051);
            Dispatch(TextBlockTextTrimmingB185312);
            if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(PrintTemplates)) {
                PivotGridControl pivotGrid = DispatchExpr(() => FindElement<PivotGridControl>(DemoBaseTesting.CurrentDemoModule));
                Window cont = DispatchExpr(() => PrintHelper.ShowPrintPreview(pivotGrid, pivotGrid));
                DispatchBusyWait(() => cont.IsLoaded);
                Thread.Sleep(2000);
                Dispatch(() => { cont.Close(); });
            }
        }

        private void AddCheck0() {
            Dispatch(ComboBoxItemsEditableB181973);
            Dispatch(
                delegate() {
                    if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(ChartsIntegration))
                        ShowFilterAction();
                });
            Dispatch(delegate() {
                if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(ChartsIntegration))
                    BestFitAction();
            });
        }

        void TextBlockTextTrimmingB185312() {
            FrameworkElement control = DemoBaseTesting.CurrentDemoModule.Options;
            if(control == null) return;
            List<TextBlock> editors = FindAllElements<TextBlock>(control);
            foreach(TextBlock edit in editors) {
                bool trimmed = TextBlockService.CalcIsTextTrimmed(edit) && edit.DesiredSize.Width > (edit.ActualWidth + edit.Margin.Left + edit.Margin.Right);
                AssertLog.IsFalse(trimmed, "Text is trimmed: " + DemoBaseTesting.CurrentDemoModule.GetType().Name + " text=" + edit.Text + ", Name=" + edit.Name +
                    ", actual " + edit.ActualWidth + "px, desired:" + edit.DesiredSize.Width + "px");
            }
        }

        void AssertTextEditsWidthB185051() {
            FrameworkElement control = DemoBaseTesting.CurrentDemoModule.Options;
            if(control == null) return;
            List<TextEdit> editors = FindAllElements<TextEdit>(control);
            foreach(TextEdit edit in editors) {
                if(edit.EditMode == EditMode.InplaceInactive || edit.IsPrintingMode == true)
                    continue;
                AssertLog.IsFalse(edit.Width == Double.PositiveInfinity, "TextEdit unlimited width: " + DemoBaseTesting.CurrentDemoModule.GetType().Name + " text=" + edit.Text + ", Name=" + edit.Name + " desired " + edit.ActualWidth + "px");
            }
        }

        void ComboBoxItemsEditableB181973() {
			if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(ExcelStyleFiltering))
				return;
            FrameworkElement control = DemoBaseTesting.CurrentDemoModule.Options;
            if(control == null) return;
            List<ComboBoxEdit> editors = FindAllElements<ComboBoxEdit>(control);
            for(int i = 0;i < editors.Count;i++) {
                AssertLog.IsTrue(editors[i].IsTextEditable == false, "ComboBoxEdit items editable: " + DemoBaseTesting.CurrentDemoModule.GetType().Name + " " + editors[i].Name);
                AssertLog.IsFalse(editors[i].SelectedIndex < 0, "ComboBoxEdit item is not selected: " + DemoBaseTesting.CurrentDemoModule.GetType().Name + " " + editors[i].Name);
            }
        }

        bool ChartGeneralOptionsCondition() {
            return DemoBaseTesting.CurrentDemoModule.GetType() == typeof(ChartsIntegration);
        }

        void BestFitAction() {
            PivotGridControl pivotGrid = FindElement<PivotGridControl>(DemoBaseTesting.CurrentDemoModule);
            pivotGrid.BestFit();
        }
        void HideFilterAction() {
            UpdateLayoutAndDoEvents();
            PivotGridControl pivotGrid = FindElement<PivotGridControl>(DemoBaseTesting.CurrentDemoModule);
            if(pivotGrid != null && pivotGrid.PrefilterContainer != null)
                pivotGrid.PrefilterContainer.Close();
            UpdateLayoutAndDoEvents();
        }

        void ShowFilterAction() {
            UpdateLayoutAndDoEvents();
            PivotGridControl pivotGrid = FindElement<PivotGridControl>(DemoBaseTesting.CurrentDemoModule);
            if(pivotGrid == null || !pivotGrid.UseLegacyFilterEditor.HasValue || !pivotGrid.UseLegacyFilterEditor.Value)
                return;
            pivotGrid.ShowFilterEditor();
            UpdateLayoutAndDoEvents();
        }

        List<T> FindAllElements<T>(FrameworkElement element) where T : FrameworkElement {
            List<T> items = new List<T>();
            LayoutHelper.ForEachElement(element, d => addItem<T>(items, d));
            return items;
        }
        bool addItem<T>(List<T> items, FrameworkElement d) where T : FrameworkElement {
            if(d.GetType() == typeof(T)) items.Add((T)d);
            return true;
        }

        T FindElement<T>(FrameworkElement element) where T : FrameworkElement {
            return (T)LayoutHelper.FindElement(element, d => d.GetType() == typeof(T));
        }
    }
}
