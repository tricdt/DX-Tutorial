Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq

Namespace GridDemo

    Public MustInherit Class LargeDataSourceObjectBase

        Private _Id As Integer

        Protected Class ValuesContainer(Of T)

            Private modifiedValues As Dictionary(Of Integer, T)

            Private ReadOnly getDefaultValue As Func(Of Integer, T)

            Public Sub New(ByVal getDefaultValue As Func(Of Integer, T))
                Me.getDefaultValue = getDefaultValue
            End Sub

            Public Function GetValue(ByVal index As Integer) As T
                If modifiedValues Is Nothing Then Return getDefaultValue(index)
                Dim value As T
                If modifiedValues.TryGetValue(index, value) Then Return value
                Return getDefaultValue(index)
            End Function

            Public Sub SetValue(ByVal index As Integer, ByVal value As T)
                If modifiedValues Is Nothing Then modifiedValues = New Dictionary(Of Integer, T)()
                modifiedValues(index) = value
            End Sub
        End Class

        Private Shared ReadOnly Priorities As Priority() = [Enum].GetValues(GetType(Priority)).Cast(Of Priority)().ToArray()

        Shared Sub New()
        End Sub

        Const BaseColumnCount As Integer = 7

        Protected ReadOnly fromValues As ValuesContainer(Of String)

        Protected ReadOnly toValues As ValuesContainer(Of String)

        Protected ReadOnly sentValues As ValuesContainer(Of Date)

        Protected ReadOnly hasAttachmentValues As ValuesContainer(Of Boolean)

        Protected ReadOnly sizeValues As ValuesContainer(Of Integer)

        Protected ReadOnly priorityValues As ValuesContainer(Of Priority)

        Protected ReadOnly subjectValues As ValuesContainer(Of String)

        Public Sub New(ByVal id As Integer)
            Me.Id = id
            fromValues = New ValuesContainer(Of String)(Function(index) OutlookData.Users(GetPseudoRandomValue(Me.Id, index, OutlookData.Users.Length)).Name)
            toValues = New ValuesContainer(Of String)(Function(index) OutlookData.Users(GetPseudoRandomValue(Me.Id, index + 5, OutlookData.Users.Length)).Name)
            sentValues = New ValuesContainer(Of Date)(Function(index) Date.Today.AddDays(GetPseudoRandomValue(Me.Id, index, 30)))
            hasAttachmentValues = New ValuesContainer(Of Boolean)(Function(index) If(GetPseudoRandomValue(Me.Id, index, 2) = 0, True, False))
            sizeValues = New ValuesContainer(Of Integer)(Function(index) GetPseudoRandomValue(Me.Id, index, 10000))
            priorityValues = New ValuesContainer(Of Priority)(Function(index) Priorities(GetPseudoRandomValue(Me.Id, index, Priorities.Length)))
            subjectValues = New ValuesContainer(Of String)(Function(index) Subjects(GetPseudoRandomValue(Me.Id, index, Subjects.Length)))
        End Sub

        <Display(Name:="Id (0)", Order:=0)>
        Public Property Id As Integer
            Get
                Return _Id
            End Get

            Private Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property

        Private Function GetPseudoRandomValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer, ByVal maxValue As Integer) As Integer
            Return(rowIndex + columnIndex) Mod maxValue
        End Function
    End Class
End Namespace
