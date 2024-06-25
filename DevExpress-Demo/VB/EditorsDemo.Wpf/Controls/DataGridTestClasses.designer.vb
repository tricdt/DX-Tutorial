Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Data
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Linq
Imports System.Linq.Expressions
Imports System.ComponentModel
Imports System

Namespace GridDemo.Controls

    <System.Data.Linq.Mapping.DatabaseAttribute(Name:="DXGridServerModeDB")>
    Public Partial Class DataGridTestClassesDataContext
        Inherits System.Data.Linq.DataContext

        Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New System.Data.Linq.Mapping.AttributeMappingSource()

#Region "Extensibility Method Definitions"
        Partial Private Sub OnCreated()
        End Sub

        Partial Private Sub InsertWpfServerSideGridTest(ByVal instance As GridDemo.Controls.WpfServerSideGridTest)
        End Sub

        Partial Private Sub UpdateWpfServerSideGridTest(ByVal instance As GridDemo.Controls.WpfServerSideGridTest)
        End Sub

        Partial Private Sub DeleteWpfServerSideGridTest(ByVal instance As GridDemo.Controls.WpfServerSideGridTest)
        End Sub

#End Region
        Public Sub New(ByVal connection As String)
            MyBase.New(connection, GridDemo.Controls.DataGridTestClassesDataContext.mappingSource)
            Me.OnCreated()
        End Sub

        Public Sub New(ByVal connection As System.Data.IDbConnection)
            MyBase.New(connection, GridDemo.Controls.DataGridTestClassesDataContext.mappingSource)
            Me.OnCreated()
        End Sub

        Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
            MyBase.New(connection, mappingSource)
            Me.OnCreated()
        End Sub

        Public Sub New(ByVal connection As System.Data.IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
            MyBase.New(connection, mappingSource)
            Me.OnCreated()
        End Sub

        Public ReadOnly Property WpfServerSideGridTests As System.Data.Linq.Table(Of GridDemo.Controls.WpfServerSideGridTest)
            Get
                Return Me.GetTable(Of GridDemo.Controls.WpfServerSideGridTest)()
            End Get
        End Property
    End Class

    <System.Data.Linq.Mapping.TableAttribute(Name:="dbo.WpfServerSideGridTest")>
    Public Partial Class WpfServerSideGridTest
        Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged

        Private Shared emptyChangingEventArgs As System.ComponentModel.PropertyChangingEventArgs = New System.ComponentModel.PropertyChangingEventArgs(System.[String].Empty)

        Private _OID As Integer

        Private _Subject As String

        Private _From As String

        Private _Sent As System.Nullable(Of System.DateTime)

        Private _Size As System.Nullable(Of Long)

        Private _HasAttachment As System.Nullable(Of Boolean)

#Region "Extensibility Method Definitions"
        Partial Private Sub OnLoaded()
        End Sub

        Partial Private Sub OnValidate(ByVal action As System.Data.Linq.ChangeAction)
        End Sub

        Partial Private Sub OnCreated()
        End Sub

        Partial Private Sub OnOIDChanging(ByVal value As Integer)
        End Sub

        Partial Private Sub OnOIDChanged()
        End Sub

        Partial Private Sub OnSubjectChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnSubjectChanged()
        End Sub

        Partial Private Sub OnFromChanging(ByVal value As String)
        End Sub

        Partial Private Sub OnFromChanged()
        End Sub

        Partial Private Sub OnSentChanging(ByVal value As System.Nullable(Of System.DateTime))
        End Sub

        Partial Private Sub OnSentChanged()
        End Sub

        Partial Private Sub OnSizeChanging(ByVal value As System.Nullable(Of Long))
        End Sub

        Partial Private Sub OnSizeChanged()
        End Sub

        Partial Private Sub OnHasAttachmentChanging(ByVal value As System.Nullable(Of Boolean))
        End Sub

        Partial Private Sub OnHasAttachmentChanged()
        End Sub

#End Region
        Public Sub New()
            Me.OnCreated()
        End Sub

        <System.Data.Linq.Mapping.ColumnAttribute(Storage:="_OID", AutoSync:=System.Data.Linq.Mapping.AutoSync.OnInsert, DbType:="Int NOT NULL IDENTITY", IsPrimaryKey:=True, IsDbGenerated:=True)>
        Public Property OID As Integer
            Get
                Return Me._OID
            End Get

            Set(ByVal value As Integer)
                If(Me._OID <> value) Then
                    Me.OnOIDChanging(value)
                    Me.SendPropertyChanging()
                    Me._OID = value
                    Me.SendPropertyChanged("OID")
                    Me.OnOIDChanged()
                End If
            End Set
        End Property

        <System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Subject", DbType:="NVarChar(100)")>
        Public Property Subject As String
            Get
                Return Me._Subject
            End Get

            Set(ByVal value As String)
                If(Not Equals(Me._Subject, value)) Then
                    Me.OnSubjectChanging(value)
                    Me.SendPropertyChanging()
                    Me._Subject = value
                    Me.SendPropertyChanged("Subject")
                    Me.OnSubjectChanged()
                End If
            End Set
        End Property

        <System.Data.Linq.Mapping.ColumnAttribute(Name:="[From]", Storage:="_From", DbType:="NVarChar(100)")>
        Public Property From As String
            Get
                Return Me._From
            End Get

            Set(ByVal value As String)
                If(Not Equals(Me._From, value)) Then
                    Me.OnFromChanging(value)
                    Me.SendPropertyChanging()
                    Me._From = value
                    Me.SendPropertyChanged("From")
                    Me.OnFromChanged()
                End If
            End Set
        End Property

        <System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Sent", DbType:="DateTime")>
        Public Property Sent As System.Nullable(Of System.DateTime)
            Get
                Return Me._Sent
            End Get

            Set(ByVal value As System.Nullable(Of System.DateTime))
                If(Me._Sent <> value) Then
                    Me.OnSentChanging(value)
                    Me.SendPropertyChanging()
                    Me._Sent = value
                    Me.SendPropertyChanged("Sent")
                    Me.OnSentChanged()
                End If
            End Set
        End Property

        <System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Size", DbType:="BigInt")>
        Public Property Size As System.Nullable(Of Long)
            Get
                Return Me._Size
            End Get

            Set(ByVal value As System.Nullable(Of Long))
                If(Me._Size <> value) Then
                    Me.OnSizeChanging(value)
                    Me.SendPropertyChanging()
                    Me._Size = value
                    Me.SendPropertyChanged("Size")
                    Me.OnSizeChanged()
                End If
            End Set
        End Property

        <System.Data.Linq.Mapping.ColumnAttribute(Storage:="_HasAttachment", DbType:="Bit")>
        Public Property HasAttachment As System.Nullable(Of Boolean)
            Get
                Return Me._HasAttachment
            End Get

            Set(ByVal value As System.Nullable(Of Boolean))
                If(Me._HasAttachment <> value) Then
                    Me.OnHasAttachmentChanging(value)
                    Me.SendPropertyChanging()
                    Me._HasAttachment = value
                    Me.SendPropertyChanged("HasAttachment")
                    Me.OnHasAttachmentChanged()
                End If
            End Set
        End Property

        Public Event PropertyChanging As System.ComponentModel.PropertyChangingEventHandler Implements Global.System.ComponentModel.INotifyPropertyChanging.PropertyChanging

        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements Global.System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Protected Overridable Sub SendPropertyChanging()
            RaiseEvent PropertyChanging(Me, GridDemo.Controls.WpfServerSideGridTest.emptyChangingEventArgs)
        End Sub

        Protected Overridable Sub SendPropertyChanged(ByVal propertyName As System.[String])
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace
