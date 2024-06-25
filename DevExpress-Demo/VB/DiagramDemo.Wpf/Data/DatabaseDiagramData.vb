Imports DevExpress.Data.Filtering
Imports DevExpress.Diagram.Core
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Xml.Serialization

Namespace DevExpress.Diagram.Demos

    Public Module DatabaseData

        Public Function GetDatabaseDefinition() As DatabaseDefinition
            Using stream = GetDataStream("DatabaseDiagram.xml")
                Dim serializer = New XmlSerializer(GetType(DatabaseDefinition))
                Return CType(serializer.Deserialize(stream), DatabaseDefinition)
            End Using
        End Function
    End Module

    <XmlInclude(GetType(TableDefinition)), XmlInclude(GetType(ConnectionDefinition))>
    <XmlRoot("Database")>
    Public Class DatabaseDefinition

        Private _Tables As Collection(Of DevExpress.Diagram.Demos.TableDefinition), _Connections As Collection(Of DevExpress.Diagram.Demos.ConnectionDefinition)

        <XmlAttribute(AttributeName:="Name")>
        Public Property Name As String

        <XmlArray("Tables"), XmlArrayItem(GetType(TableDefinition))>
        Public Property Tables As Collection(Of TableDefinition)
            Get
                Return _Tables
            End Get

            Private Set(ByVal value As Collection(Of TableDefinition))
                _Tables = value
            End Set
        End Property

        <XmlArray("Connections"), XmlArrayItem(GetType(ConnectionDefinition))>
        Public Property Connections As Collection(Of ConnectionDefinition)
            Get
                Return _Connections
            End Get

            Private Set(ByVal value As Collection(Of ConnectionDefinition))
                _Connections = value
            End Set
        End Property

        Default Public ReadOnly Property Item(ByVal tableName As String) As TableDefinition
            Get
                If String.IsNullOrEmpty(tableName) Then Throw New ArgumentException("tableName")
                Return Tables.SingleOrDefault(Function(x) String.Equals(x.Name, tableName))
            End Get
        End Property

        Public Sub New()
            Me.New(Enumerable.Empty(Of TableDefinition)(), Enumerable.Empty(Of ConnectionDefinition)())
        End Sub

        Public Sub New(ByVal tables As IEnumerable(Of TableDefinition), ByVal connections As IEnumerable(Of ConnectionDefinition))
            Me.Tables = New Collection(Of TableDefinition)(tables.ToList())
            Me.Connections = New Collection(Of ConnectionDefinition)(connections.ToList())
        End Sub
    End Class

    <XmlInclude(GetType(ColumnDefinition))>
    Public Class TableDefinition

        Private _Columns As Collection(Of DevExpress.Diagram.Demos.ColumnDefinition)

        <XmlArray("Columns")>
        <XmlArrayItem(GetType(ColumnDefinition))>
        Public Property Columns As Collection(Of ColumnDefinition)
            Get
                Return _Columns
            End Get

            Private Set(ByVal value As Collection(Of ColumnDefinition))
                _Columns = value
            End Set
        End Property

        <XmlAttribute("Name")>
        Public Property Name As String

        <XmlAttribute("PositionX")>
        Public Property PositionX As Integer

        <XmlAttribute("PositionY")>
        Public Property PositionY As Integer

        Default Public ReadOnly Property Item(ByVal columnName As String) As ColumnDefinition
            Get
                If String.IsNullOrEmpty(columnName) Then Throw New ArgumentException("columnName")
                Return Columns.SingleOrDefault(Function(x) String.Equals(x.Name, columnName))
            End Get
        End Property

        Public Sub New()
            Me.New(Enumerable.Empty(Of ColumnDefinition)())
        End Sub

        Public Sub New(ByVal columns As IEnumerable(Of ColumnDefinition))
            Me.Columns = New Collection(Of ColumnDefinition)(columns.ToList())
        End Sub
    End Class

    Public Class ColumnDefinition

        <XmlAttribute("TableName")>
        Public Property TableName As String

        <XmlAttribute("Name")>
        Public Property Name As String

        <XmlAttribute("IsPrimaryKey")>
        Public Property IsPrimaryKey As Boolean

        <XmlIgnore>
        Public ReadOnly Property Id As String
            Get
                Return String.Join(".", TableName, Name)
            End Get
        End Property
    End Class

    <XmlInclude(GetType(TableRelation))>
    Public Class ConnectionDefinition

        <XmlAttribute("From")>
        Public Property From As String

        <XmlAttribute("To")>
        Public Property [To] As String

        <XmlAttribute("FromRelation")>
        Public Property FromRelation As TableRelation

        <XmlAttribute("ToRelation")>
        Public Property ToRelation As TableRelation

        Public Sub New(ByVal from As ColumnDefinition, ByVal [to] As ColumnDefinition)
            Me.From = from.Id
            Me.To = [to].Id
        End Sub

        Public Sub New()
        End Sub
    End Class

    Public Enum TableRelation
        One
        Many
    End Enum

    Public Class DatabaseDefinitionKeySelector
        Implements IKeySelector

        Private Function GetKey(ByVal obj As Object) As Object Implements IKeySelector.GetKey
            If TypeOf obj Is TableDefinition Then
                Return CType(obj, TableDefinition).Name
            ElseIf TypeOf obj Is ColumnDefinition Then
                Return CType(obj, ColumnDefinition).Id
            End If

            Return obj
        End Function
    End Class

    Public Class TableRelationEvaluationOperator
        Implements ICustomFunctionOperator

        Private ReadOnly Property Name As String Implements ICustomFunctionOperator.Name
            Get
                Return "TableRelation"
            End Get
        End Property

        Private Function Evaluate(ParamArray operands As Object()) As Object Implements ICustomFunctionOperator.Evaluate
            Select Case CType(operands(0), TableRelation)
                Case TableRelation.One
                    Return "1"
                Case TableRelation.Many
                    Return "*"
            End Select

            Throw New ArgumentException()
        End Function

        Private Function ResultType(ParamArray operands As Type()) As Type Implements ICustomFunctionOperator.ResultType
            Return GetType(String)
        End Function
    End Class
End Namespace
