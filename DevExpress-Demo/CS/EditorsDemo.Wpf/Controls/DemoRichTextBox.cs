using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Documents;
using System.Collections.Generic;

namespace DemoUtils {
    public class DemoRichControl : System.Windows.Controls.RichTextBox {
        public DemoRichControl() {
            DefaultStyleKey = typeof(System.Windows.Controls.RichTextBox);
        }
        public bool TextIsBold {
            get { return IsTextBold(); }
            set { ToggleTextFormatBold(value); }
        }
        public bool TextIsItalic {
            get { return IsTextItalic(); }
            set { ToggleTextFormatItalic(value); }
        }
        public bool TextIsUnderline {
            get { return IsTextUnderline(); }
            set { ToggleTextFormatUnderline(value); }
        }
        public string Text {
            get { return Selection.Text; }
            set { Selection.Text = value; }
        }
        public object TextFontFamily {
            get {
                object value = Selection.GetPropertyValue(Run.FontFamilyProperty);
                return (value == DependencyProperty.UnsetValue) ? null : value;
            }
            set {
                if(value == null || value == TextFontFamily) return;
                try {
                    if(value is string)
                        Selection.ApplyPropertyValue(Run.FontFamilyProperty, new FontFamily(value as string));
                    else
                        Selection.ApplyPropertyValue(Run.FontFamilyProperty, value);
                } catch { }
            }
        }
        public object TextFontSize {
            get {
                object value = Selection.GetPropertyValue(Run.FontSizeProperty);
                if(value == DependencyProperty.UnsetValue)
                    return null;
                return value;
            }
            set {
                if(value == null || value.Equals(TextFontSize))
                    return;
                
                Selection.ApplyPropertyValue(Run.FontSizeProperty, Convert.ToDouble(value));
            }
        }
        public Color TextColor {
            set {
                if(object.Equals(value, TextColor))
                    return;
                Selection.ApplyPropertyValue(Run.ForegroundProperty, new SolidColorBrush(value));
            }
            get {                
                SolidColorBrush brush = Selection.GetPropertyValue(Run.ForegroundProperty) as SolidColorBrush;
                if(brush == null)
                    return Colors.Black;
                return brush.Color;
            }
        }
        public void SetTextColor(Color value) {
            Selection.ApplyPropertyValue(Run.ForegroundProperty, new SolidColorBrush(value));
        }
        public Color TextBackgroundColor {
            set {

                if(value == TextBackgroundColor)
                    return;
                SetTextBackgroundColor(value);
            }
            get {
                SolidColorBrush brush = Selection.GetPropertyValue(Run.BackgroundProperty) as SolidColorBrush;
                if(brush == null)
                    return Colors.Black;
                return brush.Color;
            }
        }
        public void SetTextBackgroundColor(Color value) {
            Selection.ApplyPropertyValue(Run.BackgroundProperty, new SolidColorBrush(value));
        }
        public TextAlignment GetTextAlignment() {
            object value = Selection.GetPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty);
            if(object.Equals(value, DependencyProperty.UnsetValue)) return TextAlignment.Left;
            if(object.Equals(value, TextAlignment.Center)) return TextAlignment.Center;
            else if(object.Equals(value, TextAlignment.Right)) return TextAlignment.Right;
            else return TextAlignment.Left;
        }
        public void ToggleTextAlignmentLeft() {
            Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty, TextAlignment.Left);
        }
        public void ToggleTextAlignmentCenter() {
            Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty, TextAlignment.Center);
        }
        public void ToggleTextAlignmentRight() {
            Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty, TextAlignment.Right);
        }
        public void ToggleTextAlignmentJustify() {
            Selection.ApplyPropertyValue(System.Windows.Documents.Paragraph.TextAlignmentProperty, TextAlignment.Justify);
        }
        public void Clear() {
            (Document as FlowDocument).Blocks.Clear();
        }
        public void Print() {
             System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();
             if(dialog.ShowDialog() != true) return;
             dialog.PrintVisual(this, string.Empty);
        }
        
        public bool IsEmpty {
            get {
                foreach(Block b in Document.Blocks) {
                    if(!(b is Paragraph))
                        return false;

                    foreach(object o in ((Paragraph)b).Inlines) {
                        if(!(o is Run))
                            return false;
                        Run r = o as Run;
                        if(!string.IsNullOrEmpty(r.Text))
                            return false;
                    }
            }
            return true;
            }
        }
        public bool IsSelectionEmpty {
            get {
                return Selection.IsEmpty;
            }
        }
        protected bool IsTextBold() {
            object value = Selection.GetPropertyValue(TextElement.FontWeightProperty);
            return (value == DependencyProperty.UnsetValue) ? false : (object.Equals(value, FontWeights.Bold));
        }
        protected bool IsTextItalic() {
            object value = Selection.GetPropertyValue(Run.FontStyleProperty);
            return (value == DependencyProperty.UnsetValue) ? false : (object.Equals(value, FontStyles.Italic));
        }
        protected bool IsTextUnderline() {
            object value = Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            return (value == DependencyProperty.UnsetValue) ? false : value != null && System.Windows.TextDecorations.Underline.Equals(value);
        }
        protected void ToggleTextFormatBold(bool bold) {
            if(bold == IsTextBold())
                return;
            if (!bold)
                Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
            else
                Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
        }
        protected void ToggleTextFormatItalic(bool italic) {
            if(italic == IsTextItalic())
                return;
            if (!italic)
                Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
            else
                Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
        }
        protected void ToggleTextFormatUnderline(bool underline) {
            if(underline == IsTextUnderline())
                return;
            if (!underline)
                Selection.ApplyPropertyValue(Run.TextDecorationsProperty, null);
            else
                Selection.ApplyPropertyValue(Run.TextDecorationsProperty, System.Windows.TextDecorations.Underline);
        }        
        public T GetUIElementUnderSelection<T>(BlockCollection blocks) where T : class {
            foreach(Block block in blocks) {
                Paragraph ph = block as Paragraph;
                if(ph != null) {
                    foreach(object obj in ph.Inlines) {
                        if(obj is Run)
                            continue;
                        InlineUIContainer cont = obj as InlineUIContainer;
                        if(cont != null && cont.ContentStart.CompareTo(Selection.Start) > 0 && cont.ContentStart.CompareTo(Selection.End) < 0) {
                            if(cont.Child is T)
                                return cont.Child as T;
                        }
                    }
                }
                else {
                    List lst = block as List;
                    if(lst != null) {
                        foreach(ListItem lstItem in lst.ListItems) {
                            T retVal = GetUIElementUnderSelection<T>(lstItem.Blocks);
                            if(retVal != null)
                                return retVal;
                        }
                    }
                }
            }
            return null;
            
        }
        public TextMarkerStyle ListMarkerStyle {
            get {
                Paragraph startParagraph = Selection.Start.Paragraph;
                Paragraph endParagraph = Selection.End.Paragraph;
                if(startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List)) {
                    return ((ListItem)startParagraph.Parent).List.MarkerStyle;
                }
                return TextMarkerStyle.None;
            }
            set {
                if(value == ListMarkerStyle)
                    return;
                Paragraph p = Selection.Start.Paragraph;
                if(p == null)
                    return;
                if(value == TextMarkerStyle.None) {
                    if(p.Parent is ListItem) {
                        EditingCommands.ToggleBullets.Execute(null, this);
                        p = Selection.Start.Paragraph;
                        if(p.Parent is ListItem) {
                            EditingCommands.ToggleBullets.Execute(null, this);
                        }
                    }
                    return;
                }
                if(!(p.Parent is ListItem)) {
                    EditingCommands.ToggleBullets.Execute(null, this);
                    p = this.Selection.Start.Paragraph;
                }

                if(p == null || !(p.Parent is ListItem))
                    return;
                ((ListItem)p.Parent).List.MarkerStyle = value;
            }
        }
        public T GetUIElementUnderSelection<T>() where T : class {
            BlockCollection col = Document.Blocks;
            if(Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward) == null ||
                Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward).CompareTo(Selection.End) != 0)
                return null;
            return GetUIElementUnderSelection<T>(col);            
        }
    }
}
