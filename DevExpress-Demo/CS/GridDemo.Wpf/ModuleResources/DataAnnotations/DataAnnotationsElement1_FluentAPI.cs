using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace GridDemo {
    [MetadataType(typeof(DataAnnotationsElement1Metadata))]
    public class DataAnnotationsElement1_FluentAPI {
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
        public Point ComplexTypeProperty { get; set; }
        public decimal CurrencyProperty { get; set; }
        public string MultilineTextProperty { get; set; }
        public string PhoneNumberProperty { get; set; }
    }

    public class DataAnnotationsElement1Metadata : IMetadataProvider<DataAnnotationsElement1_FluentAPI> {
        void IMetadataProvider<DataAnnotationsElement1_FluentAPI>.BuildMetadata(MetadataBuilder<DataAnnotationsElement1_FluentAPI> builder) {
            builder.Property(p => p.IntProperty).DisplayName("Int");
            builder.Property(p => p.NullableIntProperty).DisplayName("Nullable Int");
            builder.Property(p => p.DoubleProperty).DisplayName("Double");
            builder.Property(p => p.NullableDoubleProperty).DisplayName("Nullable Double");
            builder.Property(p => p.BoolProperty).DisplayName("Bool");
            builder.Property(p => p.NullableBoolProperty).DisplayName("Nullable Bool");
            builder.Property(p => p.CharProperty).DisplayName("Char");
            builder.Property(p => p.NullableCharProperty).DisplayName("Nullable Char");
            builder.Property(p => p.EnumProperty).DisplayName("Enum");
            builder.Property(p => p.NullableEnumProperty).DisplayName("Nullable Enum");
            builder.Property(p => p.StringProperty).DisplayName("String");
            builder.Property(p => p.DateTimeProperty).DisplayName("DateTime");
            builder.Property(p => p.NullableDateTimeProperty).DisplayName("Nullable DateTime");
            builder.Property(p => p.DecimalProperty).DisplayName("Decimal");
            builder.Property(p => p.NullableDecimalProperty).DisplayName("Nullable Decimal");
            builder.Property(p => p.ComplexTypeProperty).DisplayName("Point");
            builder.Property(p => p.CurrencyProperty).CurrencyDataType().DisplayName("Currency");
            builder.Property(p => p.MultilineTextProperty).MultilineTextDataType().DisplayName("Multiline Text");
            builder.Property(p => p.PhoneNumberProperty).PhoneNumberDataType().DisplayName("Phone Number");

            builder.Group("Numeric Types")
                .ContainsProperty(p => p.IntProperty)
                .ContainsProperty(p => p.NullableIntProperty)
                .ContainsProperty(p => p.DoubleProperty)
                .ContainsProperty(p => p.NullableDoubleProperty)
                .ContainsProperty(p => p.DecimalProperty)
                .ContainsProperty(p => p.NullableDecimalProperty)
                .ContainsProperty(p => p.CurrencyProperty);
            builder.Group("Bool Types")
                 .ContainsProperty(p => p.BoolProperty)
                 .ContainsProperty(p => p.NullableBoolProperty);
            builder.Group("Char And String Types")
                 .ContainsProperty(p => p.CharProperty)
                 .ContainsProperty(p => p.NullableCharProperty)
                 .ContainsProperty(p => p.StringProperty)
                 .ContainsProperty(p => p.MultilineTextProperty)
                 .ContainsProperty(p => p.PhoneNumberProperty);
            builder.Group("Enum Types")
                 .ContainsProperty(p => p.EnumProperty)
                 .ContainsProperty(p => p.NullableEnumProperty);
            builder.Group("Date Types")
                 .ContainsProperty(p => p.DateTimeProperty)
                 .ContainsProperty(p => p.NullableDateTimeProperty);
            builder.Group("Complex Types")
                 .ContainsProperty(p => p.ComplexTypeProperty);
        }
    }
}
