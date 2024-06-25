using System;
using System.Windows;
using System.ComponentModel.DataAnnotations;

namespace TreeListDemo {
    public class DataAnnotationsElement1 {

        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [Display(AutoGenerateField = false)]
        public int ParentID { get; set; }

        [Display(Name = "Int", GroupName = "Numeric Types")]
        public int IntProperty { get; set; }

        [Display(Name = "Nullable Int", GroupName = "Numeric Types")]
        public int? NullableIntProperty { get; set; }

        [Display(Name = "Double", GroupName = "Numeric Types")]
        public double DoubleProperty { get; set; }

        [Display(Name = "Nullable Double", GroupName = "Numeric Types")]
        public double? NullableDoubleProperty { get; set; }

        [Display(Name = "Bool", GroupName = "Bool Types")]
        public bool BoolProperty { get; set; }

        [Display(Name = "Nullable Bool", GroupName = "Bool Types")]
        public bool? NullableBoolProperty { get; set; }

        [Display(Name = "Char", GroupName = "Char And String Types")]
        public char CharProperty { get; set; }

        [Display(Name = "Nullable Char", GroupName = "Char And String Types")]
        public char? NullableCharProperty { get; set; }

        [Display(Name = "Enum", GroupName = "Enum Types")]
        public Gender EnumProperty { get; set; }

        [Display(Name = "Nullable Enum", GroupName = "Enum Types")]
        public Gender? NullableEnumProperty { get; set; }

        [Display(Name = "String", GroupName = "Char And String Types")]
        public string StringProperty { get; set; }

        [Display(Name = "DateTime", GroupName = "Date Types")]
        public DateTime DateTimeProperty { get; set; }

        [Display(Name = "Nullable DateTime", GroupName = "Date Types")]
        public DateTime? NullableDateTimeProperty { get; set; }

        [Display(Name = "Decimal", GroupName = "Numeric Types")]
        public decimal DecimalProperty { get; set; }

        [Display(Name = "Nullable Decimal", GroupName = "Numeric Types")]
        public decimal? NullableDecimalProperty { get; set; }

        [Display(Name = "Point", GroupName = "Complex Types")]
        public Point ComplexTypeProperty { get; set; }

        [Display(Name = "Currency", GroupName = "Numeric Types"), DataType(DataType.Currency)]
        public decimal CurrencyProperty { get; set; }

        [Display(Name = "Multiline Text", GroupName = "Char And String Types"), DataType(DataType.MultilineText)]
        public string MultilineTextProperty { get; set; }

        [Display(Name = "Phone Number", GroupName = "Char And String Types"), DataType(DataType.PhoneNumber)]
        public string PhoneNumberProperty { get; set; }

        public override string ToString() {
            return "Supported data types";
        }
    }
}
