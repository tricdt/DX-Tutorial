Imports System.Collections
Imports System.IO
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Controls

Namespace ControlsDemo.BreadcrumbSamples

    Friend Class NWindChildSelector
        Implements IChildSelector

        Private Function SelectChildren(ByVal item As Object) As IEnumerable Implements IChildSelector.SelectChildren
            Dim category As Category = TryCast(item, Category)
            If category IsNot Nothing Then Return category.Products.OrderBy(Function(x) x.ProductName)
            Dim product As Product = TryCast(item, Product)
            If product IsNot Nothing Then Return product.OrderDetails.OrderBy(Function(x) x.OrderID)
            Return Nothing
        End Function
    End Class

    Friend Module NWindObjectHelper

        Public Function GetCustomImage(ByVal item As Object) As ImageSource
            If TypeOf item Is Category Then Return GetImageSource(CType(item, Category).Icon17)
            If TypeOf item Is Product Then Return GetImageSource(CType(item, Product).Category.Icon17)
            If TypeOf item Is OrderDetailsExtended Then Return GetImageSource(CType(item, OrderDetailsExtended).Product.Category.Icon17)
            Return Nothing
        End Function

        Public Function GetCustomText(ByVal item As Object) As String
            If TypeOf item Is Category Then Return CType(item, Category).CategoryName
            If TypeOf item Is Product Then Return CType(item, Product).ProductName
            If TypeOf item Is OrderDetailsExtended Then Return String.Format("Order ID = {0}", CType(item, OrderDetailsExtended).OrderID)
            Return Nothing
        End Function

        Private Function GetImageSource(ByVal icon As Byte()) As ImageSource
            Dim stream = New MemoryStream(icon)
            Dim imageSource = New BitmapImage()
            imageSource.BeginInit()
            imageSource.StreamSource = stream
            imageSource.EndInit()
            Return imageSource
        End Function
    End Module
End Namespace
