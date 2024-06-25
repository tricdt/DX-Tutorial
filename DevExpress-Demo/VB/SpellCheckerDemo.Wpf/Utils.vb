Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Windows.Controls
Imports System.Xml.Serialization
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers.TextColorizer

Namespace SpellCheckerDemo

    Public Class DemoUtils

        Public Shared ReadOnly PathToDemoData As String = "SpellCheckerDemo.Data"

        Public Shared ReadOnly PathToDictionaries As String = PathToDemoData & ".Dictionaries"

        Public Shared Function GetPathToResource(ByVal path As String, ByVal name As String) As String
            If DemoHelper.GetDemoLanguage(GetType(DemoUtils).Assembly) = CodeLanguage.CS Then
                Return String.Format("{0}.{1}", path, name)
            Else
                Return name
            End If
        End Function

        Public Shared Function GetDataStream(ByVal path As String, ByVal name As String) As Stream
            Dim fullPath As String = GetPathToResource(path, name)
            If Not String.IsNullOrEmpty(fullPath) Then Return Assembly.GetExecutingAssembly().GetManifestResourceStream(fullPath)
            Return Nothing
        End Function
    End Class

    Public Class DocumentLoadHelper

        Public Shared Function Load(ByVal fileName As String) As Stream
            Dim path As String = DemoUtils.GetPathToResource(DemoUtils.PathToDemoData, fileName)
            Return Assembly.GetExecutingAssembly().GetManifestResourceStream(path)
        End Function
    End Class

    Public Class Employees
        Implements System.ComponentModel.INotifyPropertyChanged

        Public Property EmployeeID As Integer

        Public Property LastName As String

        Public Property FirstName As String

        Public Property Title As String

        Public Property TitleOfCourtesy As String

        Public Property BirthDate As Date

        Public Property HireDate As Date

        Public Property Address As String

        Public Property City As String

        Public Property Region As String

        Public Property PostalCode As String

        Public Property Country As String

        Public Property HomePhone As String

        Public Property Extension As String

        Public Property Salary As Double

        Public Property OnVacation As Boolean

        Public Property Photo As Byte()

        Public Property Notes As String

        Public Property ReportsTo As Integer

#Region "INotifyPropertyChanged Members"
        Private onPropertyChanged As System.ComponentModel.PropertyChangedEventHandler

        Public Custom Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements ComponentModel.INotifyPropertyChanged.PropertyChanged
            AddHandler(ByVal value As System.ComponentModel.PropertyChangedEventHandler)
                onPropertyChanged = [Delegate].Combine(onPropertyChanged, value)
            End AddHandler

            RemoveHandler(ByVal value As System.ComponentModel.PropertyChangedEventHandler)
                onPropertyChanged = [Delegate].Remove(onPropertyChanged, value)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As ComponentModel.PropertyChangedEventArgs)
                onPropertyChanged ?(sender, e)
            End RaiseEvent
        End Event
#End Region
    End Class

    Public Module EmployeesData

        Private dataSourceField As IList

        Public ReadOnly Property DataSource As IList
            Get
                If dataSourceField Is Nothing Then
                    dataSourceField = GetDataSource()
                    MakeMistakes(dataSourceField)
                End If

                Return dataSourceField
            End Get
        End Property

        Private Function GetDataSource() As IList
            Dim s As XmlSerializer = New XmlSerializer(GetType(List(Of Employees)), New XmlRootAttribute("nwind"))
            Dim stream As Stream = GetType(EmployeesData).Assembly.GetManifestResourceStream(DemoUtils.GetPathToResource(DemoUtils.PathToDemoData, "nwind.xml"))
            Return CType(s.Deserialize(stream), IList)
        End Function

        Private Sub MakeMistakes(ByVal dataSet As IList)
            For Each employee As Employees In dataSet
                Dim text As StringBuilder = New StringBuilder(employee.Notes)
                Dim charSet As List(Of Char) = CreateCharSet(text)
                Dim random As Random = New Random(Environment.TickCount)
                For i As Integer = text.Length - 1 To 0 Step -30
                    If Not Char.IsLetter(text(i)) Then Continue For
                    Dim ch As Char = GetRandomChar(charSet)
                    If Char.IsUpper(text(i)) Then ch = Char.ToUpper(ch)
                    If text(i) = ch Then
                        text.Remove(i, 1)
                    Else
                        text(i) = ch
                    End If
                Next

                employee.Notes = text.ToString()
            Next
        End Sub

        Private Function CreateCharSet(ByVal text As StringBuilder) As List(Of Char)
            Dim result As List(Of Char) = New List(Of Char)()
            Dim length As Integer = text.Length
            For i As Integer = 0 To length - 1
                Dim ch As Char = text(i)
                If Not Char.IsLetter(ch) Then Continue For
                ch = Char.ToLower(ch)
                Dim index As Integer = result.BinarySearch(ch)
                If index < 0 Then result.Insert(Not index, ch)
            Next

            Return result
        End Function

        Private Function GetRandomChar(ByVal charSet As List(Of Char)) As Char
            Dim random As Random = New Random(Environment.TickCount)
            Dim index As Integer = random.Next(0, charSet.Count - 1)
            Return charSet(index)
        End Function
    End Module
End Namespace
