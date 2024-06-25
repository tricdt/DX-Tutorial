using DevExpress.Xpf.Grid;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GridDemo {
    public enum FocusedGrid { First, Second, None }
    public class RowsAnimationElement : FrameworkContentElement {
        public static readonly DependencyProperty NewRowsProgressProperty;
        public static readonly DependencyProperty NewRowsColorProperty;
        static RowsAnimationElement() {
            NewRowsProgressProperty = DependencyProperty.Register("NewRowsProgress", typeof(double), typeof(RowsAnimationElement), new PropertyMetadata(1d));
            NewRowsColorProperty = DependencyProperty.Register("NewRowsColor", typeof(Color), typeof(RowsAnimationElement),
                new PropertyMetadata(Color.FromArgb(0x00, 0xDE, 0xF8, 0xCB)));
        }
        public double NewRowsProgress {
            get { return (double)GetValue(NewRowsProgressProperty); }
            set { SetValue(NewRowsProgressProperty, value); }
        }
        public Color NewRowsColor {
            get { return (Color)GetValue(NewRowsColorProperty); }
            set { SetValue(NewRowsColorProperty, value); }
        }
    }
    [Serializable]
    public class CopyPasteOutlookData {
        public int UniqueID { get; set; }
        public int? OID { get; set; }
        public string From { get; set; }
        public DateTime? Sent { get; set; }
        public bool? HasAttachment { get; set; }
        public double? HoursActive { get; set; }

        static public CopyPasteOutlookData ConvertOutlookDataToCopyPasteOutlookData(OutlookData outlookDataObject, CopyPasteOperations owner) {
            return new CopyPasteOutlookData() { UniqueID = ++owner.Counter, From = outlookDataObject.From, HasAttachment = outlookDataObject.HasAttachment, 
                HoursActive = outlookDataObject.HoursActive, OID = outlookDataObject.OID, Sent = outlookDataObject.Sent };
        }
    }
    class PasteCompetedHelper {
        public CopyPasteOperations Owner { get; set; }
        public Storyboard ColorStoryboard { get; set; }
        public void ColorStoryboardCompleted(object sender, EventArgs e) {
            ColorStoryboard.Completed -= new EventHandler(ColorStoryboardCompleted);
            Owner.RaisePasteCompetedEvent(new RoutedEventArgs(CopyPasteOperations.PasteCompetedEvent));
        }
    }
    class PasteHelper {
        public GridViewBase View { get; set; }
        public BindingList<CopyPasteOutlookData> List { get; set; }
        public int PositionNewRow { get; set; }
        public object[] ObjectsForCopy { get; set; }
        public CopyPasteOperations Owner { get; set; }
        public Storyboard ColorStoryboard { get; set; }

        public void ColorStoryboardCompleted(object sender, EventArgs e) {
            ColorStoryboard.Completed -= new EventHandler(ColorStoryboardCompleted);
            int posNewRow = PositionNewRow;
            Owner.PasteRowsWithoutAnimation(ref posNewRow, View, List, ObjectsForCopy, Owner.MaxAnimationRows, ObjectsForCopy.Length);
            Owner.EndPasteForCanCommands();
        }
    }
}
