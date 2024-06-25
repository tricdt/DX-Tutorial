Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Windows.Media

Namespace GridDemo

    Public Class ProductItemToImageConverter
        Implements IValueConverter

        Private Shared _Instance As ProductItemToImageConverter

        Shared Sub New()
            Instance = New ProductItemToImageConverter()
        End Sub

        Public Shared Property Instance As ProductItemToImageConverter
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As ProductItemToImageConverter)
                _Instance = value
            End Set
        End Property

        Private ReadOnly regex As Regex = New Regex("[^A-Z]+", RegexOptions.IgnoreCase Or RegexOptions.Compiled)

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim category = If(value IsNot Nothing, value.ToString(), String.Empty)
            Return If(Not String.IsNullOrEmpty(category), New Uri(String.Format("pack://application:,,,/GridDemo;component/Images/Products/{0}.svg", Clear(category))), Nothing)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Private Function Clear(ByVal category As String) As String
            Return regex.Replace(category, "")
        End Function
    End Class

    Public MustInherit Class BaseValueConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public MustOverride Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert

        Public Overridable Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public NotOverridable Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public MustInherit Class BaseMultiValueConverter
        Inherits MarkupExtension
        Implements IMultiValueConverter

        Public MustOverride Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert

        Public Overridable Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public NotOverridable Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class MultiSelectModeConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return Not Equals(CType(value, MultiSelectMode), MultiSelectMode.None)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If(CBool(value), MultiSelectMode.Row, MultiSelectMode.None)
        End Function
    End Class

    Public Class FocusedToColorConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Sub New()
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If Equals((CStr(parameter)), [Enum].GetName(GetType(FocusedGrid), CType(value, FocusedGrid))) Then Return New SolidColorBrush(Color.FromArgb(50, 200, 0, 0))
            Return New SolidColorBrush(Color.FromArgb(0, 0, 0, 0))
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Class PastUnderFocusedRowToSelectedIndexConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Sub New()
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If((CBool(value)), 0, 1)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return If((CInt(value) = 0), True, False)
        End Function
#End Region
    End Class

    Public Class BooleanToDefaultBooleanConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(CBool(value), DefaultBoolean.True, DefaultBoolean.False)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class ScrollingAnimationDurationToBooleanConverterExtension
        Inherits BaseValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Return System.Convert.ToDouble(value) > 0
        End Function

        Public Overrides Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Return If(System.Convert.ToBoolean(value), 350, 0)
        End Function
    End Class

    Public Class DefaultBooleanToNulltableConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return CInt(value) <> CInt(DefaultBoolean.False)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If Not(TypeOf value Is Boolean) Then Return DefaultBoolean.Default
            Return If(CBool(value), DefaultBoolean.True, DefaultBoolean.False)
        End Function
    End Class

    Public Class GroupNameToImageConverterExtension
        Inherits BaseValueConverter

        Public Shared images As List(Of String) = New List(Of String) From {"administration", "inventory", "manufacturing", "quality", "research", "sales"}

        Public Shared Function GetImagePathByGroupName(ByVal groupName As String) As String
            groupName = groupName.ToLowerInvariant()
            For Each item As String In images
                If groupName.Contains(item) Then
                    Return "pack://application:,,,/GridDemo;component/Images/GroupName/" & item & ".svg"
                End If
            Next

            Return groupName
        End Function

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing Then Return Nothing
            Return GetImagePathByGroupName(CStr(value))
        End Function
    End Class

    Public Class DemoHeaderImageExtension
        Inherits MarkupExtension

        Private ReadOnly imageName As String

        Public Sub New(ByVal imageName As String)
            Me.imageName = imageName
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Dim path = "pack://application:,,,/GridDemo;component/Images/HeaderImages/" & imageName.Replace(" ", String.Empty) & ".svg"
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri(path), .Size = New Size(16, 16)}
            Return CType(extension.ProvideValue(Nothing), ImageSource)
        End Function
    End Class

    Public Class ColumnHeaderTextConverter
        Implements IValueConverter

        Public Property ColumnName As String

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Return If(Equals((CStr(value)).Replace(" ", ""), ColumnName), "Bold", "Normal")
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class BirthdayImageVisibilityConverterExtension
        Inherits BaseValueConverter

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If value Is Nothing OrElse Not(TypeOf value Is Date) Then Return Visibility.Collapsed
            Dim birthDate As Date = CDate(value)
            Dim someDate As Date = Date.Now.AddMonths(3)
            Dim someMonth As Integer = If(someDate.Month < 3, someDate.Month + 12, someDate.Month)
            Dim birthMonth As Integer = If(birthDate.Month < 3, birthDate.Month + 12, birthDate.Month)
            Return If(birthMonth >= Date.Now.Month AndAlso birthMonth <= someMonth AndAlso If(birthDate.Month = Date.Now.Month, birthDate.Day > Date.Now.Day, True), Visibility.Visible, Visibility.Collapsed)
        End Function
    End Class

    Public Class CountToVisibilityConverter
        Implements IValueConverter

        Public Property Invert As Boolean

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If((CInt(value) > 0) Xor Invert, Visibility.Visible, Visibility.Collapsed)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Class IntToDoubleConverter
        Implements IValueConverter

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return System.Convert.ToDouble(value)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return System.Convert.ToInt32(value)
        End Function
#End Region
    End Class

    Public Class PriorityToImageConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return New Image() With {.Source = CType(value, EnumMemberInfo).Image}
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class EditFormShowModeConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return False
            If Not [Enum].IsDefined(GetType(EditFormShowMode), value) Then Return False
            Dim mode As EditFormShowMode = CType(value, EditFormShowMode)
            Return Not mode.Equals(EditFormShowMode.None)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class Int32Extension
        Inherits MarkupExtension

        Private ReadOnly value As Integer

        Public Sub New(ByVal value As Integer)
            Me.value = value
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return value
        End Function
    End Class

    Public Class TabItemEventArgsToDataConverter
        Inherits EventArgsConverterBase(Of TabControlTabRemovedEventArgs)

        Protected Overrides Function Convert(ByVal sender As Object, ByVal args As TabControlTabRemovedEventArgs) As Object
            Return CType(args.Item, DXTabItem).DataContext
        End Function
    End Class

    Public Class TrackBarEditRangeConverter
        Inherits BaseMultiValueConverter

        Public Overrides Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            If values.Length <> 2 Then Return New TrackBarEditRange()
            Return New TrackBarEditRange(GetApproptiateValue(values(0)), GetApproptiateValue(values(1)))
        End Function

        Private Function GetApproptiateValue(ByVal value As Object) As Short
            If TypeOf value Is Integer Then Return System.Convert.ToInt16(value)
            Return If(TypeOf value Is Short, CShort(value), CShort(0))
        End Function

        Public Overrides Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object()
            Dim trackBarEditRange = TryCast(value, TrackBarEditRange)
            If trackBarEditRange Is Nothing Then Return New Object() {CShort(0), CShort(0)}
            Return New Object() {CShort(trackBarEditRange.SelectionStart), CShort(trackBarEditRange.SelectionEnd)}
        End Function
    End Class

    Public Class TotalSummaryPositionToBooleanConverter
        Implements IValueConverter

        Public Property Position As TotalSummaryPosition = TotalSummaryPosition.Bottom

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If Not(TypeOf value Is TotalSummaryPosition) Then Return False
            Return CType(value, TotalSummaryPosition) <> TotalSummaryPosition.None
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If Not(TypeOf value Is Boolean) Then Return TotalSummaryPosition.None
            Return If(CBool(value), Position, TotalSummaryPosition.None)
        End Function
    End Class

    Public Class TotalSummaryPanelsToBooleanConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            If values.Length <> 2 OrElse Not(TypeOf values(0) Is TotalSummaryPosition) OrElse Not(TypeOf values(1) Is Boolean) Then Return False
            Return(CType(values(0), TotalSummaryPosition) <> TotalSummaryPosition.None) OrElse CBool(values(1))
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class IsNotCardViewToBooleanConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return Not(TypeOf value Is DevExpress.Xpf.Grid.CardView)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
