Imports System

Namespace ProductsDemo.Modules

    Public Class SeriesTypeDescriptor

        Public Sub New(ByVal seriesType As Type, ByVal diagramType As Type, ByVal displayText As String)
            Me.SeriesType = seriesType
            Me.DiagramType = diagramType
            Me.DisplayText = displayText
        End Sub

        Public Property SeriesType As Type

        Public Property DiagramType As Type

        Public Property DisplayText As String
    End Class

    Public Enum ChartOrientation
        GenerateSeriesFromColumns
        GenerateSeriesFromRows
    End Enum

    Public Class OnlyItemWrapper
        Inherits ItemWrapper

        Private _ChartOrientation As ChartOrientation

        Public Sub New(ByVal chartOrientation As ChartOrientation)
            Me.ChartOrientation = chartOrientation
        End Sub

        Public Overrides ReadOnly Property Text As String
            Get
                If ChartOrientation = ChartOrientation.GenerateSeriesFromColumns Then Return "Series from columns"
                Return "Series from rows"
            End Get
        End Property

        Public Property ChartOrientation As ChartOrientation
            Get
                Return _ChartOrientation
            End Get

            Private Set(ByVal value As ChartOrientation)
                _ChartOrientation = value
            End Set
        End Property
    End Class

    Public Class ItemWrapper

        Private textField As String

        Protected Sub New()
        End Sub

        Public Sub New(ByVal text As String)
            textField = text
        End Sub

        Public Overrides Function ToString() As String
            Return Text
        End Function

        Public Overridable ReadOnly Property Text As String
            Get
                Return textField
            End Get
        End Property

        Public Shared Function Create(ByVal str As String) As ItemWrapper
            Return New ItemWrapper(str)
        End Function
    End Class
End Namespace
