Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows

Namespace RibbonDemo

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
End Namespace
