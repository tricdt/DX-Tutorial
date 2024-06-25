Imports System

Namespace DockingDemo

    Public Partial Class LayoutGroups
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler SizeChanged, New Windows.SizeChangedEventHandler(AddressOf Me.LayoutGroups_SizeChanged)
        End Sub

        Private Sub LayoutGroups_SizeChanged(ByVal sender As Object, ByVal e As Windows.SizeChangedEventArgs)
            If e.NewSize.Height < 500 Then
                layoutGroup2.Visibility = Windows.Visibility.Collapsed
                layoutGroup3.Visibility = Windows.Visibility.Collapsed
            Else
                layoutGroup2.Visibility = Windows.Visibility.Visible
                layoutGroup3.Visibility = Windows.Visibility.Visible
            End If
        End Sub
    End Class
End Namespace
