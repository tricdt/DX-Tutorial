Imports DevExpress.Xpf.Charts
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data

Namespace ProductsDemo.Modules

    <ValueConversion(GetType(Decimal), GetType(Integer))>
    Public Class DecimalConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim [date] As Decimal = CInt(value)
            Return [date]
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim [date] As Integer = CInt((CDec(value)))
            Return [date]
        End Function
    End Class

    Public Class SeriesTypeDescriptorRepository

        Private _SeriesTypeDescriptors As List(Of ProductsDemo.Modules.SeriesTypeDescriptor)

        Public Property SeriesTypeDescriptors As List(Of SeriesTypeDescriptor)
            Get
                Return _SeriesTypeDescriptors
            End Get

            Private Set(ByVal value As List(Of SeriesTypeDescriptor))
                _SeriesTypeDescriptors = value
            End Set
        End Property

        Public Sub New()
            SeriesTypeDescriptors = SeriesTypes
        End Sub

        Private Shared ReadOnly XYDiagram2DType As Type = GetType(XYDiagram2D)

        Private Shared ReadOnly XYDiagram3DType As Type = GetType(XYDiagram3D)

        Private Shared ReadOnly SimpleDiagram3DType As Type = GetType(SimpleDiagram3D)

        Private Shared ReadOnly SimpleDiagram2DType As Type = GetType(SimpleDiagram2D)

        Private Shared ReadOnly DefaultSeriesType As Type = GetType(BarStackedSeries2D)

        Private Shared seriesTypesField As List(Of SeriesTypeDescriptor)

        Private Shared Function CreateSeriesTypes() As List(Of SeriesTypeDescriptor)
            Dim seriesTypes As List(Of SeriesTypeDescriptor) = New List(Of SeriesTypeDescriptor)()
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(AreaFullStackedSeries2D), XYDiagram2DType, "Area Full-Stacked Series 2D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(AreaSeries2D), XYDiagram2DType, "Area Series 2D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(AreaStackedSeries2D), XYDiagram2DType, "Area Stacked Series 2D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(BarFullStackedSeries2D), XYDiagram2DType, "Bar Full-Stacked Series 2D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(BarStackedSeries2D), XYDiagram2DType, "Bar Stacked Series 2D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(LineSeries2D), XYDiagram2DType, "Line Series 2D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(PointSeries2D), XYDiagram2DType, "Point Series 2D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(AreaSeries3D), XYDiagram3DType, "Area Series 3D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(AreaStackedSeries3D), XYDiagram3DType, "Area Stacked Series 3D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(AreaFullStackedSeries3D), XYDiagram3DType, "Area Full-Stacked Series 3D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(BarSeries3D), XYDiagram3DType, "Bar Series 3D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(PointSeries3D), XYDiagram3DType, "Point Series 3D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(PieSeries3D), SimpleDiagram3DType, "Pie Series 3D"))
            seriesTypes.Add(New SeriesTypeDescriptor(GetType(PieSeries2D), SimpleDiagram2DType, "Pie Series 2D"))
            seriesTypes.Sort(New Comparison(Of SeriesTypeDescriptor)(AddressOf CompareComboItemsByStringContent))
            Return seriesTypes
        End Function

        Private Shared ReadOnly Property SeriesTypes As List(Of SeriesTypeDescriptor)
            Get
                If seriesTypesField Is Nothing Then seriesTypesField = CreateSeriesTypes()
                Return seriesTypesField
            End Get
        End Property

        Private Shared Function CompareComboItemsByStringContent(ByVal first As SeriesTypeDescriptor, ByVal second As SeriesTypeDescriptor) As Integer
            Dim firstStr As String = TryCast(first.DisplayText, String)
            Return If(Equals(firstStr, Nothing), -1, firstStr.CompareTo(second.DisplayText))
        End Function
    End Class
End Namespace
