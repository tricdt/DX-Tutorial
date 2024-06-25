Imports DevExpress.Data
Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports System.Linq
Imports System.Windows.Threading

Namespace GridDemo

    <POCOViewModel>
    Public Class RealtimeDataSourceViewModel

        Private _UpdateInfo As ObservableCollection(Of GridDemo.RealtimeDataSourceViewModel.UpdateCountInfo), _MaxLoad As Integer

#Region "Stocks"
        Private Shared ReadOnly Names As String() = {"ANR", "FE", "GT", "PRGO", "APD", "PPL", "AES", "AVB", "IBM", "GAS", "EFX", "GPC", "ICE", "IVZ", "KO", "CCE", "SO", "STI", "BWA", "HRL", "WFM", "LM", "TROW", "K", "EXPE", "PCAR", "TRIP", "WHR", "WMT", "NU", "HST", "CVH", "LMT", "MAR", "CVC", "RF", "VMC", "PHM", "MU", "IRM", "AMT", "BXP", "STT", "PBCT", "FISV", "BLL", "MTB", "DIS", "LH", "AKAM", "CPB", "MYL", "LIFE", "LEG", "SCG", "CNX", "COL", "MCHP", "GR", "DUK", "BAC", "NUE", "UNM", "DLTR", "ABC", "TEG", "RRD", "EQR", "EXC", "BA", "CME", "NTRS", "VTR", "FITB", "PG", "KR", "M", "SNI", "ETN", "CLF", "PH", "KEY", "SHW", "HD", "AFL", "TSS", "CMI", "HBAN", "AEP", "BIG", "LTD", "ESRX", "GLW", "WPI", "MON", "AAPL", "DF", "T", "CMA", "THC", "LUV", "TXN", "TIE", "PX"}

#End Region
#Region "UpdateCountInfo"
        Public Class UpdateCountInfo

            Private _Time As Double, _Count As Integer

            Public Sub New(ByVal time As Double, ByVal count As Integer)
                Me.Time = time
                Me.Count = count
            End Sub

            Public Property Time As Double
                Get
                    Return _Time
                End Get

                Private Set(ByVal value As Double)
                    _Time = value
                End Set
            End Property

            Public Property Count As Integer
                Get
                    Return _Count
                End Get

                Private Set(ByVal value As Integer)
                    _Count = value
                End Set
            End Property
        End Class

#End Region
        Private dataUpdater As DataUpdaterBase

        Private ReadOnly updateInfoTimer As DispatcherTimer

        Private ReadOnly changesPerSecondStopwatch As Stopwatch = Stopwatch.StartNew()

        Protected Sub New()
            UpdateInfo = New ObservableCollection(Of UpdateCountInfo)()
            updateInfoTimer = New DispatcherTimer(TimeSpan.FromSeconds(0.5), DispatcherPriority.Send, AddressOf TimerShowCallback, Dispatcher.CurrentDispatcher)
            UseRealTimeSource = True
            MaxLoad = 34
            Load = MaxLoad
        End Sub

        Public Property UpdateInfo As ObservableCollection(Of UpdateCountInfo)
            Get
                Return _UpdateInfo
            End Get

            Private Set(ByVal value As ObservableCollection(Of UpdateCountInfo))
                _UpdateInfo = value
            End Set
        End Property

        Public Property MaxLoad As Integer
            Get
                Return _MaxLoad
            End Get

            Private Set(ByVal value As Integer)
                _MaxLoad = value
            End Set
        End Property

        Public Overridable Property Source As Object

        Public Overridable Property Load As Integer

        Protected Sub OnLoadChanged()
            SetLoad()
        End Sub

        Public Overridable Property UseRealTimeSource As Boolean

        Protected Sub OnUseRealTimeSourceChanged()
            ClearData()
            Dim list = Names.[Select](Function(name) New MarketData(name)).ToList()
            If UseRealTimeSource Then
                Source = New RealTimeSource With {.DataSource = list}
                dataUpdater = New RealTimeDataUpdater(list)
            Else
                Source = list
                dataUpdater = New BackgroundDataUpdater(list)
            End If

            SetLoad()
        End Sub

        Public Sub Dispose()
            ClearData()
            updateInfoTimer.Stop()
        End Sub

        Private Sub SetLoad()
            dataUpdater.SetLoad(Load, MaxLoad)
        End Sub

        Private Sub ClearData()
            If dataUpdater IsNot Nothing Then dataUpdater.Stop()
            If TypeOf Source Is RealTimeSource Then
                CType(Source, RealTimeSource).Dispose()
            End If
        End Sub

        Private Function GetChangesPerSecond() As Integer
            Dim changes = dataUpdater.GetAndResetChangesCount()
            Dim changeReportInterval As TimeSpan = changesPerSecondStopwatch.Elapsed
            changesPerSecondStopwatch.Restart()
            Return Convert.ToInt32(changes / changeReportInterval.TotalSeconds)
        End Function

        Private Sub TimerShowCallback(ByVal sender As Object, ByVal e As EventArgs)
            Dim changePerSecond = GetChangesPerSecond()
            UpdateInfo.Add(New UpdateCountInfo(Date.Now.TimeOfDay.TotalSeconds, changePerSecond))
            If UpdateInfo.Count > 10 Then UpdateInfo.RemoveAt(0)
        End Sub
    End Class
End Namespace
