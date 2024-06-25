Imports System.Windows
Imports System.Windows.Controls

Namespace MapDemo

    Public Class MapShapeTooltip
        Inherits Control

        Public Shared ReadOnly TextProperty As DependencyProperty = DependencyProperty.Register("Text", GetType(String), GetType(MapShapeTooltip), New PropertyMetadata(String.Empty))

        Public Shared ReadOnly TopProperty As DependencyProperty = DependencyProperty.Register("Top", GetType(Double), GetType(MapShapeTooltip), New PropertyMetadata(0.0))

        Public Shared ReadOnly LeftProperty As DependencyProperty = DependencyProperty.Register("Left", GetType(Double), GetType(MapShapeTooltip), New PropertyMetadata(0.0))

        Public Property Text As String
            Get
                Return CStr(GetValue(TextProperty))
            End Get

            Set(ByVal value As String)
                SetValue(TextProperty, value)
            End Set
        End Property

        Public Property Left As Double
            Get
                Return CDbl(GetValue(LeftProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(LeftProperty, value)
            End Set
        End Property

        Public Property Top As Double
            Get
                Return CDbl(GetValue(TopProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(TopProperty, value)
            End Set
        End Property

        Const verticalOffset As Double = -30

        Const horizontalOffset As Double = 0

        Public Sub New()
            DefaultStyleKey = GetType(MapShapeTooltip)
        End Sub

        Public Sub Show(ByVal relativeTo As FrameworkElement, ByVal position As Point, ByVal text As String)
            Left = position.X + horizontalOffset
            Top = position.Y + verticalOffset
            Me.Text = text
            Visibility = Visibility.Visible
        End Sub

        Public Sub Hide()
            Visibility = Visibility.Collapsed
            Text = ""
        End Sub
    End Class
End Namespace
