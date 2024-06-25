Imports DevExpress.Export
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.PdfViewer
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.Xpf.Spreadsheet
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Localization
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraPrinting.Native.ExportOptionsControllers
Imports Microsoft.Win32
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Threading
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Xpf.PivotGrid.Printing

Namespace PivotGridDemo

    Public Module DemoModuleWYSIWYGExportHelper

        Public Sub DoExport(ByVal pivot As PivotGridControl, ByVal format As ExportFormat)
            Select Case format
                Case ExportFormat.Pdf, ExportFormat.Rtf, ExportFormat.Docx
                    pivot.PrintLayoutMode = PrintLayoutMode.MultiplePagesLayout
                Case Else
                    pivot.PrintLayoutMode = PrintLayoutMode.SinglePageLayout
            End Select

            Select Case format
                Case ExportFormat.Xls
                    Export(pivot, Function(link) New Action(Of Stream, XlsExportOptions)(AddressOf link.ExportToXls))
                Case ExportFormat.Xlsx
                    Export(pivot, Function(link) New Action(Of Stream, XlsxExportOptions)(AddressOf link.ExportToXlsx))
                Case ExportFormat.Pdf
                    Export(pivot, Function(link) New Action(Of Stream, PdfExportOptions)(AddressOf link.ExportToPdf))
                Case ExportFormat.Htm
                    Export(pivot, Function(link) New Action(Of Stream, HtmlExportOptions)(AddressOf link.ExportToHtml))
                Case ExportFormat.Mht
                    Export(pivot, Function(link) New Action(Of Stream, MhtExportOptions)(AddressOf link.ExportToMht))
                Case ExportFormat.Rtf
                    Export(pivot, Function(link) New Action(Of Stream, RtfExportOptions)(AddressOf link.ExportToRtf))
                Case ExportFormat.Txt
                    Export(pivot, Function(link) New Action(Of Stream, TextExportOptions)(AddressOf link.ExportToText))
                Case ExportFormat.Image
                    Export(pivot, Function(link) New Action(Of Stream, ImageExportOptions)(AddressOf link.ExportToImage))
                Case ExportFormat.Xps
                    Export(pivot, Function(link) New Action(Of Stream, XpsExportOptions)(AddressOf link.ExportToXps))
                Case ExportFormat.Docx
                    Export(pivot, Function(link) New Action(Of Stream, DocxExportOptions)(AddressOf link.ExportToDocx))
            End Select
        End Sub

        Private Sub OnAfterBuildPages(ByVal sender As Object, ByVal e As EventArgs)
            Call DXSplashScreen.Close()
        End Sub

        Private Sub UnsubscribeProgressEvents(ByVal link As PrintableControlLink, ByVal onExportProgress As EventHandler)
            RemoveHandler link.PrintingSystem.ProgressReflector.PositionChanged, onExportProgress
            RemoveHandler link.PrintingSystem.AfterBuildPages, AddressOf OnAfterBuildPages
        End Sub

        Private Sub SubscribeProgressEvents(ByVal link As PrintableControlLink, ByVal onExportProgress As EventHandler)
            AddHandler link.PrintingSystem.ProgressReflector.PositionChanged, onExportProgress
            AddHandler link.PrintingSystem.AfterBuildPages, AddressOf OnAfterBuildPages
        End Sub

        Private Sub Export(Of T As {ExportOptionsBase, New})(ByVal pivot As PivotGridControl, ByVal getExportToStreamMethod As Func(Of PrintableControlLink, Action(Of Stream, T)))
            Dim link = New PrintableControlLink(pivot)
            Dim onExportProgress As EventHandler = Sub(o, e) ExportProgress(New ProgressChangedEventArgs(link.PrintingSystem.ProgressReflector.Position, Nothing))
            Using stream As MemoryStream = New MemoryStream()
                ExportCore(getExportToStreamMethod(link), stream, Sub(options) SubscribeProgressEvents(link, onExportProgress), Sub(options) UnsubscribeProgressEvents(link, onExportProgress))
            End Using
        End Sub
    End Module

    Public Module DemoModuleExportHelper

        Public Sub ExportToXlsx(ByVal pivot As PivotGridControl)
            Export(New Action(Of Stream, XlsxExportOptionsEx)(AddressOf pivot.ExportToXlsx))
        End Sub

        Public Sub ExportToXls(ByVal pivot As PivotGridControl)
            Export(New Action(Of Stream, XlsExportOptionsEx)(AddressOf pivot.ExportToXls))
        End Sub

        Public Sub ExportToCsv(ByVal pivot As PivotGridControl)
            Export(New Action(Of Stream, CsvExportOptionsEx)(AddressOf pivot.ExportToCsv))
        End Sub

        Private Sub Export(Of T As {ExportOptionsBase, New})(ByVal exportToStreamMethod As Action(Of Stream, T))
            Call Dispatcher.CurrentDispatcher.BeginInvoke(New Action(Of Action(Of Stream, T))(AddressOf ExportCore), DispatcherPriority.ContextIdle, exportToStreamMethod)
        End Sub

        Private Sub ExportCore(Of T As {ExportOptionsBase, New})(ByVal exportToStreamMethod As Action(Of Stream, T))
            Using stream As MemoryStream = New MemoryStream()
                ExportHelper.ExportCore(exportToStreamMethod, stream, New Action(Of T)(AddressOf SubscribeProgressEvents), New Action(Of T)(AddressOf UnsubscribeProgressEvents))
            End Using
        End Sub

        Private Sub UnsubscribeProgressEvents(Of T As ExportOptionsBase)(ByVal options As T)
            RemoveHandler CType(options, IDataAwareExportOptions).ExportProgress, AddressOf ExportProgress
        End Sub

        Private Sub SubscribeProgressEvents(Of T As ExportOptionsBase)(ByVal options As T)
            AddHandler CType(options, IDataAwareExportOptions).ExportProgress, AddressOf ExportProgress
        End Sub
    End Module

    Friend Module ExportHelper

        Const OpenInInternalViewer As String = "Open with DevExpress Control"

        Const OpenInExternalViewer As String = "Open with Default App"

        Const OpenExportedFile As String = "Open Exported File"

        Const OpenExportedFileDescription As String = "You can view the exported file in your system default" & Microsoft.VisualBasic.Constants.vbLf & "application or in a DevExpress WPF {0} control."

        Private InternalViewerDisplayTexts As Dictionary(Of ViewerType, String) = New Dictionary(Of ViewerType, String)() From {{ViewerType.Spreadsheet, "Spreadsheet"}, {ViewerType.RichEdit, "RichEdit"}, {ViewerType.PDFViewer, "PDF Viewer"}}

        Public Sub ExportCore(Of T As {ExportOptionsBase, New})(ByVal exportToStream As Action(Of Stream, T), ByVal stream As Stream, ByVal subscribeProgress As Action(Of T), ByVal unsubscribeProgress As Action(Of T))
            If stream Is Nothing Then Return
            Try
                Call DXSplashScreen.Show(Of ExportWaitIndicator)()
                Dim options = New T()
                subscribeProgress(options)
                Try
                    exportToStream(stream, options)
                Finally
                    unsubscribeProgress(options)
                    If DXSplashScreen.IsActive Then Call DXSplashScreen.Close()
                End Try

                stream.Seek(0, SeekOrigin.Begin)
                Dim viewerType As ViewerType = GetInternalViewerType(options, IsLargeFile(stream))
                If viewerType = ViewerType.External Then
                    If ShouldOpenExportedFile() Then OpenExternalViewer(stream, options)
                    Return
                End If

                Select Case GetExportType(viewerType)
                    Case ViewerType.External
                        OpenExternalViewer(stream, options)
                        Return
                    Case ViewerType.None
                        Return
                    Case Else
                        OpenInternalViewer(stream, options, viewerType)
                        Return
                End Select
            Catch e As Exception
                DXMessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End Sub

        Private Function GetExportType(ByVal viewerType As ViewerType) As ViewerType
            Dim dialogCommands As List(Of UICommand) = New List(Of UICommand)()
            Dim result As ViewerType = ViewerType.None
            dialogCommands.Add(New UICommand(0, OpenInInternalViewer, DelegateCommandFactory.Create(Sub() result = viewerType), True, False))
            dialogCommands.Add(New UICommand(1, OpenInExternalViewer, DelegateCommandFactory.Create(Sub() result = ViewerType.External), True, False))
            Dim d As DXDialogWindow = New DXDialogWindow() With {.ResizeMode = ResizeMode.NoResize, .SizeToContent = SizeToContent.WidthAndHeight, .WindowStyle = WindowStyle.SingleBorderWindow, .ShowInTaskbar = False, .WindowStartupLocation = WindowStartupLocation.CenterOwner, .Owner = Application.Current.MainWindow, .Title = OpenExportedFile}
            d.CommandsSource = dialogCommands
            d.Content = New TextBlock() With {.Text = String.Format(OpenExportedFileDescription, InternalViewerDisplayTexts(viewerType)), .Margin = New Thickness(24, 15, 24, 16), .HorizontalAlignment = HorizontalAlignment.Center}
            d.ShowDialogWindow()
            Return result
        End Function

        Private Function IsLargeFile(ByVal stream As Stream) As Boolean
            Return stream.Length > CLng(300000)
        End Function

        Private Sub OpenExternalViewer(Of T As ExportOptionsBase)(ByVal stream As Stream, ByVal options As T)
            Dim fullFilePath As String = SaveToFile(stream, options)
            ProcessLaunchHelper.StartConfirmed(fullFilePath)
        End Sub

        Private Function GetInternalViewerType(ByVal options As ExportOptionsBase, ByVal isLargeFile As Boolean) As ViewerType
            Select Case options.GetFormat()
                Case ExportFormat.Csv, ExportFormat.Xls, ExportFormat.Xlsx
                    Return If(isLargeFile, ViewerType.External, ViewerType.Spreadsheet)
                Case ExportFormat.Rtf
                    Return If(isLargeFile, ViewerType.External, ViewerType.RichEdit)
                Case ExportFormat.Pdf
                    Return ViewerType.PDFViewer
                Case Else
                    Return ViewerType.External
            End Select
        End Function

        Private Sub OpenInternalViewer(Of T As ExportOptionsBase)(ByVal stream As Stream, ByVal options As T, ByVal vieweType As ViewerType)
            Select Case vieweType
                Case ViewerType.Spreadsheet
                    OpenInternalViewerWindow(New Action(Of SpreadsheetControl, Stream, T)(AddressOf SpreadSheetLoadDocument), Function() New SpreadsheetControl(), stream, options)
                    Return
                Case ViewerType.PDFViewer
                    OpenInternalViewerWindow(New Action(Of PdfViewerControl, Stream, T)(AddressOf PdfViewerLoadDocument), Function() New PdfViewerControl(), stream, options)
                    Return
                Case ViewerType.RichEdit
                    OpenInternalViewerWindow(New Action(Of RichEditControl, Stream, T)(AddressOf RichEditLoadDocument), Function() New RichEditControl(), stream, options)
                    Return
            End Select
        End Sub

        Private Sub OpenInternalViewerWindow(Of T1 As ExportOptionsBase, T2 As FrameworkElement)(ByVal loadDocumentAction As Action(Of T2, Stream, T1), ByVal createViewer As Func(Of T2), ByVal stream As Stream, ByVal options As T1)
            Call DXSplashScreen.Show(Of OpenViewerWaitIndicator)()
            Dim viewerWindow = New ThemedWindow() With {.Title = "Document", .Owner = Application.Current.MainWindow, .WindowStartupLocation = WindowStartupLocation.CenterOwner}
            Dim viewerControl As T2 = createViewer()
            viewerWindow.Content = viewerControl
            loadDocumentAction(viewerControl, stream, options)
            AddHandler viewerWindow.Loaded, AddressOf ViewerWindow_Loaded1
            viewerWindow.ShowDialog()
        End Sub

        Private Function GetSpreadsheetDocumentFormat(ByVal options As ExportOptionsBase) As DevExpress.Spreadsheet.DocumentFormat
            Select Case options.GetFormat()
                Case ExportFormat.Csv
                    Return DevExpress.Spreadsheet.DocumentFormat.Csv
                Case ExportFormat.Xls
                    Return DevExpress.Spreadsheet.DocumentFormat.Xls
                Case ExportFormat.Xlsx
                    Return DevExpress.Spreadsheet.DocumentFormat.Xlsx
                Case Else
                    Return DevExpress.Spreadsheet.DocumentFormat.Undefined
            End Select
        End Function

        Private Function GetRichEditDocumentFormat(ByVal options As ExportOptionsBase) As DevExpress.XtraRichEdit.DocumentFormat
            Select Case options.GetFormat()
                Case ExportFormat.Rtf
                    Return DevExpress.XtraRichEdit.DocumentFormat.Rtf
                Case ExportFormat.Htm
                    Return DevExpress.XtraRichEdit.DocumentFormat.Html
                Case ExportFormat.Mht
                    Return DevExpress.XtraRichEdit.DocumentFormat.Mht
                Case Else
                    Return DevExpress.XtraRichEdit.DocumentFormat.Undefined
            End Select
        End Function

        Private Sub ViewerWindow_Loaded1(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim element As FrameworkElement = CType(sender, FrameworkElement)
            RemoveHandler element.Loaded, AddressOf ViewerWindow_Loaded1
            If DXSplashScreen.IsActive Then Call DXSplashScreen.Close()
        End Sub

        Private Sub RichEditLoadDocument(Of T As ExportOptionsBase)(ByVal richEditControl As RichEditControl, ByVal stream As Stream, ByVal options As T)
            richEditControl.CommandBarStyle = DevExpress.Xpf.RichEdit.CommandBarStyle.Ribbon
            richEditControl.LoadDocument(stream, GetRichEditDocumentFormat(options))
        End Sub

        Private Sub SpreadSheetLoadDocument(Of T As ExportOptionsBase)(ByVal spreadSheetControl As SpreadsheetControl, ByVal stream As Stream, ByVal options As T)
            spreadSheetControl.CommandBarStyle = DevExpress.Xpf.Spreadsheet.CommandBarStyle.Ribbon
            spreadSheetControl.LoadDocument(stream, GetSpreadsheetDocumentFormat(options))
        End Sub

        Private Sub PdfViewerLoadDocument(Of T As ExportOptionsBase)(ByVal pdfViewerControl As PdfViewerControl, ByVal stream As Stream, ByVal options As T)
            pdfViewerControl.DocumentSource = stream
        End Sub

        Private Function SaveToFile(Of T As ExportOptionsBase)(ByVal stream As Stream, ByVal options As T) As String
            Dim tempFileName As String = Path.ChangeExtension(Path.GetTempFileName(), options.GetFileExtension())
            Using fileStream As FileStream = New FileStream(tempFileName, FileMode.Create)
                stream.CopyTo(fileStream)
            End Using

            Return tempFileName
        End Function

        Public Sub ExportProgress(ByVal ea As ProgressChangedEventArgs)
            If Not DXSplashScreen.IsActive Then Return
            DXSplashScreen.Progress(ea.ProgressPercentage)
        End Sub

        Public Function GetFileName(ByVal options As ExportOptionsBase) As String
            Return GetFileName(ExportOptionsControllerBase.GetControllerByOptions(options))
        End Function

        Private Function GetFileName(ByVal controller As ExportOptionsControllerBase) As String
            Dim dlg As SaveFileDialog = CreateSaveFileDialog(controller)
            If dlg.ShowDialog() = True AndAlso Not String.IsNullOrEmpty(dlg.FileName) Then
                Return PathExtensionHelper.GetValidExtension(dlg.FileName, controller.FileExtensions(0), controller.FileExtensions)
            Else
                Return String.Empty
            End If
        End Function

        Private Function CreateSaveFileDialog(ByVal controller As ExportOptionsControllerBase) As SaveFileDialog
            Dim dlg As SaveFileDialog = New SaveFileDialog()
            dlg.Title = PreviewLocalizer.GetString(PreviewStringId.SaveDlg_Title)
            dlg.ValidateNames = True
            dlg.FileName = PrintPreviewOptions.DefaultFileNameDefault
            dlg.Filter = controller.Filter
            Return dlg
        End Function

        Private Function ShouldOpenExportedFile() As Boolean
            Return DXMessageBox.Show(PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestion), PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestionCaption), MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes
        End Function
    End Module

    Public Class PrintingIconImageSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim rawIconName As String = TryCast(value, String)
            If Equals(rawIconName, Nothing) Then Return Nothing
            Dim iconName As String = Regex.Replace(rawIconName, "[^a-zA-Z]", String.Empty)
            Dim iconPath As String = "Images/BarItems/" & iconName & "_32x32.png"
            Return New PrintingResourceImageExtension() With {.ResourceName = iconPath}.ProvideValue(Nothing)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class InternalViewerViewModel

        Private _Stream As Stream, _Options As ExportOptionsBase

        Public Sub New(ByVal stream As Stream, ByVal options As ExportOptionsBase)
            Me.Stream = stream
            Me.Options = options
        End Sub

        Public Property Stream As Stream
            Get
                Return _Stream
            End Get

            Private Set(ByVal value As Stream)
                _Stream = value
            End Set
        End Property

        Public Property Options As ExportOptionsBase
            Get
                Return _Options
            End Get

            Private Set(ByVal value As ExportOptionsBase)
                _Options = value
            End Set
        End Property

        Public Sub StopSplashScreen()
            If DXSplashScreen.IsActive Then Call DXSplashScreen.Close()
        End Sub
    End Class

    Public Enum ViewerType
        None
        External
        Spreadsheet
        RichEdit
        PDFViewer
    End Enum
End Namespace
