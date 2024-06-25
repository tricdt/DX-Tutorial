Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports DevExpress.Mvvm

Namespace BarsDemo

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class BarButtonInfo

        Private action As System.Action

        Public Overridable Property Caption As String

        Public Overridable Property LargeGlyph As ImageSource

        Public Overridable Property SmallGlyph As ImageSource

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub ExecuteAction()
            If Me.action IsNot Nothing Then
                Me.action()
            End If
        End Sub

        Public Sub New(ByVal action As System.Action)
            Me.Caption = ""
            Me.action = action
        End Sub
    End Class

    Public Enum MyParentCommandType
        CommandCreation
        BarCreation
    End Enum

    Public Class ParentBarButtonInfo
        Inherits BarsDemo.BarButtonInfo

        Public Sub New(ByVal viewModel As BarsDemo.MVVMBarViewModel, ByVal type As BarsDemo.MyParentCommandType)
            MyBase.New(Sub() BarsDemo.ParentBarButtonInfo.Execute(type, viewModel))
        End Sub

        Private Shared Sub Execute(ByVal type As BarsDemo.MyParentCommandType, ByVal viewModel As BarsDemo.MVVMBarViewModel)
            Select Case type
                Case BarsDemo.MyParentCommandType.CommandCreation
                    viewModel.Bars(CInt((0))).Commands.Add(BarsDemo.ParentBarButtonInfo.CreateNewBarButtonInfo(viewModel))
                Case BarsDemo.MyParentCommandType.BarCreation
                    Dim model As BarsDemo.BarViewModel = DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New BarsDemo.BarViewModel() With {.Name = "New Bar"})
                    model.Commands.Add(BarsDemo.ParentBarButtonInfo.CreateNewBarButtonInfo(viewModel))
                    viewModel.Bars.Add(model)
            End Select
        End Sub

        Private Shared Function CreateNewBarButtonInfo(ByVal viewModel As BarsDemo.MVVMBarViewModel) As BarButtonInfo
            Dim caption As String = "New Command"
            Dim action As System.Action = Sub() viewModel.MessageBoxService.ShowMessage(System.[String].Format("Command ""{0}"" executed", caption))
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New BarsDemo.BarButtonInfo(action) With {.Caption = caption, .SmallGlyph = BarsDemo.DXImageHelper.GetDXImage("SvgImages/RichEdit/InsertImage.svg"), .LargeGlyph = BarsDemo.DXImageHelper.GetDXImage("SvgImages/RichEdit/InsertImage.svg")})
        End Function
    End Class

    Public Class GroupBarButtonInfo
        Inherits BarsDemo.BarButtonInfo

        Public Sub New()
            MyBase.New(Nothing)
            Me.Commands = New System.Collections.ObjectModel.ObservableCollection(Of BarsDemo.BarButtonInfo)()
        End Sub

        Public Overridable Property Commands As ObservableCollection(Of BarsDemo.BarButtonInfo)
    End Class
End Namespace
