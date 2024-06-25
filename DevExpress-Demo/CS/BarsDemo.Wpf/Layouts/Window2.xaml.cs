using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;
using System.Windows;
using System.Windows.Controls;

namespace BarsDemo {
    
    
    
    public partial class AlignmentView : UserControl {
        public AlignmentView() {
            InitializeComponent();
        }        
    }
    public class AlignmentModel {
        [BindableProperty(OnPropertyChangedMethodName = "OnChanged")]
        public virtual bool IsLeft { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "OnChanged")]
        public virtual bool IsRight { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "OnChanged")]
        public virtual bool IsCenter { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "OnChanged")]
        public virtual bool IsJustify { get; set; }
        public virtual TextAlignment Alignment { get; set; }

        public AlignmentModel() {
            IsLeft = true;
        }

        public void OnChanged() {
            if (IsLeft)
                Alignment = TextAlignment.Left;
            if (IsRight)
                Alignment = TextAlignment.Right;
            if (IsJustify)
                Alignment = TextAlignment.Justify;
            if (IsCenter)
                Alignment = TextAlignment.Center;
        }
    }
}
