Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Pdf
Imports DevExpress.Xpf.PdfViewer
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows

Namespace DevExpress.DevAV.Common.View

    Public Class WatermarkBehavior
        Inherits Behavior(Of PdfViewerControl)

        Private ReadOnly Shared watermarkFormat As PdfStringFormat

        Public Shared ReadOnly DocumentSourceProperty As DependencyProperty = DependencyProperty.Register("DocumentSource", GetType(Stream), GetType(WatermarkBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, WatermarkBehavior).UpdateDocumnt()))

        Public Shared ReadOnly TextProperty As DependencyProperty = DependencyProperty.Register("Text", GetType(String), GetType(WatermarkBehavior), New PropertyMetadata(String.Empty, Sub(d, e) CType(d, WatermarkBehavior).UpdateDocumnt()))

        Public Property DocumentSource As Stream
            Get
                Return CType(GetValue(DocumentSourceProperty), Stream)
            End Get

            Set(ByVal value As Stream)
                SetValue(DocumentSourceProperty, value)
            End Set
        End Property

        Public Property Text As String
            Get
                Return CStr(GetValue(TextProperty))
            End Get

            Set(ByVal value As String)
                SetValue(TextProperty, value)
            End Set
        End Property

        Shared Sub New()
            watermarkFormat = New PdfStringFormat()
            watermarkFormat.FormatFlags = PdfStringFormatFlags.NoWrap Or PdfStringFormatFlags.NoClip
            watermarkFormat.Alignment = PdfStringAlignment.Center
            watermarkFormat.LineAlignment = PdfStringAlignment.Center
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            UpdateDocumnt()
        End Sub

        Private Sub UpdateDocumnt()
            If AssociatedObject Is Nothing OrElse String.IsNullOrEmpty(Text) OrElse DocumentSource Is Nothing Then
                AssociatedObject.DocumentSource = Nothing
                Return
            End If

            Using processor As PdfDocumentProcessor = New PdfDocumentProcessor()
                processor.LoadDocument(DocumentSource)
                AddWatermark(processor, Text)
                Dim tmpFile = Path.GetTempFileName()
                processor.SaveDocument(tmpFile)
                AssociatedObject.OpenDocument(tmpFile)
            End Using
        End Sub

        Private Shared Sub AddWatermark(ByVal processor As PdfDocumentProcessor, ByVal watermark As String)
            Dim pages = processor.Document.Pages
            For i As Integer = 0 To pages.Count - 1
                Using graphics = processor.CreateGraphics()
                    Using font As Font = New Font("Segoe UI", 48, System.Drawing.FontStyle.Regular)
                        Dim pageLayout As RectangleF = New RectangleF(-CSng(pages(i).CropBox.Width) * 0.35F, CSng(pages(i).CropBox.Height) * 0.1F, CSng(pages(i).CropBox.Width) * 1.25F, CSng(pages(i).CropBox.Height))
                        Dim angle = Math.Asin(CDbl(pageLayout.Width) / CDbl(pageLayout.Height)) * 180.0 / Math.PI
                        graphics.TranslateTransform(-pageLayout.X, -pageLayout.Y)
                        graphics.RotateTransform(CSng(angle))
                        Using textBrush As SolidBrush = New SolidBrush(Color.FromArgb(100, Color.Red))
                            graphics.DrawString(watermark, font, textBrush, New PointF(50, 50))
                        End Using
                    End Using

                    graphics.AddToPageForeground(pages(i))
                End Using
            Next
        End Sub
    End Class
End Namespace
