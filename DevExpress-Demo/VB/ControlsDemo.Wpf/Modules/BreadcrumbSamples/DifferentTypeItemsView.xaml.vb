Imports System.Linq
Imports System.Windows.Controls
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Controls

Namespace ControlsDemo.BreadcrumbSamples

    Public Partial Class DifferentTypeItemsView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            breadcrumb.ItemsSource = NWindContext.Create().Categories.OrderBy(Function(x) x.CategoryName)
            breadcrumb.ChildSelector = New NWindChildSelector()
            AddHandler breadcrumb.CustomDisplayText, AddressOf GetCustomText
            AddHandler breadcrumb.CustomImage, AddressOf GetCustomImage
            breadcrumb.EmptyItemText = "Select Category"
        End Sub

        Private Sub GetCustomImage(ByVal sender As Object, ByVal e As BreadcrumbCustomImageEventArgs)
            e.Image = NWindObjectHelper.GetCustomImage(e.Item)
        End Sub

        Private Sub GetCustomText(ByVal sender As Object, ByVal e As BreadcrumbCustomDisplayTextEventArgs)
            e.DisplayText = NWindObjectHelper.GetCustomText(e.Item)
        End Sub
    End Class
End Namespace
