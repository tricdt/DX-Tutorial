Imports System.Windows
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views

    Public Partial Class StaticFiltersPanel
        Inherits UserControl

        Public Property Title As String
            Get
                Return CStr(GetValue(TitleProperty))
            End Get

            Set(ByVal value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property

        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(StaticFiltersPanel), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class
End Namespace
