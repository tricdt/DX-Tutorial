Imports DevExpress.Xpf.DemoBase
Imports System.Windows
Imports System.Windows.Controls

Namespace PivotGridDemo

    <CodeFile("ViewModels/MVVMPivotViewModel.(cs)")>
    Public Partial Class MVVMPivot
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

#Region "TemplateSelector"
    Public Class FormatConditionGeneratorTemplateSelector
        Inherits DataTemplateSelector

        Public Property DataBarTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim formatConditionItem = TryCast(item, FormatConditionItem)
            If formatConditionItem IsNot Nothing Then
                Select Case formatConditionItem.Type
                    Case FormatConditionType.DataBar
                        Return DataBarTemplate
                End Select
            End If

            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class
#End Region
End Namespace
