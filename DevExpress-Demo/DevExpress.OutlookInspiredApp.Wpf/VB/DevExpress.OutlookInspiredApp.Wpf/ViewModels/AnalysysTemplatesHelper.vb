Imports System
Imports System.IO
Imports DevExpress.Utils

Namespace DevExpress.DevAV.ViewModels

    Public Enum AnalysisTemplate
        CustomerSales
        ProductSales
    End Enum

    Public Module AnalysisTemplatesHelper

        Public Function GetAnalysisTemplate(ByVal template As AnalysisTemplate) As Stream
            Select Case template
                Case AnalysisTemplate.CustomerSales
                    Return GetTemplateStream("CustomerAnalysis.xlsx")
                Case AnalysisTemplate.ProductSales
                    Return GetTemplateStream("ProductAnalysis.xlsx")
                Case Else
                    Throw New NotSupportedException(template.ToString())
            End Select
        End Function

        Private Function GetTemplateStream(ByVal templateName As String) As Stream
            Return AssemblyHelper.GetEmbeddedResourceStream(GetType(AnalysisTemplatesHelper).Assembly, templateName, False)
        End Function
    End Module
End Namespace
