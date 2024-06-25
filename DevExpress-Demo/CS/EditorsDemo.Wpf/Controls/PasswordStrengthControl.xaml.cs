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
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;

namespace EditorsDemo {
    
    
    
    public partial class PasswordStrengthControl : UserControl {
        public static readonly DependencyProperty PasswordStrengthProperty;
        public static readonly DependencyProperty IsValidPasswordProperty;
        static PasswordStrengthControl() {
            Type ownerType = typeof(PasswordStrengthControl);
            PasswordStrengthProperty = DependencyProperty.Register("PasswordStrength", typeof(PasswordStrength), ownerType, new PropertyMetadata(PasswordStrength.Weak, PasswordStrengthPropertyChanged));
            IsValidPasswordProperty = DependencyProperty.Register("IsValidPassword", typeof(bool), ownerType, new PropertyMetadata(false, PasswordStrengthPropertyChanged));
        }
        static void PasswordStrengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PasswordStrengthControl)d).PasswordStrengthChanged();
        }
        static void IsValidPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PasswordStrengthControl)d).IsValidPasswordChanged();
        }

        DataTemplate EnabledTemplate { get; set; }
        DataTemplate DisabledTemplate { get; set; }
        DataTemplate EmptyTemplate { get; set; }
        public PasswordStrength PasswordStrength {
            get { return (PasswordStrength)GetValue(PasswordStrengthProperty); }
            set { SetValue(PasswordStrengthProperty, value); }
        }
        public bool IsValidPassword {
            get { return (bool)GetValue(IsValidPasswordProperty); }
            set { SetValue(IsValidPasswordProperty, value); }
        }

        public PasswordStrengthControl() {
            InitializeComponent();
            Loaded += PasswordStrengthControl_Loaded;
        }
        void PasswordStrengthControl_Loaded(object sender, RoutedEventArgs e) {
            EnabledTemplate = ResourceHelper.FindResource(this, "enabled") as DataTemplate;
            DisabledTemplate = ResourceHelper.FindResource(this, "disabled") as DataTemplate;
            EmptyTemplate = ResourceHelper.FindResource(this, "empty") as DataTemplate;
            Update();
        }
        void PasswordStrengthChanged() {
            Update();
        }
        void IsValidPasswordChanged() {
            Update();
        }
        void Update() {
            DataTemplate enabled = IsValidPassword ? EnabledTemplate : DisabledTemplate;
            var contentPresenters = this.panel.Children.OfType<ContentPresenter>().ToArray();
            for(int i = 0; i < 4; i++)
                contentPresenters[i].ContentTemplate = i < (int)PasswordStrength + 1 ? enabled : EmptyTemplate;
        }
    }
}
