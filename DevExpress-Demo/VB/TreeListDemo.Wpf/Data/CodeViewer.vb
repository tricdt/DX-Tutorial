Imports System
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Resources
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.Internal
Imports System.Collections
Imports System.Globalization
Imports System.Windows.Data

Namespace TreeListDemo

    Public Class CodeViewer
        Inherits CodeViewPresenter

        Public Shared ReadOnly CurrentItemsCollectionProperty As DependencyProperty = DependencyProperty.Register("CurrentItemsCollection", GetType(IEnumerable), GetType(CodeViewer), New PropertyMetadata(New PropertyChangedCallback(AddressOf OnCurrentItemsCollectionChanged)))

        Public Shared Sub OnCurrentItemsCollectionChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, CodeViewer).OnCurrentItemsCollectionChanged()
        End Sub

        Public Sub New()
            FontFamily = New FontFamily("Consolas")
            FontSize = 13
        End Sub

        Public Property CurrentItemsCollection As IEnumerable
            Get
                Return CType(GetValue(CurrentItemsCollectionProperty), IEnumerable)
            End Get

            Set(ByVal value As IEnumerable)
                SetValue(CurrentItemsCollectionProperty, value)
            End Set
        End Property

        Private Function LoadSourceCode(ByVal type As Type) As String
            Dim resourcePath As String = String.Format("/{0};component/Data/DataAnnotations/{1}{2}", type.Assembly.FullName.Split(","c)(0), type.Name, DemoHelper.GetCodeSuffix(type.Assembly))
            Dim resource As StreamResourceInfo = Application.GetResourceStream(New Uri(resourcePath, UriKind.Relative))
            Return If(resource IsNot Nothing, New StreamReader(resource.Stream).ReadToEnd(), Nothing)
        End Function

        Private Sub OnCurrentItemsCollectionChanged()
            If CurrentItemsCollection IsNot Nothing Then
                Dim elementType As Type = CType(New IEnumerableToFirstItemConverter(), IValueConverter).Convert(CurrentItemsCollection, Nothing, Nothing, CultureInfo.CurrentCulture).GetType()
                CodeText = New CodeLanguageText(DemoHelper.GetDemoLanguage(elementType.Assembly), LoadSourceCode(elementType))
            Else
                CodeText = Nothing
            End If
        End Sub
    End Class
End Namespace
