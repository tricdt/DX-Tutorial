Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports DevExpress.Xpf.LayoutControl

Namespace LayoutControlDemo

    Public Partial Class pageEmployees
        Inherits LayoutControlDemoModule

        Public Sub New()
            InitializeComponent()
            RenderOptions.SetBitmapScalingMode(lc, BitmapScalingMode.HighQuality)
        End Sub

        Private Sub GroupBox_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim groupBox = CType(sender, GroupBox)
            groupBox.State = If(groupBox.State = GroupBoxState.Normal, GroupBoxState.Maximized, GroupBoxState.Normal)
        End Sub
    End Class

    Public Class EmployeeViewModel

        Private _ImageSource As ImageSource, _Model As Employee

        Public Sub New(ByVal employee As Employee)
            Model = employee
            ImageSource = ImageHelper.CreateImageFromStream(New MemoryStream(Model.ImageData))
        End Sub

        Public ReadOnly Property AddressLine2 As String
            Get
                Dim result As String = Model.City
                If Not String.IsNullOrEmpty(Model.StateProvinceName) Then result += ", " & Model.StateProvinceName
                If Not String.IsNullOrEmpty(Model.PostalCode) Then result += ", " & Model.PostalCode
                If Not String.IsNullOrEmpty(Model.CountryRegionName) Then result += ", " & Model.CountryRegionName
                Return result
            End Get
        End Property

        Public Property ImageSource As ImageSource
            Get
                Return _ImageSource
            End Get

            Private Set(ByVal value As ImageSource)
                _ImageSource = value
            End Set
        End Property

        Public Property Model As Employee
            Get
                Return _Model
            End Get

            Private Set(ByVal value As Employee)
                _Model = value
            End Set
        End Property
    End Class

    Public Class EmployeesViewModel
        Inherits List(Of EmployeeViewModel)

        Public Sub New()
            Me.New(EmployeesWithPhotoData.DataSource)
        End Sub

        Public Sub New(ByVal model As IEnumerable(Of Employee))
            For Each employee As Employee In model
                Add(New EmployeeViewModel(employee))
            Next

            Sort(New Comparison(Of EmployeeViewModel)(AddressOf CompareByLastNameFirstName))
        End Sub

        Private Function CompareByLastNameFirstName(ByVal x As EmployeeViewModel, ByVal y As EmployeeViewModel) As Integer
            Dim value1 As String = x.Model.LastName & x.Model.FirstName
            Dim value2 As String = y.Model.LastName & y.Model.FirstName
            Return String.Compare(value1, value2)
        End Function
    End Class

    Public Class GenderToImageConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim genderName =(CStr(value)).ToLowerInvariant()
            If Equals(genderName, "m") Then Return "pack://application:,,,/LayoutControlDemo;component/Images/Male.svg"
            If Equals(genderName, "f") Then Return "pack://application:,,,/LayoutControlDemo;component/Images/Female.svg"
            Return genderName
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class
End Namespace
