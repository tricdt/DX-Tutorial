Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports System.Windows.Media.Imaging
Imports DevExpress.SalesDemo.Model
Imports System.Runtime.InteropServices

Namespace ProductsDemo

    Public MustInherit Class NavigationModule
        Inherits ViewModelBase

        Private _DataProvider As IDataProvider

        Public Sub New()
            IsActive = True
        End Sub

        Public MustOverride ReadOnly Property Caption As String

        Public MustOverride ReadOnly Property Description As String

        Public MustOverride ReadOnly Property Glyph As BitmapImage

        Private isActiveField As Boolean = False

        Public Property IsActive As Boolean
            Get
                Return isActiveField
            End Get

            Set(ByVal value As Boolean)
                SetProperty(isActiveField, value, "IsActive", New Action(AddressOf OnIsActiveChanged))
            End Set
        End Property

        Public Property DataProvider As IDataProvider
            Get
                Return _DataProvider
            End Get

            Private Set(ByVal value As IDataProvider)
                _DataProvider = value
            End Set
        End Property

        Private isDataLoadingField As Boolean = False

        Public Property IsDataLoading As Boolean
            Get
                Return isDataLoadingField
            End Get

            Private Set(ByVal value As Boolean)
                SetProperty(isDataLoadingField, value, "IsDataLoading")
            End Set
        End Property

        Private isDataLoadedField As Boolean = False

        Public Property IsDataLoaded As Boolean
            Get
                Return isDataLoadedField
            End Get

            Private Set(ByVal value As Boolean)
                SetProperty(isDataLoadedField, value, "IsDataLoaded")
            End Set
        End Property

        Protected MustOverride Sub SaveAndClearData()

        Protected MustOverride Sub RestoreData()

        Protected MustOverride Sub InitializeData()

        Protected Sub SaveAndClearPropertyValue(Of T)(ByRef storage As T, ByVal propName As String, ByVal Optional nullValue As T = Nothing)
            Dim storageValue As T = storage
            Dim resultValue As T = Nothing
            If PropertyCache Is Nothing Then PropertyCache = New Dictionary(Of String, Object)()
            If PropertyCache.ContainsKey(propName) Then PropertyCache.Remove(propName)
            PropertyCache.Add(propName, storageValue)
            resultValue = nullValue
            storage = resultValue
            RaisePropertyChanged(propName)
        End Sub

        Protected Sub SavePropertyValue(Of T)(ByVal storage As T, ByVal propName As String)
            If PropertyCache Is Nothing Then PropertyCache = New Dictionary(Of String, Object)()
            If PropertyCache.ContainsKey(propName) Then PropertyCache.Remove(propName)
            PropertyCache.Add(propName, storage)
        End Sub

        Protected Sub RestorePropertyValue(Of T)(<Out> ByRef storage As T, ByVal propName As String, ByVal doRaisePropertyChanged As Boolean)
            Dim resultValue As T = Nothing
            If PropertyCache IsNot Nothing AndAlso PropertyCache.ContainsKey(propName) Then
                resultValue = CType(PropertyCache(propName), T)
                PropertyCache.Remove(propName)
            End If

            storage = resultValue
            If doRaisePropertyChanged Then RaisePropertyChanged(propName)
        End Sub

        Protected Overrides Sub OnInitializeInDesignMode()
            MyBase.OnInitializeInDesignMode()
            DataProvider = New SampleDataProvider()
            InitializeData()
        End Sub

        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            DataProvider = GetDataProvider()
        End Sub

        Private Sub OnIsActiveChanged()
            If IsInDesignMode Then Return
            If IsDataLoading Then Return
            If IsActive AndAlso Not IsDataLoaded Then
                InitializeInBackground()
                Return
            End If

            If IsActive = False Then
                SaveAndClearData()
            Else
                RestoreData()
            End If
        End Sub

        Private Sub InitializeInBackground()
            If IsDataLoading OrElse IsDataLoaded Then Return
            IsDataLoading = True
            Dim t = DoInBackground(New Action(AddressOf InitializeData))
            t.ContinueWith(Sub(x)
                IsDataLoading = False
                IsDataLoaded = True
            End Sub)
            t.Start()
        End Sub

        Private Function DoInBackground(ByVal action As Action) As Task
            Return New Task(action)
        End Function

        Private PropertyCache As Dictionary(Of String, Object)
    End Class
End Namespace
