Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class PasswordStrengthControl
        Inherits UserControl

        Public Shared ReadOnly PasswordStrengthProperty As DependencyProperty

        Public Shared ReadOnly IsValidPasswordProperty As DependencyProperty

        Shared Sub New()
            Dim ownerType As Type = GetType(PasswordStrengthControl)
            PasswordStrengthProperty = DependencyProperty.Register("PasswordStrength", GetType(PasswordStrength), ownerType, New PropertyMetadata(PasswordStrength.Weak, AddressOf PasswordStrengthPropertyChanged))
            IsValidPasswordProperty = DependencyProperty.Register("IsValidPassword", GetType(Boolean), ownerType, New PropertyMetadata(False, AddressOf PasswordStrengthPropertyChanged))
        End Sub

        Private Shared Sub PasswordStrengthPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, PasswordStrengthControl).PasswordStrengthChanged()
        End Sub

        Private Shared Sub IsValidPasswordPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, PasswordStrengthControl).IsValidPasswordChanged()
        End Sub

        Private Property EnabledTemplate As DataTemplate

        Private Property DisabledTemplate As DataTemplate

        Private Property EmptyTemplate As DataTemplate

        Public Property PasswordStrength As PasswordStrength
            Get
                Return CType(GetValue(PasswordStrengthProperty), PasswordStrength)
            End Get

            Set(ByVal value As PasswordStrength)
                SetValue(PasswordStrengthProperty, value)
            End Set
        End Property

        Public Property IsValidPassword As Boolean
            Get
                Return CBool(GetValue(IsValidPasswordProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsValidPasswordProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            AddHandler Loaded, AddressOf PasswordStrengthControl_Loaded
        End Sub

        Private Sub PasswordStrengthControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            EnabledTemplate = TryCast(ResourceHelper.FindResource(Me, "enabled"), DataTemplate)
            DisabledTemplate = TryCast(ResourceHelper.FindResource(Me, "disabled"), DataTemplate)
            EmptyTemplate = TryCast(ResourceHelper.FindResource(Me, "empty"), DataTemplate)
            Update()
        End Sub

        Private Sub PasswordStrengthChanged()
            Update()
        End Sub

        Private Sub IsValidPasswordChanged()
            Update()
        End Sub

        Private Sub Update()
            Dim enabled As DataTemplate = If(IsValidPassword, EnabledTemplate, DisabledTemplate)
            Dim contentPresenters = panel.Children.OfType(Of ContentPresenter)().ToArray()
            For i As Integer = 0 To 4 - 1
                contentPresenters(i).ContentTemplate = If(i < CInt(PasswordStrength) + 1, enabled, EmptyTemplate)
            Next
        End Sub
    End Class
End Namespace
