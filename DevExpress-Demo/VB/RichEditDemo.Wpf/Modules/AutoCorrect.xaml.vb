Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.Services
Imports DevExpress.Office.Utils
Imports System.Reflection
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace RichEditDemo

    Public Partial Class AutoCorrect
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub RichEditControl_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim service As IAutoCorrectService = richEdit.GetService(Of IAutoCorrectService)()
            If service IsNot Nothing Then
                Dim replaceTable As AutoCorrectReplaceInfoCollection = New AutoCorrectReplaceInfoCollection()
                replaceTable.Add("(C)", "©")
                Dim assembly As Assembly = [GetType]().Assembly
                replaceTable.Add(New AutoCorrectReplaceInfo(":)", OfficeImage.CreateImage(assembly.GetManifestResourceStream(DemoHelper.GetPath("RichEditDemo.", assembly) & "smile.png"))))
                replaceTable.Add("pctus", "Please do not hesitate to contact us again in case of any further questions.")
                replaceTable.Add("wnwd", "well-nourished, well-developed")
                service.SetReplaceTable(replaceTable)
            End If
        End Sub
    End Class
End Namespace
