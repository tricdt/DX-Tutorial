using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditDemo {
    public class CustomDrawPagePainter : PagePainter {
        List<FixedRange> _rangesForHighlight;
        RichEditControl _richEditControl;
        FixedRange _rangeForRowHighlight;

        public CustomDrawPagePainter(RichEditControl richEdit, List<FixedRange> rangesForHighlight, FixedRange rangeForRowHighlight) {
            this._richEditControl = richEdit;
            this._rangesForHighlight = rangesForHighlight;
            this._rangeForRowHighlight = rangeForRowHighlight;
        }

        bool IsHighlightingAllowed { get; set; }

        public override void DrawPageArea(LayoutPageArea pageArea) {
            IsHighlightingAllowed = true;
            base.DrawPageArea(pageArea);
            IsHighlightingAllowed = false;
        }
        public override void DrawRow(LayoutRow row) {
            if (IsHighlightingAllowed && row.Range.Intersect(this._rangeForRowHighlight)) {
                RichEditPen pen = new RichEditPen(Colors.Blue, Canvas.ConvertToDrawingLayoutUnits(2, this._richEditControl.LayoutUnit));
                Canvas.DrawRectangle(pen, row.Bounds);
            }
            base.DrawRow(row);
        }
        public override void DrawPlainTextBox(PlainTextBox plainTextBox) {
            HighlightElement(plainTextBox);
            base.DrawPlainTextBox(plainTextBox);
        }
        public override void DrawPageNumberBox(PlainTextBox pageNumberBox) {
            HighlightElement(pageNumberBox);
            base.DrawPageNumberBox(pageNumberBox);
        }
        public override void DrawSpaceBox(PlainTextBox spaceBox) {
            HighlightElement(spaceBox);
            base.DrawSpaceBox(spaceBox);
        }
        void HighlightElement(PlainTextBox element) {
            if (!IsHighlightingAllowed)
                return;
            FixedRange range = this._rangesForHighlight.FirstOrDefault(r => { return element.Range.Intersect(r); });
            if (range == null)
                return;
            RichEditBrush brush = new RichEditBrush(Colors.Yellow);
            if (range.Equals(element.Range))
                Canvas.FillRectangle(brush, element.Bounds);
            else {
                CharacterBoxCollection characterBoxes = this._richEditControl.DocumentLayout.Split(element);
                CharacterBox firstBox = characterBoxes[0];
                CharacterBox lastBox = characterBoxes[characterBoxes.Count - 1];
                foreach (CharacterBox box in characterBoxes) {
                    if (box.Range.Start == range.Start)
                        firstBox = box;
                    if (box.Range.Start + box.Range.Length == range.Start + range.Length)
                        lastBox = box;
                }
                Canvas.FillRectangle(brush, System.Drawing.Rectangle.FromLTRB(firstBox.Bounds.X, firstBox.Bounds.Y, lastBox.Bounds.Right, lastBox.Bounds.Bottom));
            }
        }
    }
}
