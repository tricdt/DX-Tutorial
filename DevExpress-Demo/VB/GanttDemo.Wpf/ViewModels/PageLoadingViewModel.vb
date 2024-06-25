Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Threading
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Utils
Imports DevExpress.Xpf.Gantt

Namespace GanttDemo

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class PageLoadingViewModel

        Private _DataItems As ObservableCollection(Of GanttDemo.DataLoadingItem)

        Private Class DataLoadingItemComparer
            Implements System.Collections.Generic.IComparer(Of GanttDemo.DataLoadingItem)

            Public Function Compare(ByVal x As GanttDemo.DataLoadingItem, ByVal y As GanttDemo.DataLoadingItem) As Integer Implements Global.System.Collections.Generic.IComparer(Of Global.GanttDemo.DataLoadingItem).Compare
                Return x.FinishDate.CompareTo(y.FinishDate)
            End Function
        End Class

        Const csvFilePath As String = "Data\PageLoading.csv"

        Public Property DataItems As ObservableCollection(Of GanttDemo.DataLoadingItem)
            Get
                Return _DataItems
            End Get

            Private Set(ByVal value As ObservableCollection(Of GanttDemo.DataLoadingItem))
                _DataItems = value
            End Set
        End Property

        Public Overridable Property LastItem As DataLoadingItem

        Public Overridable Property StartTime As DateTime

        Private ReadOnly sourceItems As System.Collections.Generic.List(Of GanttDemo.DataLoadingItem)

        Private ReadOnly timer As System.Windows.Threading.DispatcherTimer = New System.Windows.Threading.DispatcherTimer()

        Private itemIndex As Integer = 0

        Public Sub New()
            AddHandler Me.timer.Tick, AddressOf Me.TimerTick
            Me.DataItems = New System.Collections.ObjectModel.ObservableCollection(Of GanttDemo.DataLoadingItem)()
            Using stream = GanttDemo.ResourceUtils.GetResourceStream(GanttDemo.PageLoadingViewModel.csvFilePath)
                Me.sourceItems = GanttDemo.CSVLoader.LoadItems(stream)
            End Using

            Me.StartTime = System.Linq.Enumerable.Min(Of GanttDemo.DataLoadingItem, Global.System.DateTime)(Me.sourceItems, CType((Function(x) CDate((x.StartTime))), System.Func(Of GanttDemo.DataLoadingItem, System.DateTime))).AddMilliseconds(-300)
            Me.sourceItems.Sort(New GanttDemo.PageLoadingViewModel.DataLoadingItemComparer())
        End Sub

        Private Sub TimerTick(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.timer.[Stop]()
            Me.DataItems.Add(Me.sourceItems(Me.itemIndex))
            Me.LastItem = Me.sourceItems(Me.itemIndex)
            If Me.itemIndex < Me.sourceItems.Count - 1 Then
                Me.timer.Interval = Me.sourceItems(CInt((Me.itemIndex + 1))).FinishDate - Me.sourceItems(CInt((Me.itemIndex))).FinishDate
                Me.timer.Start()
            End If

            Me.itemIndex += 1
        End Sub

        Public Sub OnModuleLoaded()
            Me.timer.Start()
        End Sub

        Public Sub OnModuleUnloaded()
            Me.timer.[Stop]()
        End Sub

        Public Sub ReloadPage()
            Me.timer.[Stop]()
            Me.DataItems.Clear()
            Me.itemIndex = 0
            Me.timer.Interval = Me.sourceItems(CInt((0))).Duration
            Me.timer.Start()
        End Sub
    End Class

    Public Class DataLoadingItem
        Inherits DevExpress.Utils.ImmutableObject

        Private _StartTime As DateTime, _Duration As TimeSpan, _Name As String, _Status As String, _Type As String, _Size As Integer

        Public Property StartTime As DateTime
            Get
                Return _StartTime
            End Get

            Private Set(ByVal value As DateTime)
                _StartTime = value
            End Set
        End Property

        Public Property Duration As TimeSpan
            Get
                Return _Duration
            End Get

            Private Set(ByVal value As TimeSpan)
                _Duration = value
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

        Public Property Status As String
            Get
                Return _Status
            End Get

            Private Set(ByVal value As String)
                _Status = value
            End Set
        End Property

        Public Property Type As String
            Get
                Return _Type
            End Get

            Private Set(ByVal value As String)
                _Type = value
            End Set
        End Property

        Public Property Size As Integer
            Get
                Return _Size
            End Get

            Private Set(ByVal value As Integer)
                _Size = value
            End Set
        End Property

        Friend ReadOnly Property FinishDate As DateTime
            Get
                Return Me.StartTime + Me.Duration
            End Get
        End Property

        Public Sub New(ByVal startTime As System.DateTime, ByVal duration As System.TimeSpan, ByVal name As String, ByVal status As String, ByVal type As String, ByVal size As Integer)
            Me.StartTime = startTime
            Me.Duration = duration
            Me.Name = name
            Me.Status = status
            Me.Type = type
            Me.Size = size
        End Sub
    End Class

    Public Class DurationToStringConverter
        Inherits System.Windows.Markup.MarkupExtension
        Implements System.Windows.Data.IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return Me
        End Function

        Private Function FormatDuration(ByVal duration As System.TimeSpan) As String
            Return If(duration.TotalSeconds > 1, String.Format("{0} s", duration.TotalSeconds), String.Format("{0} ms", duration.TotalMilliseconds))
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If TypeOf value Is System.TimeSpan Then Return Me.FormatDuration(CType(value, System.TimeSpan))
            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class

    Public Class ByteSizeToStringConverter
        Inherits System.Windows.Markup.MarkupExtension
        Implements System.Windows.Data.IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return Me
        End Function

        Private Function FormatByteSize(ByVal size As Integer) As String
            Return If(size > 1024, String.Format("{0}KB", size \ 1024), String.Format("{0}B", size))
        End Function

        Private Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If TypeOf value Is Integer Then Return Me.FormatByteSize(CInt(value))
            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class
End Namespace
