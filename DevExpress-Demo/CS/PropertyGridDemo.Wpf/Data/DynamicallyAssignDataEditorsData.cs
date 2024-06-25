using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using DevExpress.Xpf.Editors;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm.DataAnnotations;

namespace PropertyGridDemo {
    [MetadataType(typeof(DynamicallyAssignDataEditorsMetadata))]
    public class DynamicallyAssignDataEditorsData {
        const string SimpleEditorsGroup = "Simple editors";
        const string PopupEditorsGroup = "Popup editors";
        const string MiscEditorsGroup = "Misc editors";

        public DynamicallyAssignDataEditorsData() {
            var list = new MultiEditorsList();
            foreach(PropertyDescriptor property in TypeDescriptor.GetProperties(this)) {
                property.SetValue(this, list.GetValue(property.Name));
            }
        }

        [Display(Name = "TextEdit", GroupName = SimpleEditorsGroup)]
        public string Name { get; set; }

        [Display(Name = "TextEdit (with numeric mask)", Description = "NumericTextEdit", GroupName = SimpleEditorsGroup)]
        public long ID { get; set; }

        [Display(Name = "TextEdit (with RegEx mask)", Description = "RegExTextEdit", GroupName = SimpleEditorsGroup)]
        public string Code { get; set; }

        [Display(Name = "PasswordBoxEdit", GroupName = SimpleEditorsGroup)]
        public string Password { get; set; }

        [Display(Name = "ButtonEdit", GroupName = SimpleEditorsGroup)]
        public string Countries { get; set; }


        [Display(Name = "ComboBoxEdit (with AutoComplete)", Description = "AutoCompleteComboBoxEdit", GroupName = PopupEditorsGroup)]
        public string Country { get; set; }

        [Display(Name = "SearchLookUpEdit", GroupName = PopupEditorsGroup)]
        public long Category1 { get; set; }

        [Display(Name = "LookUpEdit", GroupName = PopupEditorsGroup)]
        public long Category2 { get; set; }

        [Display(Name = "MemoEdit", GroupName = PopupEditorsGroup)]
        public string Notes { get; set; }

        [Display(Name = "DateEdit", GroupName = PopupEditorsGroup)]
        public DateTime Date1 { get; set; }

        [Display(Name = "DatePicker", GroupName = PopupEditorsGroup)]
        public DateTime Date2 { get; set; }

        [Display(Name = "FontEdit", GroupName = PopupEditorsGroup)]
        public FontFamily Font { get; set; }

        [Display(Name = "PopupCalcEdit", GroupName = PopupEditorsGroup)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "PopupColorEdit", GroupName = PopupEditorsGroup)]
        public Color Color { get; set; }

        [Display(Name = "PopupBrushEdit", GroupName = PopupEditorsGroup)]
        public Brush Brush { get; set; }

        [Display(Name = "PopupImageEdit", GroupName = PopupEditorsGroup)]
        public ImageSource Picture { get; set; }


        [Display(Name = "TrackBarEdit", GroupName = MiscEditorsGroup)]
        public double UnitsInStock { get; set; }

        [Display(Name = "ZoomTrackBarEdit", GroupName = MiscEditorsGroup)]
        public double ReorderLevel { get; set; }

        [Display(Name = "RangeTrackBarEdit", GroupName = MiscEditorsGroup)]
        public TrackBarEditRange Range { get; set; }

        [Display(Name = "ProgressBarEdit", GroupName = MiscEditorsGroup)]
        public double UnitsOnOrder { get; set; }

        [Display(Name = "CheckEdit", GroupName = MiscEditorsGroup)]
        public bool Discontinued { get; set; }

        [Display(Name = "ToggleSwitchEdit", GroupName = MiscEditorsGroup)]
        public bool Discontinued2 { get; set; }

        [Display(Name = "SpinEdit", GroupName = MiscEditorsGroup)]
        public double Discount { get; set; }

        [Display(Name = "ListBoxEdit", GroupName = MiscEditorsGroup)]
        public string PalleteSize { get; set; }

        [Display(Name = "BarCodeEdit", GroupName = MiscEditorsGroup)]
        public int Data { get; set; }

        [Display(Name = "RatingEdit", GroupName = MiscEditorsGroup)]
        public double Rating { get; set; }

        [Display(Name = "HyperlinkEdit", GroupName = MiscEditorsGroup)]
        public string HyperLink { get; set; }
    }
    public static class DynamicallyAssignDataEditorsMetadata {
        public static void BuildMetadata(MetadataBuilder<DynamicallyAssignDataEditorsData> builder) {
            builder.Property(x => x.Password)
                .PasswordDataType();
            builder.Property(x => x.Notes)
                .MultilineTextDataType();
        }
    }
}
