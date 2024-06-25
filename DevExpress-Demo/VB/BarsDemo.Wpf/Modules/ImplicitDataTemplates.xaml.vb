Imports DevExpress.Xpf.DemoBase
Imports System.Collections.Generic

Namespace BarsDemo

    <CodeFile("ViewModels/ImplicitDataTemplatesModel.(cs)")>
    Public Partial Class ImplicitDataTemplates
        Inherits BarsDemoModule

        Public Sub New()
            InitializeComponent()
            DataContext = New List(Of CommandModel) From {New GroupModel() With {.Caption = "File", .Glyph = "SvgImages/Icon Builder/Actions_Home.svg", .Commands = New List(Of CommandModel)() From {New CommandModel() With {.Caption = "New", .Glyph = "SvgImages/Outlook Inspired/New.svg"}, New CommandModel() With {.Caption = "Open", .Glyph = "SvgImages/Outlook Inspired/Open.svg"}, New CommandModel() With {.Caption = "Save", .Glyph = "SvgImages/Outlook Inspired/Save.svg"}}}, New CommandModel() With {.Caption = "Settings", .Glyph = "SvgImages/Scheduling/ViewSettings.svg"}, New EditorModel() With {.Caption = "Search:", .Glyph = "SvgImages/Outlook Inspired/Find.svg"}, New LabelModel() With {.Content = Date.Now}}
        End Sub
    End Class
End Namespace
