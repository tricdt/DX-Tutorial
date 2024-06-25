Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo

    Public Partial Class pageImageGallery
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Public ReadOnly Property Images As IEnumerable
            Get
                Dim result = New List(Of BitmapImage)()
                For i As Integer = 1 To 10
                    result.Add(New BitmapImage(New Uri(String.Format(UriPrefix & "/Images/Photos/{0:D2}.jpg", i), UriKind.Relative)))
                Next

                Return result
            End Get
        End Property

        Private Sub layoutImagesItemsSizeChanged(ByVal sender As Object, ByVal e As ValueChangedEventArgs(Of Size))
            Dim size As Size = layoutImages.MaximizedElementOriginalSize
            If Not Double.IsInfinity(e.NewValue.Width) Then
                size.Height = Double.NaN
            Else
                size.Width = Double.NaN
            End If

            layoutImages.MaximizedElementOriginalSize = size
        End Sub
    End Class

    Public Class ImageContainer
        Inherits ContentControlBase

        Protected Overrides Sub OnMouseLeftButtonUp(ByVal e As MouseButtonEventArgs)
            MyBase.OnMouseLeftButtonUp(e)
            If Controller.IsMouseLeftButtonDown Then
                Dim layoutControl = TryCast(Parent, FlowLayoutControl)
                If layoutControl IsNot Nothing Then
                    Controller.IsMouseEntered = False
                    layoutControl.MaximizedElement = If(layoutControl.MaximizedElement Is Me, Nothing, Me)
                End If
            End If
        End Sub

        Protected Overrides Sub OnSizeChanged(ByVal e As SizeChangedEventArgs)
            MyBase.OnSizeChanged(e)
            If Not Double.IsNaN(Width) AndAlso Not Double.IsNaN(Height) Then
                If e.NewSize.Width <> e.PreviousSize.Width Then
                    Height = Double.NaN
                Else
                    Width = Double.NaN
                End If
            End If
        End Sub
    End Class
End Namespace
