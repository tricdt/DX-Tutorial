using System;
using System.Windows;
using System.Windows.Media;

namespace PropertyGridDemo {
    public class SimpleEditingData {
        public int IntProperty { get; set; }
        public int? NullableIntProperty { get; set; }
        public double DoubleProperty { get; set; }
        public double? NullableDoubleProperty { get; set; }
        public bool BoolProperty { get; set; }
        public bool? NullableBoolProperty { get; set; }
        public char CharProperty { get; set; }
        public char? NullableCharProperty { get; set; }
        public Gender EnumProperty { get; set; }
        public Gender? NullableEnumProperty { get; set; }
        public string StringProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public DateTime? NullableDateTimeProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public decimal? NullableDecimalProperty { get; set; }
        public Point PointProperty { get; set; }
        public Color ColorProperty { get; set; }

        public decimal CurrencyProperty { get; set; }
        public string MultilineTextProperty { get; set; }
        public string PasswordProperty { get; set; }
        public string PhoneNumberProperty { get; set; }
    }
}
