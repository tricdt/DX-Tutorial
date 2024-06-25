using System.Collections.Generic;
using System.Windows;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.Xpf.RichEdit;
using System;
using DrawingPoint = System.Drawing.Point;
using System.Text;

namespace RichEditDemo {
    public partial class HitTesting : RichEditDemoModule {
        Dictionary<LayoutType, RichEditPen> _highlightOptions = new Dictionary<LayoutType, RichEditPen>();

        public HitTesting() {
            InitializeComponent();
            SpecifyHighlightOptions();
        }

        int CurrentPageIndex { get; set; }
        RichEditHitTestResult HitTestResult { get; set; }
        Point MousePosition { get; set; }
        Dictionary<LayoutType, RichEditPen> HighlightOptions { get { return _highlightOptions; } }

        void RichEditControl_BeforePagePaint(object sender, BeforePagePaintEventArgs e) {
            if (e.CanvasOwnerType == CanvasOwnerType.Control && e.Page.Index == CurrentPageIndex && HitTestResult != null) {
                RichEditPen defaultHighlightingPen = (highlightOther.IsChecked == true) ? new RichEditPen(otherHigtlightColor.Color, 3) : null;
                e.Painter = new HitTestPagePainter(RichEditHitTestResult.Reverse(HitTestResult), HighlightOptions, defaultHighlightingPen);
            }
        }
        void HitTest(DrawingPoint point) {
            PageLayoutPosition pageLayoutPosition = RichEditControl.ActiveView.GetDocumentLayoutPosition(point);
            if (pageLayoutPosition == null) {
                HitTestResult = null;
                return;
            }

            CurrentPageIndex = pageLayoutPosition.PageIndex;
            DrawingPoint position = pageLayoutPosition.Position;
            LayoutPage page = RichEditControl.DocumentLayout.GetPage(CurrentPageIndex);
            HitTestManager hitTestManager = new HitTestManager(richEdit.DocumentLayout);
            HitTestSearchOption searchOption = exactHit.IsChecked.Value ? HitTestSearchOption.Exact : HitTestSearchOption.Nearest;

            switch ((ScopeType)cbScope.EditValue) {
                case ScopeType.Page:
                    HitTestResult = hitTestManager.HitTest(page, position, searchOption);
                    break;
                case ScopeType.MainPageArea:
                    HitTestResult = hitTestManager.HitTest(page.PageAreas[0], position, searchOption);
                    break;
                case ScopeType.HeaderPageArea:
                    if (page.Header != null)
                        HitTestResult = hitTestManager.HitTest(page.Header, position, searchOption);
                    break;
                case ScopeType.FooterPageArea:
                    if (page.Footer != null)
                        HitTestResult = hitTestManager.HitTest(page.Footer, position, searchOption);
                    break;
            }
        }
        void HighlightPotionsChanged(object sender, RoutedEventArgs e) {
            SpecifyHighlightOptions();
        }
        void SpecifyHighlightOptions() {
            HighlightOptions.Clear();
            HighlightOptions.Add(LayoutType.Page, (highlightPage.IsChecked == true) ? new RichEditPen(pageHigtlightColor.Color, 3) : null);
            HighlightOptions.Add(LayoutType.PageArea, (highlightPageArea.IsChecked == true) ? new RichEditPen(pageAreaHigtlightColor.Color, 3) : null);
            HighlightOptions.Add(LayoutType.Column, (highlightColumn.IsChecked == true) ? new RichEditPen(columnHigtlightColor.Color, 3) : null);
            HighlightOptions.Add(LayoutType.Row, (highlightRow.IsChecked == true) ? new RichEditPen(rowHigtlightColor.Color, 3) : null);
            HighlightOptions.Add(LayoutType.PlainTextBox, (highlightBox.IsChecked == true) ? new RichEditPen(boxHigtlightColor.Color, 3) : null);
            HighlightOptions.Add(LayoutType.CharacterBox, (highlightCharacterBox.IsChecked == true) ? new RichEditPen(characterBoxHigtlightColor.Color, 3) : null);
        }
        void RichEditControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
            Point position = e.GetPosition(richEdit);
            if (MousePosition != position) {
                HitTest(new DrawingPoint((int)position.X, (int)position.Y));
                RichEditControl.Refresh();
                MousePosition = position;
            }
        }
        string Concat(char c, int count) {
            StringBuilder builder = new StringBuilder(count);
            builder.Append(c, count);
            return builder.ToString();
        }
        void RichEditControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            lbResult.Items.Clear();
            RichEditHitTestResult reversedResult = RichEditHitTestResult.Reverse(HitTestResult);
            int i = 0;
            while (reversedResult != null) {
                string item = String.Format("{0}- {1}", Concat(' ', i * 2), reversedResult.LayoutElement.Type);
                lbResult.Items.Add(item);
                reversedResult = reversedResult.Next;
                i++;
            }
        }
    }
}
