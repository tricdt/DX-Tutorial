Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Media
Imports System.Xml.Linq
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.TreeMap

Namespace TreeMapDemo

    <CodeFile("Modules/Sunburst/FlatDataAdapter.xaml")>
    <CodeFile("Modules/Sunburst/FlatDataAdapter.xaml.(cs)")>
    <NoAutogeneratedCodeFiles>
    Public Partial Class SunburstFlatDataAdapterDemo
        Inherits TreeMapDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = LoadDataFromXML()
        End Sub

        Private Function LoadDataFromXML() As List(Of ChemicalElement)
            Dim document As XDocument = LoadXDocumentFromResources("/Data/ChemicalElements.xml")
            Dim infos As List(Of ChemicalElement) = New List(Of ChemicalElement)()
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("ArrayOfElement").Elements()
                    Dim chemicalElement As ChemicalElement = New ChemicalElement()
                    chemicalElement.Name = element.Element("Name").Value
                    chemicalElement.AtomicMass = element.Element("AtomicMass").Value
                    chemicalElement.AtomicNumber = element.Element("AtomicNumber").Value
                    chemicalElement.Density = element.Element("Density").Value
                    chemicalElement.MeltingPoint = element.Element("MeltingPoint").Value
                    chemicalElement.BoilingPoint = element.Element("BoilingPoint").Value
                    chemicalElement.Block = element.Element("Block").Value
                    chemicalElement.Family = element.Element("Family").Value
                    chemicalElement.Symbol = element.Element("Symbol").Value
                    infos.Add(chemicalElement)
                Next
            End If

            Return infos
        End Function
    End Class

    Public Class ChemicalElement

        Public Property AtomicNumber As String

        Public Property AtomicMass As String

        Public Property Density As String

        Public Property MeltingPoint As String

        Public Property BoilingPoint As String

        Public Property Name As String

        Public Property Block As String

        Public Property Family As String

        Public Property Symbol As String

        Public ReadOnly Property FakeValue As Integer
            Get
                Return 1
            End Get
        End Property
    End Class

    Public Class ChemicalElementColorizer
        Inherits SunburstPaletteColorizer

        Private brushesField As Dictionary(Of String, Brush)

        Private ReadOnly Property Brushes As Dictionary(Of String, Brush)
            Get
                If brushesField Is Nothing Then
                    brushesField = New Dictionary(Of String, Brush)()
                    Dim sBlockBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 216, 103, 159))
                    sBlockBrush.Freeze()
                    brushesField("S-block") = sBlockBrush
                    Dim pBlockBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 234, 202, 39))
                    pBlockBrush.Freeze()
                    brushesField("P-block") = pBlockBrush
                    Dim dBlockBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 102, 156, 220))
                    dBlockBrush.Freeze()
                    brushesField("D-block") = dBlockBrush
                    Dim fBlockBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 126, 171, 54))
                    fBlockBrush.Freeze()
                    brushesField("F-block") = fBlockBrush
                    Dim lanthanideBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 145, 183, 46))
                    lanthanideBrush.Freeze()
                    brushesField("Lanthanide") = lanthanideBrush
                    Dim actinideBrush As Brush = New SolidColorBrush(Color.FromArgb(255, 107, 160, 52))
                    actinideBrush.Freeze()
                    brushesField("Actinide") = actinideBrush
                End If

                Return brushesField
            End Get
        End Property

        Protected Overrides Function CreateObject() As TreeMapDependencyObject
            Return New ChemicalElementColorizer()
        End Function

        Public Overrides Function GetItemBrush(ByVal item As ISunburstSectorInfo) As Brush
            Dim element As ChemicalElement = If(item.GroupInfo.IsParent, CType(CType(item.SourceObject, IList)(0), ChemicalElement), CType(item.SourceObject, ChemicalElement))
            Dim key As String = If(Equals(element.Block, "F-block") AndAlso item.GroupInfo.GroupLevel > 0, element.Family, element.Block)
            Return Brushes(key)
        End Function
    End Class
End Namespace
