Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration
Imports System.Linq.Expressions

Namespace DevExpress.DevAV.ViewModels

    Public Class FilterTreeModelPageSpecificSettings(Of TSettings As ApplicationSettingsBase)
        Implements IFilterTreeModelPageSpecificSettings

        Private ReadOnly staticFiltersTitleField As String

        Private ReadOnly customFiltersTitleField As String

        Private ReadOnly settings As TSettings

        Private ReadOnly customFiltersProperty As PropertyDescriptor

        Private ReadOnly staticFiltersProperty As PropertyDescriptor

        Private ReadOnly hiddenFilterPropertiesField As IEnumerable(Of String)

        Private ReadOnly additionalFilterPropertiesField As IEnumerable(Of String)

        Public Sub New(ByVal settings As TSettings, ByVal staticFiltersTitle As String, ByVal getStaticFiltersExpression As Expression(Of Func(Of TSettings, FilterInfoList)), ByVal getCustomFiltersExpression As Expression(Of Func(Of TSettings, FilterInfoList)), ByVal Optional hiddenFilterProperties As IEnumerable(Of String) = Nothing, ByVal Optional additionalFilterProperties As IEnumerable(Of String) = Nothing, ByVal Optional customFiltersTitle As String = "Custom Filters")
            Me.settings = settings
            staticFiltersTitleField = staticFiltersTitle
            customFiltersTitleField = customFiltersTitle
            staticFiltersProperty = GetProperty(getStaticFiltersExpression)
            customFiltersProperty = GetProperty(getCustomFiltersExpression)
            hiddenFilterPropertiesField = hiddenFilterProperties
            additionalFilterPropertiesField = additionalFilterProperties
        End Sub

        Private Property CustomFilters As FilterInfoList Implements IFilterTreeModelPageSpecificSettings.CustomFilters
            Get
                Return GetFilters(customFiltersProperty)
            End Get

            Set(ByVal value As FilterInfoList)
                SetFilters(customFiltersProperty, value)
            End Set
        End Property

        Private Property StaticFilters As FilterInfoList Implements IFilterTreeModelPageSpecificSettings.StaticFilters
            Get
                Return GetFilters(staticFiltersProperty)
            End Get

            Set(ByVal value As FilterInfoList)
                SetFilters(staticFiltersProperty, value)
            End Set
        End Property

        Private ReadOnly Property StaticFiltersTitle As String Implements IFilterTreeModelPageSpecificSettings.StaticFiltersTitle
            Get
                Return staticFiltersTitleField
            End Get
        End Property

        Private ReadOnly Property CustomFiltersTitle As String Implements IFilterTreeModelPageSpecificSettings.CustomFiltersTitle
            Get
                Return customFiltersTitleField
            End Get
        End Property

        Private ReadOnly Property HiddenFilterProperties As IEnumerable(Of String) Implements IFilterTreeModelPageSpecificSettings.HiddenFilterProperties
            Get
                Return hiddenFilterPropertiesField
            End Get
        End Property

        Private ReadOnly Property AdditionalFilterProperties As IEnumerable(Of String) Implements IFilterTreeModelPageSpecificSettings.AdditionalFilterProperties
            Get
                Return additionalFilterPropertiesField
            End Get
        End Property

        Private Sub SaveSettings() Implements IFilterTreeModelPageSpecificSettings.SaveSettings
            settings.Save()
        End Sub

        Private Function GetProperty(ByVal expression As Expression(Of Func(Of TSettings, FilterInfoList))) As PropertyDescriptor
            If expression IsNot Nothing Then Return TypeDescriptor.GetProperties(settings)(GetPropertyName(expression))
            Return Nothing
        End Function

        Private Function GetFilters(ByVal [property] As PropertyDescriptor) As FilterInfoList
            Return If([property] IsNot Nothing, CType([property].GetValue(settings), FilterInfoList), Nothing)
        End Function

        Private Sub SetFilters(ByVal [property] As PropertyDescriptor, ByVal value As FilterInfoList)
            If [property] IsNot Nothing Then [property].SetValue(settings, value)
        End Sub

        Private Shared Function GetPropertyName(ByVal expression As Expression(Of Func(Of TSettings, FilterInfoList))) As String
            Dim memberExpression As MemberExpression = TryCast(expression.Body, MemberExpression)
            If memberExpression Is Nothing Then
                Throw New ArgumentException("expression")
            End If

            Return memberExpression.Member.Name
        End Function
    End Class
End Namespace
