Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Xml.Linq

Namespace ChartsDemo

    Friend Module HPIStatistics

        Public Function Load() As List(Of HpiStatisticsItem)
            Dim document As XDocument = LoadXmlFromResources("/Data/HPI.xml")
            Dim result As List(Of HpiStatisticsItem) = New List(Of HpiStatisticsItem)()
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("G20HPIs").Elements()
                    Dim country As String = element.Element("Country").Value
                    Dim population As Double = Integer.Parse(element.Element("Population").Value) / 1000000.0R
                    Dim hpi As Double = Convert.ToDouble(element.Element("HPI").Value, CultureInfo.InvariantCulture)
                    Dim product As Decimal = Convert.ToDecimal(element.Element("Product").Value, CultureInfo.InvariantCulture)
                    result.Add(New HpiStatisticsItem(country, population, hpi, product, String.Format("{0:n2}", hpi)))
                Next
            End If

            Return result
        End Function
    End Module

    Public Class HpiStatisticsItem

        Private _Country As String, _Population As Double, _HPI As Double, _HPIHint As String, _Product As Decimal

        Public Property Country As String
            Get
                Return _Country
            End Get

            Private Set(ByVal value As String)
                _Country = value
            End Set
        End Property

        Public Property Population As Double
            Get
                Return _Population
            End Get

            Private Set(ByVal value As Double)
                _Population = value
            End Set
        End Property

        Public Property HPI As Double
            Get
                Return _HPI
            End Get

            Private Set(ByVal value As Double)
                _HPI = value
            End Set
        End Property

        Public Property HPIHint As String
            Get
                Return _HPIHint
            End Get

            Private Set(ByVal value As String)
                _HPIHint = value
            End Set
        End Property

        Public Property Product As Decimal
            Get
                Return _Product
            End Get

            Private Set(ByVal value As Decimal)
                _Product = value
            End Set
        End Property

        Public Sub New(ByVal country As String, ByVal population As Double, ByVal hpi As Double, ByVal product As Decimal, ByVal hpiHint As String)
            Me.Country = country
            Me.Population = population
            Me.HPI = hpi
            Me.Product = product
            Me.HPIHint = hpiHint
        End Sub
    End Class
End Namespace
