Imports DevExpress.DemoData
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid.LookUp
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee

Namespace CommonDemo

    <DevExpress.Xpf.DemoBase.CodeFileAttribute("ModuleResources/MultiColumnLookupEditorTemplates.xaml")>
    Public Partial Class StandaloneMultiColumnLookupEditor
        Inherits CommonDemo.CommonDemoModule

        Public Sub New()
            Me.ViewModel = New CommonDemo.LookUpEditorDemoViewModel()
            Me.DataContext = Me.ViewModel
            Call System.Windows.Input.Keyboard.AddGotKeyboardFocusHandler(Me, New System.Windows.Input.KeyboardFocusChangedEventHandler(AddressOf Me.OnKeyBoardFocusChanged))
            Me.InitializeComponent()
        End Sub

        Private Property ViewModel As LookUpEditorDemoViewModel

        Private Sub OnKeyBoardFocusChanged(ByVal sender As Object, ByVal e As System.Windows.Input.KeyboardFocusChangedEventArgs)
            Dim focused = TryCast(e.NewFocus, System.Windows.DependencyObject)
            Me.ViewModel.FocusedEditor = If(TypeOf focused Is DevExpress.Xpf.Grid.LookUp.LookUpEdit, TryCast(focused, DevExpress.Xpf.Grid.LookUp.LookUpEdit), DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject(Of DevExpress.Xpf.Grid.LookUp.LookUpEdit)(focused))
        End Sub
    End Class

    Public Class LookUpEditorDemoViewModel
        Inherits DevExpress.Mvvm.ViewModelBase

        Private _OrdersSource As List(Of DevExpress.DemoData.Models.OrderDetailsExtended), _Employees As IList(Of DevExpress.Xpf.DemoBase.DataClasses.Employee), _Products As IList(Of DevExpress.DemoData.Models.Product)

        Public Sub New()
            Dim dataLoader = New DevExpress.DemoData.NWindDataLoader()
            Me.OrdersSource = New System.Collections.Generic.List(Of DevExpress.DemoData.Models.OrderDetailsExtended)(Me.GetOrderDetails(dataLoader))
            Me.SelectedOrders = New System.Collections.Generic.List(Of Object)() From {Me.OrdersSource(CInt((0))).ProductName, Me.OrdersSource(CInt((2))).ProductName, Me.OrdersSource(CInt((10))).ProductName}
            Me.Employees = New System.Collections.Generic.List(Of DevExpress.Xpf.DemoBase.DataClasses.Employee)(DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData.DataSource)
            Me.Products = New System.Collections.Generic.List(Of DevExpress.DemoData.Models.Product)(CType(dataLoader.Products, System.Collections.Generic.IList(Of DevExpress.DemoData.Models.Product)))
        End Sub

        Public Property OrdersSource As List(Of DevExpress.DemoData.Models.OrderDetailsExtended)
            Get
                Return _OrdersSource
            End Get

            Private Set(ByVal value As List(Of DevExpress.DemoData.Models.OrderDetailsExtended))
                _OrdersSource = value
            End Set
        End Property

        Public Property SelectedOrders As IList(Of Object)

        Public Property Employees As IList(Of DevExpress.Xpf.DemoBase.DataClasses.Employee)
            Get
                Return _Employees
            End Get

            Private Set(ByVal value As IList(Of DevExpress.Xpf.DemoBase.DataClasses.Employee))
                _Employees = value
            End Set
        End Property

        Public Property Products As IList(Of DevExpress.DemoData.Models.Product)
            Get
                Return _Products
            End Get

            Private Set(ByVal value As IList(Of DevExpress.DemoData.Models.Product))
                _Products = value
            End Set
        End Property

        Public Property FocusedEditor As LookUpEdit
            Get
                Return GetValue(Of DevExpress.Xpf.Grid.LookUp.LookUpEdit)()
            End Get

            Set(ByVal value As LookUpEdit)
                SetValue(value)
            End Set
        End Property

        Private Function GetOrderDetails(ByVal dataLoader As DevExpress.DemoData.NWindDataLoader) As IEnumerable(Of DevExpress.DemoData.Models.OrderDetailsExtended)
            Return CType(dataLoader.OrderDetailsExtended, System.Collections.Generic.IEnumerable(Of DevExpress.DemoData.Models.OrderDetailsExtended)).Distinct(New CommonDemo.OrderEqualityComparer())
        End Function
    End Class

    Public Class OrderEqualityComparer
        Implements System.Collections.Generic.IEqualityComparer(Of DevExpress.DemoData.Models.OrderDetailsExtended)

        Public Overloads Function Equals(ByVal x As DevExpress.DemoData.Models.OrderDetailsExtended, ByVal y As DevExpress.DemoData.Models.OrderDetailsExtended) As Boolean Implements Global.System.Collections.Generic.IEqualityComparer(Of Global.DevExpress.DemoData.Models.OrderDetailsExtended).Equals
            If x Is Nothing AndAlso y IsNot Nothing Then Return False
            If x IsNot Nothing AndAlso y Is Nothing Then Return False
            Return Equals(x.ProductName, y.ProductName)
        End Function

        Public Overloads Function GetHashCode(ByVal obj As DevExpress.DemoData.Models.OrderDetailsExtended) As Integer Implements Global.System.Collections.Generic.IEqualityComparer(Of Global.DevExpress.DemoData.Models.OrderDetailsExtended).GetHashCode
            Return obj.ProductName.GetHashCode()
        End Function
    End Class

    Public Class LookUpEditOptionsTemplateSelector
        Inherits System.Windows.Controls.DataTemplateSelector

        Public Property LookUpTemplate As DataTemplate

        Public Property SearchLookUpTemplate As DataTemplate

        Public Property MultiSelectLookUpTemplate As DataTemplate

        Public Property PlaceHolderTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As System.Windows.DependencyObject) As DataTemplate
            Dim edit = TryCast(item, DevExpress.Xpf.Editors.LookUpEditBase)
            If edit IsNot Nothing Then
                Select Case edit.Name
                    Case "defaultLookUpEdit"
                        Return Me.LookUpTemplate
                    Case "searchLookUpEdit"
                        Return Me.SearchLookUpTemplate
                    Case "multiSelectLookUpEdit"
                        Return Me.MultiSelectLookUpTemplate
                End Select
            End If

            Return Me.PlaceHolderTemplate
        End Function
    End Class
End Namespace
