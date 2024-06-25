Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports System.Linq
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Controls
Imports DevExpress.Xpf.Core
Imports GridDemo.ModuleResources

Namespace GridDemo

    <POCOViewModel>
    Public Class CardViewExplorerViewModel
        Implements IChildSelector

#Region "Properties"
        Private _Root As String, _Source As ObservableCollectionCore(Of GridDemo.CardViewExplorerFileSource)

        Public Property Root As String
            Get
                Return _Root
            End Get

            Private Set(ByVal value As String)
                _Root = value
            End Set
        End Property

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

        Private _path As String

        Public Property Path As String
            Get
                Return _path
            End Get

            Set(ByVal value As String)
                If Equals(value, Nothing) Then Return
                If Not Equals(value, Root) AndAlso Not value.EndsWith("\") Then value += "\"
                If Not Equals(value, Root) AndAlso Not Directory.Exists(value) Then
                    RaisePathChanged()
                    Return
                End If

                If Not Equals(_path, Nothing) Then ForwardStack.Push(_path)
                _path = value
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
#Region "POCO commands      "
        Public Sub Back()
            BackStack.Push(Path)
            Dim tmp As String = ForwardStack.Pop()
            _path = tmp
            RaisePathChanged()
            OpenFolder(tmp, False)
        End Sub

        Public Function CanBack() As Boolean
            Return ForwardStack.Count > 0
        End Function

        Public Sub Forward()
            Dim tmp As String = BackStack.Pop()
            ForwardStack.Push(_path)
            _path = tmp
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
            Dim path As String = Me.Path.TrimEnd("\"c)
            If path.Length <> 2 Then
                Me.Path = Directory.GetParent(path).FullName
            Else
                Me.Path = Root
            End If
        End Sub

        Public Function CanUp() As Boolean
            Return Not Equals(Path, Root)
        End Function

#End Region
#Region "Members"
        Protected Sub New()
            Root = "Root"
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
                    Dim info = New DirectoryInfo(path)
                    If info.Exists Then
                        For Each item In info.EnumerateDirectories().Where(Function(x)(x.Attributes And (FileAttributes.Hidden Or FileAttributes.System)) = 0)
                            Source.Add(CardViewExplorerFileSource.Create(item.FullName, CardViewExplorerFileSource.TypeElement.Folder, sizeType, size))
                        Next

                        For Each item In info.EnumerateFiles()
                            Source.Add(CardViewExplorerFileSource.Create(item.FullName, CardViewExplorerFileSource.TypeElement.File, sizeType, size))
                        Next
                    End If
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
            For Each drive In DriveInfo.GetDrives().Where(Function(x) x.DriveType = DriveType.Fixed)
                Source.Add(CardViewExplorerFileSource.Create(drive.RootDirectory.Name, CardViewExplorerFileSource.TypeElement.Drive, SizeType, Size))
            Next

            _path = Root
            RaisePathChanged()
        End Sub

        Private Sub RaisePathChanged()
            RaisePropertyChanged(Function(x) x.Path)
        End Sub

#End Region
#Region "IChildSelector"
        Private Function SelectChildren(ByVal item As Object) As IEnumerable Implements IChildSelector.SelectChildren
            Dim info = TryCast(item, DirectoryInfo)
            If info Is Nothing OrElse Not info.Exists Then Return Nothing
            Try
                Dim dirs = info.EnumerateDirectories().Where(Function(x)(x.Attributes And (FileAttributes.Hidden Or FileAttributes.System)) = 0)
                Return dirs
            Catch
                Return Enumerable.Empty(Of FileSystemInfo)()
            End Try
        End Function

        Public Sub CustomDisplayText(ByVal arg As BreadcrumbCustomDisplayTextEventArgs)
            Dim info = TryCast(arg.Item, DirectoryInfo)
            If info Is Nothing Then Return
            arg.DisplayText = info.Name.TrimEnd("\"c)
        End Sub

        Public Sub CustomImage(ByVal arg As BreadcrumbCustomImageEventArgs)
            Dim info = TryCast(arg.Item, DirectoryInfo)
            If info Is Nothing Then Return
            arg.Image = GetSmallIcon(info.FullName)
        End Sub

        Public Sub QueryPath(ByVal arg As BreadcrumbQueryPathEventArgs)
            If arg.Path.Count() = 0 Then Return
            Dim argPath As String = If(arg.Path.Count() = 1, arg.Path.FirstOrDefault() & arg.PathSeparator, String.Join(arg.PathSeparator, arg.Path))
            Dim infoList = New List(Of DirectoryInfo)()
            Dim info = New DirectoryInfo(argPath)
            If Not info.Exists Then
                arg.Breadcrumbs = infoList
                Return
            End If

            Do
                infoList.Insert(0, info)
                info = info.Parent
            Loop While info IsNot Nothing AndAlso info.Exists

            arg.Breadcrumbs = infoList
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
    End Enum
End Namespace
