using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PropertyGridDemo {
    [MetadataType(typeof(Metadata.SeriesOptionsMetadata))]
    public class SeriesOptions {
        public SeriesOptions() {
            Fill = FillType.CreateFillOptions(this);
            Border = BorderType.CreateBorderOptions(this);
            Options = ViewModelSource.Create<CommonSeriesOptions>();
            Mirror = ViewModelSource.Create<MirrorOptions>();
            Label = ViewModelSource.Create<LabelOptions>();
            InitializeOptions();
        }

        void InitializeOptions() {
            FillType = FillOptionsType.SolidFill;
            ((SolidFillOptions)Fill).Do(x => {
                x.Color = Colors.LightGray;
                x.Opacity = 0.5d;
            });
            BorderType = BorderOptionsType.Solid;
            ((SolidBorderOptions)Border).Do(x => {
                x.Color = (Color)ColorConverter.ConvertFromString("#FF093367");
                x.Opacity = 0.75;
                x.Thickness = 7d;
                x.DashStyle = DashStyles.Dash;
            });
            ShowLabel = true;
            Mirror.Show = true;
            Options.Model = new BorderlessSimpleBar2DModel();
        }

        [Category("FillnLine")]
        public virtual FillOptions Fill { get; set; }
        public virtual FillOptionsType FillType { get; set; }
        public void OnFillTypeChanged() {
            Fill = FillType.CreateFillOptions(this);
        }

        [Category("FillnLine")]
        public virtual BorderOptions Border { get; set; }
        public virtual BorderOptionsType BorderType { get; set; }
        public void OnBorderTypeChanged() {
            Border = BorderType.CreateBorderOptions(this);
        }
        [Category("Effects")]
        public virtual LabelOptions Label { get; set; }
        public virtual bool ShowLabel { get; set; }
        protected void OnShowLabelChanged() {
            Label = ShowLabel ? ViewModelSource.Create<VisibleLabelOptions>() : ViewModelSource.Create<LabelOptions>();
        }
        [Category("Effects")]
        public MirrorOptions Mirror { get; set; }
        [Category("SeriesOptions")]
        [DisplayName("Series Options")]
        public CommonSeriesOptions Options { get; set; }
    }
    [MetadataType(typeof(Metadata.CommonSeriesOptionsMetadata))]
    public class CommonSeriesOptions {
        public CommonSeriesOptions() {
            Model = Bar2DModel.GetPredefinedKinds().First();
        }
        public virtual object Model { get; set; }
    }
}
