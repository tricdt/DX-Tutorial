Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq

Namespace GridDemo

    <POCOViewModel>
    Public Class CardViewExplorerViewModel

        Private _Source As ObservableCollectionCore(Of GridDemo.CardViewExplorerFileSource)

#Region "Properties"
        Const Root As String = "Root"

        Private ForwardStack As Stack(Of String) = New Stack(Of String)()

        Private BackStack As Stack(Of String) = New Stack(Of String)()

        Public Overridable Property SizeType As SizeIcon

        Protected Sub OnSizeTypeChanged()
            Resize()
            RaisePropertyChanged(Function(x) x.Size)
        End Sub

        Public ReadOnly Property Size As Integer
            Get
                Select Case SizeType
                    Case SizeIcon.ExtraLarge
                        Return 256
                    Case SizeIcon.Large
                        Return 128
                    Case SizeIcon.Medium
                        Return 65
                    Case Else
                        Return 128
                End Select
            End Get
        End Property

        Private pathField As String

        Public Property Path As String
            Get
                Return pathField
            End Get

            Set(ByVal value As String)
                If Not Equals(value, Root) AndAlso Not IO.Directory.Exists(value) Then
                    RaisePathChanged()
                    Return
                End If

                If Not Equals(pathField, Nothing) Then ForwardStack.Push(pathField)
                pathField = value
                OpenFolder(value)
                RaisePathChanged()
            End Set
        End Property

        Public Overridable Property IsLoading As Boolean

        Public Overridable Property SearchText As String

        Public Property Source As ObservableCollectionCore(Of CardViewExplorerFileSource)
            Get
                Return _Source
            End Get

            Private Set(ByVal value As ObservableCollectionCore(Of CardViewExplorerFileSource))
                _Source = value
            End Set
        End Property

        Public Property CurrentItem As CardViewExplorerFileSource

#End Region
#Region "POCO commands"
        Public Sub Back()
            BackStack.Push(Path)
            Dim tmp As String = ForwardStack.Pop()
            pathField = tmp
            RaisePathChanged()
            OpenFolder(tmp, False)
        End Sub

        Public Function CanBack() As Boolean
            Return ForwardStack.Count > 0
        End Function

        Public Sub Forward()
            Dim tmp As String = BackStack.Pop()
            ForwardStack.Push(pathField)
            pathField = tmp
            RaisePathChanged()
            OpenFolder(tmp, False)
        End Sub

        Public Function CanForward() As Boolean
            Return BackStack.Count > 0
        End Function

        Public Sub Open()
            Dim element As CardViewExplorerFileSource = CurrentItem
            Path = element.FullPath
        End Sub

        Public Function CanOpen() As Boolean
            Return CurrentItem IsNot Nothing AndAlso CurrentItem.Type <> CardViewExplorerFileSource.TypeElement.File
        End Function

        Public Sub Up()
            If Path.Length <> 3 Then
                Path = IO.Directory.GetParent(Path).FullName
            Else
                Path = Root
            End If
        End Sub

        Public Function CanUp() As Boolean
            Return Not Equals(Path, Root)
        End Function

#End Region
#Region "Members"
        Public Sub New()
            Source = New ObservableCollectionCore(Of CardViewExplorerFileSource)()
            SizeType = SizeIcon.Medium
            OpenRoot()
        End Sub

        Private Sub OpenFolder(ByVal path As String, ByVal Optional clearNextStack As Boolean = True)
            Source.Clear()
            Source.BeginUpdate()
            Try
                IsLoading = True
                If Equals(path, Root) Then
                    OpenRoot()
                Else
                    Dim sizeType As SizeIcon = Me.SizeType
                    Dim size As Integer = Me.Size
                    For Each item In IO.Directory.EnumerateDirectories(path)
                        Source.Add(CardViewExplorerFileSource.Create(IO.Path.Combine(path, item), CardViewExplorerFileSource.TypeElement.Folder, sizeType, size))
                    Next

                    For Each item In IO.Directory.EnumerateFiles(path)
                        Source.Add(CardViewExplorerFileSource.Create(item, CardViewExplorerFileSource.TypeElement.File, sizeType, size))
                    Next
                End If

                If clearNextStack Then BackStack.Clear()
            Catch ex As UnauthorizedAccessException
                Windows.MessageBox.Show(ex.Message, "Error", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error)
                Back()
            Finally
                IsLoading = False
                Source.EndUpdate()
            End Try
        End Sub

        Private Sub Resize()
            Try
                IsLoading = True
                CardViewExplorerFileSource.ClearCache()
                For Each item As CardViewExplorerFileSource In Source
                    item.Resize(SizeType, Size)
                Next
            Finally
                IsLoading = False
            End Try
        End Sub

        Private Sub OpenRoot()
            Source.Clear()
            For Each drive In IO.DriveInfo.GetDrives().Where(Function(x) x.DriveType = IO.DriveType.Fixed)
                Source.Add(CardViewExplorerFileSource.Create(drive.RootDirectory.Name, CardViewExplorerFileSource.TypeElement.Drive, SizeType, Size))
            Next

            pathField = Root
            RaisePathChanged()
        End Sub

        Private Sub RaisePathChanged()
            RaisePropertyChanged(Function(x) x.Path)
        End Sub
#End Region
    End Class

    Public Enum SizeIcon
        <Display(Name:="Extra large icons")>
        ExtraLarge
        <Display(Name:="Large icons")>
        Large
        <Display(Name:="Medium icons")>
        Medium
        <Display(Name:="Small icons")>
        Small
    End Enum
End Namespace
