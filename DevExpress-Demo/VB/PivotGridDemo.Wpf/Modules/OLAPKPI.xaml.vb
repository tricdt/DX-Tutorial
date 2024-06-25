Imports System.IO
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Partial Class OLAPKPI
        Inherits PivotGridDemoModule

        Private Shared sampleFileNameField As String

        Public Sub New()
            InitializeComponent()
            If UseXmlaConnection Then pivotGrid.OlapDataProvider = OlapDataProvider.Xmla
            pivotGrid.SetOlapConnectionStringAsync("Provider=msolap;Data Source=" & SampleFileName & ";Initial Catalog=" & SampleCatalogName & ";Cube Name=Adventure Works")
        End Sub

        Private Shared ReadOnly Property SampleFileName As String
            Get
                If UseXmlaConnection Then Return "http://demos.devexpress.com/Services/OLAP/msmdpump.dll"
                If String.IsNullOrEmpty(sampleFileNameField) Then
                    sampleFileNameField = Path.GetFullPath(DataFilesHelper.FindFile("AdventureWorks.cub", DataFilesHelper.DataPath))
                    If File.Exists(sampleFileNameField) Then
                        Try
                            File.SetAttributes(sampleFileNameField, FileAttributes.Normal)
                        Catch
                        End Try
                    End If
                End If

                Return sampleFileNameField
            End Get
        End Property

        Private Shared ReadOnly Property SampleCatalogName As String
            Get
                If UseXmlaConnection Then Return "Adventure Works DW Standard Edition"
                Return "Adventure Works"
            End Get
        End Property

        Private Shared ReadOnly Property UseXmlaConnection As Boolean
            Get
                Return Not DevExpress.XtraPivotGrid.Data.OLAPMetaGetter.IsProviderAvailable
            End Get
        End Property
    End Class
End Namespace
