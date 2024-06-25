Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows.Media.Imaging

Namespace NavigationDemo.Utils

    Public Module ImageHelper

        Private ReadOnly EmployeeDepartmentImages As List(Of String) = New List(Of String) From {"administration", "inventory", "manufacturing", "quality", "research", "sales"}

        Public Function GetEmployeeDepartmentImage(ByVal departmentName As String) As Uri
            Dim imageName = EmployeeDepartmentImages.FirstOrDefault(Function(x) departmentName.ToLower().Contains(x))
            If String.IsNullOrEmpty(imageName) Then Return Nothing
            Return New Uri("/NavigationDemo;component/Images/Departments/" & imageName & ".svg", UriKind.RelativeOrAbsolute)
        End Function
    End Module

    Public Class PhotoInfo

        Private _Image As ImageSource, _Uri As String, _Dimension As String, _Name As String, _Size As String

        Public Sub New(ByVal uri As String, ByVal rating As Integer)
            Image = New BitmapImage(New Uri(uri))
            Dimension = String.Format("{0}x{1}", Image.Width, Image.Height)
            Size = String.Format("{0} KB", (New FileInfo(uri).Length / 1024).ToString())
            Name = Path.GetFileName(uri)
            Me.Uri = uri
            Me.Rating = rating
        End Sub

        Public Property Image As ImageSource
            Get
                Return _Image
            End Get

            Private Set(ByVal value As ImageSource)
                _Image = value
            End Set
        End Property

        Public Property Uri As String
            Get
                Return _Uri
            End Get

            Private Set(ByVal value As String)
                _Uri = value
            End Set
        End Property

        Public Property Dimension As String
            Get
                Return _Dimension
            End Get

            Private Set(ByVal value As String)
                _Dimension = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Size As String
            Get
                Return _Size
            End Get

            Private Set(ByVal value As String)
                _Size = value
            End Set
        End Property

        Public Overridable Property Rating As Integer
    End Class
End Namespace
