using DevExpress.Mvvm.UI.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BarsDemo {
    public class SelectedTextBehavior : Behavior<TextBox> {
        public static readonly DependencyProperty SelectionStartProperty =
            DependencyProperty.Register("SelectionStart", typeof(int), typeof(SelectedTextBehavior), new PropertyMetadata(0, new PropertyChangedCallback(SelectionStartChangedCallback)));
        public static readonly DependencyProperty SelectionLengthProperty =
            DependencyProperty.Register("SelectionLength", typeof(int), typeof(SelectedTextBehavior), new PropertyMetadata(0, new PropertyChangedCallback(SelectionLengthChangedCallback)));
        public static readonly DependencyProperty SelectedTextProperty =
            DependencyProperty.Register("SelectedText", typeof(string), typeof(SelectedTextBehavior), new PropertyMetadata(null, new PropertyChangedCallback(SelectedTextChangedCallback)));

        public int SelectionStart {
            get { return (int)GetValue(SelectionStartProperty); }
            set { SetValue(SelectionStartProperty, value); }
        }
        public int SelectionLength {
            get { return (int)GetValue(SelectionLengthProperty); }
            set { SetValue(SelectionLengthProperty, value); }
        }
        public string SelectedText {
            get { return (string)GetValue(SelectedTextProperty); }
            set { SetValue(SelectedTextProperty, value); }
        }

        static void SelectionStartChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((SelectedTextBehavior)d).SelectionStartChangedCallback(e);
        }
        static void SelectionLengthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((SelectedTextBehavior)d).SelectionLengthChangedCallback(e);
        }
        static void SelectedTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((SelectedTextBehavior)d).SelectedTextChangedCallback(e);
        }

        bool isUpdate = false;
        void SelectedTextChangedCallback(DependencyPropertyChangedEventArgs e) {
            if(!isUpdate)
                AssociatedObject.SelectedText = (string)e.NewValue;
        }
        void SelectionLengthChangedCallback(DependencyPropertyChangedEventArgs e) {
            if(!isUpdate)
                AssociatedObject.SelectionLength = (int)e.NewValue;
        }
        void SelectionStartChangedCallback(DependencyPropertyChangedEventArgs e) {
            if(!isUpdate)
                AssociatedObject.SelectionStart = (int)e.NewValue;
        }
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }
        void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e) {
            AssociatedObject.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
        void AssociatedObject_SelectionChanged(object sender, RoutedEventArgs e) {
            isUpdate = true;
            SelectedText = AssociatedObject.SelectedText;
            SelectionStart = AssociatedObject.SelectionStart;
            SelectionLength = AssociatedObject.SelectionLength;
            isUpdate = false;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
        }
    }
}
