Imports DevExpress.Data.Helpers
Imports DevExpress.Utils
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Grid
Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media.Imaging
Imports System.Windows.Markup
Imports System.Globalization
Imports System.Windows.Media
Imports DevExpress.Xpf.Core

Namespace ProductsDemo.Modules

    Public Class EmptyPhotoConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value IsNot Nothing Then Return value
            Return New System.Windows.Media.Imaging.BitmapImage(New System.Uri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Contacts/Unknown-user.png", System.UriKind.RelativeOrAbsolute))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Return value
        End Function
    End Class

    Public Class ViewToVisibilityConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is ProductsDemo.Modules.ViewType) Then Return System.Windows.Visibility.Collapsed
            Dim viewType As ProductsDemo.Modules.ViewType = CType(value, ProductsDemo.Modules.ViewType)
            Return If(viewType = ProductsDemo.Modules.ViewType.TableView, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotSupportedException()
        End Function
    End Class

    Public Class ViewToMarginConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Return If(TypeOf value Is DevExpress.Xpf.Grid.TableView, New System.Windows.Thickness(1, 1, 4, 1), New System.Windows.Thickness(1))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotSupportedException()
        End Function
    End Class

    Public Class ObjectToOpacityConverter
        Implements System.Windows.Data.IValueConverter

        Public Property Invert As Boolean

        Public Property Opacity As Double

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Return If((value IsNot Nothing Xor Me.Invert), Me.Opacity, 0)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class

    Public Class ColumnIconConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is Integer) Then Return Nothing
            Dim intValue As Integer = CInt(value)
            Dim extension = New DevExpress.Xpf.Core.SvgImageSourceExtension() With {.Uri = New System.Uri(String.Format("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Status_{0}.svg", intValue), System.UriKind.RelativeOrAbsolute), .Size = New System.Windows.Size(16, 16)}
            Return CType(extension.ProvideValue(Nothing), System.Windows.Media.ImageSource)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotSupportedException()
        End Function
    End Class

    Public Class ColumnPriorityConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is ProductsDemo.Modules.TaskPriority) Then Return Nothing
            Dim priority As ProductsDemo.Modules.TaskPriority = CType(value, ProductsDemo.Modules.TaskPriority)
            If priority = ProductsDemo.Modules.TaskPriority.Medium Then Return Nothing
            Dim extension = New DevExpress.Xpf.Core.SvgImageSourceExtension() With {.Uri = New System.Uri(System.[String].Format("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Priority_{0}.svg", priority), System.UriKind.RelativeOrAbsolute), .Size = New System.Windows.Size(16, 16)}
            Return CType(extension.ProvideValue(Nothing), System.Windows.Media.ImageSource)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotSupportedException()
        End Function
    End Class

    Public Class ColumnCategoryImageConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is ProductsDemo.Modules.TaskCategory) Then Return Nothing
            Dim category As ProductsDemo.Modules.TaskCategory = CType(value, ProductsDemo.Modules.TaskCategory)
            Return System.[String].Format("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Category_{0}.svg", category)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotSupportedException()
        End Function
    End Class

    Public Class ColumnFlagStatusImageConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is ProductsDemo.Modules.FlagStatus) Then Return Nothing
            Dim status As ProductsDemo.Modules.FlagStatus = CType(value, ProductsDemo.Modules.FlagStatus)
            Dim extension = New DevExpress.Xpf.Core.SvgImageSourceExtension() With {.Uri = New System.Uri(System.[String].Format("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/{0}.svg", status), System.UriKind.RelativeOrAbsolute), .Size = New System.Windows.Size(16, 16)}
            Return CType(extension.ProvideValue(Nothing), System.Windows.Media.ImageSource)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotSupportedException()
        End Function
    End Class

    Public Class TaskStatusCompletedConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is ProductsDemo.Modules.TaskStatus) Then Return False
            Dim status As ProductsDemo.Modules.TaskStatus = CType(value, ProductsDemo.Modules.TaskStatus)
            Return status = ProductsDemo.Modules.TaskStatus.Completed
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotSupportedException()
        End Function
    End Class

    Public Class BoolToDecorationsConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value Is Nothing OrElse Not(TypeOf value Is Boolean) Then Return Nothing
            Dim b As Boolean = CBool(value)
            Return If(Not b, Nothing, System.Windows.TextDecorations.Strikethrough)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotSupportedException()
        End Function
    End Class

    Public Class SplitStringConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Return If(value Is Nothing, Nothing, DevExpress.Data.Helpers.SplitStringHelper.SplitPascalCaseString(value.ToString()))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotSupportedException()
        End Function
    End Class

    Public Class FieldNameToTemplateConverter
        Inherits DevExpress.Mvvm.BindableBase
        Implements System.Windows.Data.IValueConverter

        Private Shared cache As System.Collections.Generic.Dictionary(Of String, Object) = New System.Collections.Generic.Dictionary(Of String, Object)()

        Private targetTemplateNameField As String = Nothing

        Public Property TargetTemplateName As String
            Get
                Return Me.targetTemplateNameField
            End Get

            Set(ByVal value As String)
                Me.targetTemplateNameField = value
                Me.RaisePropertiesChanged("TargetTemplateName")
            End Set
        End Property

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Dim column As DevExpress.Xpf.Grid.GridColumnBase = TryCast(value, DevExpress.Xpf.Grid.GridColumnBase)
            If column Is Nothing OrElse System.[String].IsNullOrWhiteSpace(column.FieldName) Then Return Nothing
            Dim fullTemplateName As String = System.[String].Format("{0}_{1}", column.FieldName, Me.TargetTemplateName)
            If Not ProductsDemo.Modules.FieldNameToTemplateConverter.cache.ContainsKey(fullTemplateName) Then Call ProductsDemo.Modules.FieldNameToTemplateConverter.cache.Add(fullTemplateName, column.TryFindResource(fullTemplateName))
            Return ProductsDemo.Modules.FieldNameToTemplateConverter.cache(fullTemplateName)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class

    Public Class PhoneFormatConverter
        Implements System.Windows.Data.IValueConverter

#Region "IValueConverter Members"
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return String.Empty
            Dim phoneNumber As String = CStr(value)
            If phoneNumber.Length = 0 Then Return phoneNumber
            Return String.Format("{0} {1}", phoneNumber.Substring(0, 5), phoneNumber.Substring(5))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
#End Region
    End Class

    Public Class MaskToTextEditSettingsConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Dim mask As String = TryCast(value, String)
            If value Is Nothing Then Return Nothing
            Return New DevExpress.Xpf.Editors.Settings.TextEditSettings With {.Mask = mask, .MaskUseAsDisplayFormat = True, .MaskType = DevExpress.Xpf.Editors.MaskType.RegEx}
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class

    Public Class AddressDetailPanelConverter
        Implements System.Windows.Data.IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Dim address As ProductsDemo.Modules.Address = TryCast(value, ProductsDemo.Modules.Address)
            If address Is Nothing Then Return Nothing
            Return address.GetAddressFullString(" | ")
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Throw New System.NotImplementedException()
        End Function
    End Class

    Public Class FocusedRowHandleToActiveRecordConverter
        Inherits System.Windows.Markup.MarkupExtension
        Implements System.Windows.Data.IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return Me
        End Function

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.Convert
            Dim view As DevExpress.Xpf.Grid.GridViewBase = TryCast(parameter, DevExpress.Xpf.Grid.GridViewBase)
            If value Is Nothing OrElse value.[GetType]() IsNot GetType(Integer) OrElse view Is Nothing Then Return -1
            Dim focusedRowHandle As Integer = CInt(value)
            Return view.Grid.GetListIndexByRowHandle(focusedRowHandle)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements Global.System.Windows.Data.IValueConverter.ConvertBack
            Dim view As DevExpress.Xpf.Grid.GridViewBase = TryCast(parameter, DevExpress.Xpf.Grid.GridViewBase)
            If value Is Nothing OrElse value.[GetType]() IsNot GetType(Integer) OrElse view Is Nothing Then Return -1
            Dim activeRecord As Integer = CInt(value)
            Return view.Grid.GetRowHandleByListIndex(activeRecord)
        End Function
    End Class
End Namespace
