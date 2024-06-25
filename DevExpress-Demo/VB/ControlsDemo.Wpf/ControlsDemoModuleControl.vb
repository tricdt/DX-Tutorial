Imports System
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DevExpress.Xpf.DemoBase

Namespace ControlsDemo

    Public Class ControlsDemoModule
        Inherits DemoModule

    End Class

    Public Class SourceConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Class UniversalContainerConverter(Of T)
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Return If(TypeOf value Is T, GetContainer(CType(value, T)), Nothing)
        End Function

        Protected Overridable Function GetContainer(ByVal value As T) As Object
            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return CType(value, UniversalContainer(Of T)).Value
        End Function
#End Region
    End Class

    Public Class UniversalContainer(Of T)

        Public Property Name As String

        Public Property DisplayName As String

        Public Property Value As T

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return If(TypeOf obj Is UniversalContainer(Of T), Equals(Value, CType(obj, UniversalContainer(Of T)).Value), False)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Value.GetHashCode()
        End Function
    End Class

    Public Enum DockSide
        Top
        Bottom
    End Enum
End Namespace
