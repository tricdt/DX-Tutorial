Imports System
Imports System.Linq
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors
Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.DemoBase

Namespace EditorsDemo

    <CodeFile("ModuleResources/AutoSuggestEditTemplates.xaml")>
    Public Partial Class AutoSuggestEditModule
        Inherits EditorsDemoModule

        Private ReadOnly context As NWindContext = NWindContext.Create()

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Countries_OnQuerySubmitted(ByVal sender As Object, ByVal e As AutoSuggestEditQuerySubmittedEventArgs)
            countries.ItemsSource = If(String.IsNullOrEmpty(e.Text), Nothing, context.CountriesArray.Where(Function(x) x.StartsWith(e.Text, StringComparison.InvariantCultureIgnoreCase)).Take(10).ToArray())
        End Sub

        Private Sub Products_OnQuerySubmitted(ByVal sender As Object, ByVal e As AutoSuggestEditQuerySubmittedEventArgs)
            products.ItemsSource = If(String.IsNullOrEmpty(e.Text), Nothing, context.Products.ToList().Where(Function(x) Regex.IsMatch(x.ProductName, Regex.Escape(e.Text), RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace)).Take(10).ToArray())
        End Sub

        Private Sub LookUp_OnQuerySubmitted(ByVal sender As Object, ByVal e As AutoSuggestEditQuerySubmittedEventArgs)
            lookup.ItemsSource = If(String.IsNullOrEmpty(e.Text), Nothing, context.Products.ToList().Where(Function(x) ProcessProduct(x, e.Text)).ToArray())
        End Sub

        Private Function ProcessProduct(ByVal product As Product, ByVal text As String) As Boolean
            Return Regex.IsMatch(product.ProductName, Regex.Escape(text), RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace) OrElse Regex.IsMatch(product.QuantityPerUnit, Regex.Escape(text), RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace) OrElse (product.UnitPrice IsNot Nothing AndAlso Regex.IsMatch(product.UnitPrice.ToString(), Regex.Escape(text), RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace))
        End Function
    End Class
End Namespace
