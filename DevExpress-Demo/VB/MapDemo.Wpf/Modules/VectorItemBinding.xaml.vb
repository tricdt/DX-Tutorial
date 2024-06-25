Imports System
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Xml.Linq
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class VectorItemBinding
        Inherits MapDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = LoadDataFromXML()
        End Sub

        Private Function LoadDataFromXML() As ObservableCollection(Of ShipInfo)
            Dim ships As ObservableCollection(Of ShipInfo) = New ObservableCollection(Of ShipInfo)()
            Dim document As XDocument = LoadXmlFromResources("/Data/Ships.xml")
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("Ships").Elements()
                    Dim shipInfo As ShipInfo = New ShipInfo(Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture), element.Element("Name").Value, element.Element("Description").Value, Convert.ToInt16(element.Element("Year").Value))
                    ships.Add(shipInfo)
                Next
            End If

            Return ships
        End Function
    End Class

    Public Class ShipInfo

        Private _Location As GeoPoint, _Name As String, _Year As Integer, _Description As String

        Public Sub New(ByVal latitude As Double, ByVal longitude As Double, ByVal name As String, ByVal description As String, ByVal year As Integer)
            Location = New GeoPoint(latitude, longitude)
            Me.Name = name
            Me.Year = year
            Me.Description = description
        End Sub

        Public Property Location As GeoPoint
            Get
                Return _Location
            End Get

            Private Set(ByVal value As GeoPoint)
                _Location = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Year As Integer
            Get
                Return _Year
            End Get

            Private Set(ByVal value As Integer)
                _Year = value
            End Set
        End Property

        Public Property Description As String
            Get
                Return _Description
            End Get

            Private Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public ReadOnly Property Header As String
            Get
                Return Name & " (" & Year & ")"
            End Get
        End Property
    End Class
End Namespace
