using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.VideoRent.Wpf.Helpers {
    class NoteControl : Control {
        public static readonly DependencyProperty TextProperty;

        static NoteControl() {
            Type ownerType = typeof(NoteControl);
            TextProperty = DependencyProperty.Register("Text", typeof(string), ownerType, new PropertyMetadata(null));
        }
        public NoteControl() {
            DefaultStyleKey = typeof(NoteControl);
        }
        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }
    }
}
