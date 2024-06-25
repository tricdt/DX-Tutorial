Imports DevExpress.Xpf.PivotGrid
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DependencyPropertyManager = System.Windows.DependencyProperty

Namespace ProductsDemo.Modules

    Public Partial Class PivotGridModule
        Inherits UserControl

        Shared Sub New()
            Dim ownerType As Type = GetType(PivotGridModule)
            PivotGridControlProperty = DependencyPropertyManager.Register("PivotGridControl", GetType(PivotGridModule), ownerType, New PropertyMetadata(Nothing, AddressOf OnPivotGridControlChanged))
        End Sub

        Public Shared Sub OnPivotGridControlChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            If e.NewValue Is Nothing Then Return
            CType(d, PivotGridModule).Pivot = New WeakReference(e.NewValue)
        End Sub

        Private Pivot As WeakReference = Nothing

        Public Shared ReadOnly PivotGridControlProperty As DependencyPropertyManager

        Protected Overridable ReadOnly Property NeedChangeEditorsTheme As Boolean
            Get
                Return False
            End Get
        End Property

        Public Property PivotGridControl As PivotGridControl
            Get
                Return CType(GetValue(PivotGridControlProperty), PivotGridControl)
            End Get

            Set(ByVal value As PivotGridControl)
                SetValue(PivotGridControlProperty, value)
            End Set
        End Property

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.pivotGrid.BestFit()
        End Sub
    End Class

    Public Class IntToKMConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim s As String = TryCast(value, String)
            If Equals(s, Nothing) Then Return value
            Dim result As Integer = Nothing
            If Integer.TryParse(s, result) AndAlso result > 0 Then Return Convert(result)
            Return value
        End Function

        Private Function Convert(ByVal result As Integer) As Object
            If result > 1000000000 Then
                Return(result \ 1000000000).ToString() & "B"
            ElseIf result > 1000000 Then
                Return(result \ 1000000).ToString() & "M"
            ElseIf result > 10000 Then
                Return(result \ 1000).ToString() & "K"
            Else
                Return result.ToString()
            End If
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
