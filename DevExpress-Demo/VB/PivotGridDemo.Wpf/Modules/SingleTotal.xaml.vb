Imports System
Imports System.Globalization
Imports System.Windows.Data
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Partial Class SingleTotal
        Inherits PivotGridDemoModule

        Public Shared ReadOnly Property Converter As IValueConverter
            Get
                Return New DiscountFormatConverter()
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Class DiscountFormatConverter
            Implements IValueConverter

            Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
                If TypeOf value Is FieldSummaryType Then
                    Dim summaryType As FieldSummaryType = CType(value, FieldSummaryType)
                    Select Case summaryType
                        Case FieldSummaryType.Sum
                            Return "N"
                        Case FieldSummaryType.Max, FieldSummaryType.Min, FieldSummaryType.Average, FieldSummaryType.StdDev, FieldSummaryType.StdDevp, FieldSummaryType.Var, FieldSummaryType.Varp
                            Return "p"
                    End Select
                End If

                Return String.Empty
            End Function

            Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
                Throw New NotImplementedException()
            End Function
        End Class
    End Class
End Namespace
