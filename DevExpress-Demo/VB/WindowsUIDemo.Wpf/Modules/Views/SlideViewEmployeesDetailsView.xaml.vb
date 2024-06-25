Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.WindowsUI

Namespace WindowsUIDemo.Modules.Views

    Public Partial Class SlideViewEmployeesDetailsView
        Inherits NavigationPage

        Public Shared ReadOnly ParameterProperty As DependencyProperty = DependencyProperty.Register("Parameter", GetType(Object), GetType(SlideViewEmployeesDetailsView), Nothing)

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub OnNavigatedTo(ByVal e As DevExpress.Xpf.WindowsUI.Navigation.NavigationEventArgs)
            MyBase.OnNavigatedTo(e)
            Parameter = e.Parameter
        End Sub

        Private Sub DataLayoutControl_AutoGeneratingItem(ByVal sender As Object, ByVal e As DevExpress.Xpf.LayoutControl.DataLayoutControlAutoGeneratingItemEventArgs)
            Dim except = {"Photo", "SubEmployee", "EmployeeID"}
            If except.Contains(e.PropertyName) Then e.Cancel = True
        End Sub

        Public Property Parameter As Object
            Get
                Return CObj(GetValue(ParameterProperty))
            End Get

            Set(ByVal value As Object)
                SetValue(ParameterProperty, value)
            End Set
        End Property
    End Class
End Namespace
