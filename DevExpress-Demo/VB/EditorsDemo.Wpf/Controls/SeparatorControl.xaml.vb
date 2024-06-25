Imports System.Windows
Imports System.Windows.Controls

Namespace EditorsDemo

    Public Partial Class SeparatorControl
        Inherits UserControl

        Public Shared ReadOnly HeaderProperty As DependencyProperty

        Shared Sub New()
            HeaderProperty = DependencyProperty.Register("Header", GetType(String), GetType(SeparatorControl))
        End Sub

        Public Sub New()
            InitializeComponent()
            DataContext = Me
        End Sub

        Public Property Header As String
            Get
                Return CStr(GetValue(HeaderProperty))
            End Get

            Set(ByVal value As String)
                SetValue(HeaderProperty, value)
            End Set
        End Property
    End Class
End Namespace
