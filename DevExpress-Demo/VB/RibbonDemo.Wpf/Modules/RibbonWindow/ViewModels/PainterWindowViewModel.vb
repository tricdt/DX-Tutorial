Imports DevExpress.Xpf.Ribbon
Imports System.Collections.Generic
Imports System.Windows.Media

Namespace RibbonDemo

    Public Class PainterWindowViewModel

        Private _FontSizes As IEnumerable(Of Double?)

#Region "properties        "
        Public Overridable Property PageCategoryColor As Color

        Public Overridable Property PageCategoryAlignment As RibbonPageCategoryCaptionAlignment

        Public Overridable Property ToolbarShowMode As RibbonQuickAccessToolbarShowMode

        Public Overridable Property RibbonStyle As RibbonStyle

        Public Property FontSizes As IEnumerable(Of Double?)
            Get
                Return _FontSizes
            End Get

            Protected Set(ByVal value As IEnumerable(Of Double?))
                _FontSizes = value
            End Set
        End Property

#End Region
#Region "commands"
        Public Sub CloseWindow()
            CloseWindowService.Close()
        End Sub

#End Region
#Region "services"
        Public Overridable ReadOnly Property CloseWindowService As ICloseWindowService
            Get
                Return Nothing
            End Get
        End Property

#End Region
        Public Sub New()
            RibbonStyle = RibbonStyle.Office2010
            PageCategoryColor = Colors.Orange
            PageCategoryAlignment = RibbonPageCategoryCaptionAlignment.Right
            FontSizes = New Double?() {3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144}
        End Sub
    End Class
End Namespace
