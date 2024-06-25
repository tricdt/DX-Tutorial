Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Windows
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Dialogs
Imports DevExpress.Xpf.Gantt

Namespace GanttDemo

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class ProjectViewModel

        Const xmlFilePath As String = "Data\SoftwareDevelopmentPlan.xml"

        Public Overridable Property Tasks As GanttDemo.TaskDataItem()

        Public Overridable Property Resources As IEnumerable(Of GanttDemo.ResourceDataItem)

        Public Overridable Property ResourceLinks As IEnumerable(Of GanttDemo.ResourceLinkDataItem)

        Public Overridable Property WorkingTimeRules As IEnumerable(Of DevExpress.Xpf.Gantt.WorkingTimeRule)

        Public Overridable Property WorkdaysRules As IEnumerable(Of DevExpress.Xpf.Gantt.WorkdayRule)

        Public Sub New()
            Using stream = GanttDemo.ResourceUtils.GetResourceStream(GanttDemo.ProjectViewModel.xmlFilePath)
                GanttDemo.ProjectXMLLoader.LoadModel(stream, Me)
            End Using
        End Sub

        Public Sub LoadFile()
            Using dialog = New DevExpress.Xpf.Dialogs.DXOpenFileDialog() With {.Filter = "MS Project XML files (*.xml)|*.xml", .Multiselect = False}
                Dim dialogResult = dialog.ShowDialog()
                If dialogResult.HasValue AndAlso dialogResult.Value Then
                    Using stream = New System.IO.FileStream(dialog.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                        Try
                            GanttDemo.ProjectXMLLoader.LoadModel(stream, Me)
                        Catch
                            Dim fileName = System.IO.Path.GetFileName(dialog.FileName)
                            Call DevExpress.Xpf.Core.ThemedMessageBox.Show("Invalid File", String.Format("The ""{0}"" file is not a valid MS Project file.", fileName), System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.[Error])
                        End Try
                    End Using
                End If
            End Using
        End Sub
    End Class

    Public Enum ContentDisplayMode
        Resources
        Name
    End Enum

    Public Class ResourceDataItem
        Inherits DevExpress.Utils.ImmutableObject

        Private _UID As String, _Name As String

        Public Property UID As String
            Get
                Return _UID
            End Get

            Private Set(ByVal value As String)
                _UID = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Sub New(ByVal uid As String, ByVal name As String)
            Me.UID = uid
            Me.Name = name
        End Sub
    End Class

    Public Class ResourceLinkDataItem
        Inherits DevExpress.Utils.ImmutableObject

        Private _TaskUID As String, _ResourceUID As String, _Units As Double

        Public Property TaskUID As String
            Get
                Return _TaskUID
            End Get

            Private Set(ByVal value As String)
                _TaskUID = value
            End Set
        End Property

        Public Property ResourceUID As String
            Get
                Return _ResourceUID
            End Get

            Private Set(ByVal value As String)
                _ResourceUID = value
            End Set
        End Property

        Public Property Units As Double
            Get
                Return _Units
            End Get

            Private Set(ByVal value As Double)
                _Units = value
            End Set
        End Property

        Public Sub New(ByVal taskUID As String, ByVal resourceUID As String, ByVal units As Double)
            Me.TaskUID = taskUID
            Me.ResourceUID = resourceUID
            Me.Units = units
        End Sub
    End Class

    Public Class TaskDataItem
        Inherits DevExpress.Utils.ImmutableObject

        Private _ParentUID As String, _StartDate As DateTime, _FinishDate As DateTime, _BaselineStartDate As System.DateTime?, _BaselineFinishDate As System.DateTime?, _Name As String, _UID As String, _Predecessors As IEnumerable(Of String), _Progress As Double

        Public Property ParentUID As String
            Get
                Return _ParentUID
            End Get

            Private Set(ByVal value As String)
                _ParentUID = value
            End Set
        End Property

        Public Property StartDate As DateTime
            Get
                Return _StartDate
            End Get

            Private Set(ByVal value As DateTime)
                _StartDate = value
            End Set
        End Property

        Public Property FinishDate As DateTime
            Get
                Return _FinishDate
            End Get

            Private Set(ByVal value As DateTime)
                _FinishDate = value
            End Set
        End Property

        Public Property BaselineStartDate As System.DateTime?
            Get
                Return _BaselineStartDate
            End Get

            Private Set(ByVal value As System.DateTime?)
                _BaselineStartDate = value
            End Set
        End Property

        Public Property BaselineFinishDate As System.DateTime?
            Get
                Return _BaselineFinishDate
            End Get

            Private Set(ByVal value As System.DateTime?)
                _BaselineFinishDate = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property UID As String
            Get
                Return _UID
            End Get

            Private Set(ByVal value As String)
                _UID = value
            End Set
        End Property

        Public Property Predecessors As IEnumerable(Of String)
            Get
                Return _Predecessors
            End Get

            Private Set(ByVal value As IEnumerable(Of String))
                _Predecessors = value
            End Set
        End Property

        Public Property Progress As Double
            Get
                Return _Progress
            End Get

            Private Set(ByVal value As Double)
                _Progress = value
            End Set
        End Property

        Public Sub New(ByVal uid As String, ByVal parentUID As String, ByVal name As String, ByVal startDate As String, ByVal finishDate As String, ByVal baselineStartDate As String, ByVal baselineFinishDate As String, ByVal progress As Double, ByVal predecessors As System.Collections.Generic.IEnumerable(Of String))
            Me.UID = uid
            Me.ParentUID = parentUID
            Me.Name = name
            Me.StartDate = Me.ParseDateTime(CStr((startDate))).Value
            Me.FinishDate = Me.ParseDateTime(CStr((finishDate))).Value
            Me.BaselineStartDate = Me.ParseDateTime(baselineStartDate)
            Me.BaselineFinishDate = Me.ParseDateTime(baselineFinishDate)
            Me.Progress = progress
            Me.Predecessors = predecessors
        End Sub

        Private Function ParseDateTime(ByVal inputString As String) As System.DateTime?
            If String.IsNullOrEmpty(inputString) Then Return Nothing
            Return System.DateTime.Parse(inputString, System.Globalization.CultureInfo.InvariantCulture)
        End Function
    End Class
End Namespace
