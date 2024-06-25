using System;
using System.Windows;
using System.Windows.Media;

namespace PropertyGridDemo {
    public class ObjectInspectorViewModel {
        public virtual SimpleEditingData Data { get; protected set; }
        public ObjectInspectorViewModel() {
            Data = new SimpleEditingData() {
                IntProperty = 123,
                NullableIntProperty = 4567,
                DoubleProperty = 12345.12345,
                NullableDoubleProperty = 6789.6789,
                BoolProperty = true,
                CharProperty = 'Y',
                NullableCharProperty = 'N',
                NullableEnumProperty = Gender.Female,
                StringProperty = "text",
                DateTimeProperty = DateTime.Today,
                NullableDateTimeProperty = DateTime.Today.AddDays(1),
                DecimalProperty = 12345.12345m,
                NullableDecimalProperty = 789.789m,
                PointProperty = new Point(123.45, 678.9),
                CurrencyProperty = 1234567.89m,
                MultilineTextProperty = "first line of text\r\nsecond line of text\r\nthird line of text",
                PasswordProperty = "password",
                PhoneNumberProperty = "1234567890",
                ColorProperty = Colors.Red,
            };
        }
    }
}
