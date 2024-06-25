using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EditorsDemo {
    public partial class SVGImageModule {
        public SVGImageModule() {
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata<EnumMetadataBuilder>();
            InitializeComponent();
        }

        void cmbGlyphSizesEditValueChanged(object sender, EditValueChangedEventArgs e) {
            editor.EditValue = 1;
        }
    }
    public class EnumMetadataBuilder {
        public static void BuildMetadata(EnumMetadataBuilder<HorizontalAlignment> builder) {
            builder
                .Member(HorizontalAlignment.Center)
                    .ImageUri("pack://application:,,,/Images/Svg/CenterAlignment.svg")
                .EndMember()
                .Member(HorizontalAlignment.Left)
                    .ImageUri("pack://application:,,,/Images/Svg/LeftAlignment.svg")
                .EndMember()
                .Member(HorizontalAlignment.Right)
                    .ImageUri("pack://application:,,,/Images/Svg/RightAlignment.svg")
                .EndMember()
                .Member(HorizontalAlignment.Stretch)
                    .ImageUri("pack://application:,,,/Images/Svg/StretchAlignment.svg")
                .EndMember();
        }
    }
}
