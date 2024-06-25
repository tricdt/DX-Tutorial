Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.PropertyGrid
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows.Controls

Namespace PropertyGridDemo

    Public Partial Class TypeConvertersFluentAPIModule
        Inherits PropertyGridDemoModule

        Public Sub New()
            InitializeComponent()
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata(Of ProductDescriptorMetadata)()
            DataContext = New ProductDescriptor With {.Product = New Product With {.Name = "Macintosh"}, .Tags = New String() {"Apple", "Fruit", "Round"}}
        End Sub

        Private Sub PropertyGridControl_OnCustomExpand(ByVal sender As Object, ByVal args As CustomExpandEventArgs)
            If args.IsInitializing Then
                args.IsExpanded = True
            End If
        End Sub
    End Class

    Public Class Product

        Public Property Name As String

        Public Property Count As Integer
    End Class

    Public Class ProductDescriptor

        Public Property Product As Product

        Public Property Tags As String()
    End Class

    Public Class ProductDescriptorMetadata

        Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of ProductDescriptor))
            builder.Property(Function(x) x.Product).TypeConverter().ConvertFromRule(Function(ByVal str As String) New Product With {.Name = str, .Count = 0}).ConvertToRule(Function(product) If(product Is Nothing, Nothing, product.Name)).PropertiesProvider(Function() TypeDescriptor.GetProperties(GetType(Product)).Cast(Of PropertyDescriptor)()).EndTypeConverter()
            builder.Property(Function(x) x.Tags).TypeConverter().ConvertFromRule(Function(ByVal str As String) If(String.IsNullOrEmpty(str), Nothing, str.Split(" "c))).ConvertToRule(Function(strArray) If(strArray Is Nothing, Nothing, Enumerable.Aggregate(strArray, Function(sum, element) sum & " " & element))).EndTypeConverter()
        End Sub
    End Class
End Namespace
