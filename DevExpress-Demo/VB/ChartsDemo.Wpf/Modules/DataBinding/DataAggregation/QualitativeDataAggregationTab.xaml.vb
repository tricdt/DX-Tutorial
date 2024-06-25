Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Markup
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Public Partial Class QualitativeDataAggregationTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
            lbeAggregationFunctions.FilterCriteria = CriteriaOperator.Parse("Not ([Id] in ('Financial', 'Histogram'))")
        End Sub
    End Class

    Public Class AggregateFunctionToTitleConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value.GetType() IsNot GetType(AggregateFunction) Then Return value
            Dim [function] As AggregateFunction = CType(value, AggregateFunction)
            Select Case [function]
                Case AggregateFunction.None
                    Return "Sale Prices (USD)"
                Case AggregateFunction.Average
                    Return "Average Sale Price (USD)"
                Case AggregateFunction.Minimum
                    Return "Minimum Sale Price (USD)"
                Case AggregateFunction.Maximum
                    Return "Maximum Sale Price (USD)"
                Case AggregateFunction.Sum
                    Return "Total Income (USD)"
                Case AggregateFunction.Count
                    Return "Sold Items Count"
                Case AggregateFunction.Custom
                    Return "Price Standard Deviation"
                Case Else
                    Throw New InvalidEnumArgumentException(String.Format("The {0} value is not supported.", [function]))
            End Select
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return value
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
