Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Namespace DockingDemo.ViewModels

    Public Class RibbonMergingViewModel

        Public Sub New()
            Panels = New ObservableCollection(Of PanelViewModel)()
            Panels.Add(TextPanelViewModel.Create("Simple Pad", New Point(0.0, 0.0), Me))
            Panels.Add(PaintPanelViewModel.Create("Simple Paint", New Point(300.0, 50.0), Me))
        End Sub

        Public Overridable Property Panels As IList(Of PanelViewModel)

        Public Sub CreateNewTextPanel()
            Panels.Add(TextPanelViewModel.Create("Simple Pad", New Point(50, 50), Me))
            Panels(Panels.Count - 1).IsActive = True
        End Sub

        Public Sub CreateNewPaintPanel()
            Panels.Add(PaintPanelViewModel.Create("Simple Paint", New Point(70, 60), Me))
            Panels(Panels.Count - 1).IsActive = True
        End Sub
    End Class

    <POCOViewModel>
    Public Class TextPanelViewModel
        Inherits PanelViewModel

        Protected Sub New()
        End Sub

        Public Shared Function Create(ByVal caption As String, ByVal location As Point, ByVal parentViewModel As RibbonMergingViewModel) As TextPanelViewModel
            Dim instance As TextPanelViewModel = ViewModelSource.Create(Function() New TextPanelViewModel())
            instance.ParentViewModel = parentViewModel
            instance.Caption = caption
            instance.Location = location
            Return instance
        End Function
    End Class

    <POCOViewModel>
    Public Class PaintPanelViewModel
        Inherits PanelViewModel

        Protected Sub New()
        End Sub

        Public Shared Function Create(ByVal caption As String, ByVal location As Point, ByVal parentViewModel As RibbonMergingViewModel) As PaintPanelViewModel
            Dim instance As PaintPanelViewModel = ViewModelSource.Create(Function() New PaintPanelViewModel())
            instance.Caption = caption
            instance.Location = location
            instance.ParentViewModel = parentViewModel
            Return instance
        End Function
    End Class

    Public MustInherit Class PanelViewModel

        Private _FontSizes As IEnumerable(Of Double?)

        Protected Property ParentViewModel As RibbonMergingViewModel

        Public Overridable Property Caption As String

        Public Overridable Property Location As Point

        Public Overridable Property IsActive As Boolean

        Public Property FontSizes As IEnumerable(Of Double?)
            Get
                Return _FontSizes
            End Get

            Protected Set(ByVal value As IEnumerable(Of Double?))
                _FontSizes = value
            End Set
        End Property

        Public Overridable Sub Close(ByVal param As Object)
            ParentViewModel.Panels.Remove(Me)
            If ParentViewModel.Panels.Count <> 0 Then ParentViewModel.Panels(0).IsActive = True
        End Sub

        Public Sub New()
            FontSizes = New Double?() {3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 44, 48, 52, 56, 60, 64, 68, 72, 76, 80, 88, 96, 104, 112, 120, 128, 136, 144}
        End Sub
    End Class
End Namespace
