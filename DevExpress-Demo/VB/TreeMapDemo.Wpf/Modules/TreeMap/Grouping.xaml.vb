Imports System.Collections.Generic
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.TreeMap
Imports System.Xml
Imports DevExpress.Xpf.DemoBase
Imports System.Windows

Namespace TreeMapDemo

    <CodeFile("Modules/TreeMap/Grouping.xaml")>
    <CodeFile("Modules/TreeMap/Grouping.xaml.(cs)")>
    <NoAutogeneratedCodeFiles>
    Public Partial Class Grouping
        Inherits TreeMapDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    <POCOViewModel>
    Public Class GroupingViewModel
        Inherits ViewModelBase

        Private Shared instanceField As GroupingViewModel

        Public Shared ReadOnly Property Instance As GroupingViewModel
            Get
                If instanceField Is Nothing Then instanceField = ViewModelSource.Create(Function() New GroupingViewModel())
                Return instanceField
            End Get
        End Property

        Public Overridable Property BillionairesInfos As XmlDocument

        Public Overridable Property ValueMembers As List(Of String)

        Public Overridable Property GroupDefinitionSettings As List(Of GroupDefinitionSettings)

        Public Overridable Property GroupDefinition As GroupDefinitionSettings

        Protected Sub New()
            BillionairesInfos = LoadXmlDocumentFromResources("/Data/Billionares.xml")
            GroupDefinitionSettings = GetGroupDefinitionSettings()
            GroupDefinition = GroupDefinitionSettings(1)
        End Sub

        Private Function GetGroupDefinitionSettings() As List(Of GroupDefinitionSettings)
            Dim GroupByResidence As GroupDefinitionCollection = New GroupDefinitionCollection()
            GroupByResidence.Add(New TreeMapGroupDefinition() With {.GroupDataMember = "Residence"})
            Dim GroupByAgeCategory As GroupDefinitionCollection = New GroupDefinitionCollection()
            GroupByAgeCategory.Add(New TreeMapGroupDefinition() With {.GroupDataMember = "AgeCategory"})
            Dim GroupByResidenceAndAgeCategory As GroupDefinitionCollection = New GroupDefinitionCollection()
            GroupByResidenceAndAgeCategory.Add(New TreeMapGroupDefinition() With {.GroupDataMember = "Residence"})
            GroupByResidenceAndAgeCategory.Add(New TreeMapGroupDefinition() With {.GroupDataMember = "AgeCategory"})
            Return New List(Of GroupDefinitionSettings)() From {New GroupDefinitionSettings() With {.CollectionGroupDefinitionName = "Without Grouping", .ColorizeGroups = False, .CollectionGroupDefinition = New GroupDefinitionCollection()}, New GroupDefinitionSettings() With {.CollectionGroupDefinitionName = "Group By Residence", .ColorizeGroups = True, .CollectionGroupDefinition = GroupByResidence, .LegendVisibility = Visibility.Visible, .LegendTitle = "Net Worth by Residence," & Microsoft.VisualBasic.Constants.vbLf & "in billions of dollars"}, New GroupDefinitionSettings() With {.CollectionGroupDefinitionName = "Group By Age Category", .ColorizeGroups = True, .CollectionGroupDefinition = GroupByAgeCategory, .LegendVisibility = Visibility.Visible, .LegendTitle = "Net Worth by Age Category," & Microsoft.VisualBasic.Constants.vbLf & "in billions of dollars"}, New GroupDefinitionSettings() With {.CollectionGroupDefinitionName = "Group By Residence And Age Category", .ColorizeGroups = True, .CollectionGroupDefinition = GroupByResidenceAndAgeCategory, .LegendVisibility = Visibility.Collapsed}}
        End Function
    End Class

    Public Class GroupDefinitionSettings

        Public Property CollectionGroupDefinition As GroupDefinitionCollection

        Public Property CollectionGroupDefinitionName As String

        Public Property ColorizeGroups As Boolean

        Public Property LegendVisibility As Visibility

        Public Property LegendTitle As String

        Public Overrides Function ToString() As String
            Return CollectionGroupDefinitionName
        End Function
    End Class
End Namespace
