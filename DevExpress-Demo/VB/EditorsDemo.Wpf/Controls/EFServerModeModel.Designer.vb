Imports System
Imports System.Data.Objects
Imports System.Data.Objects.DataClasses
Imports System.Data.EntityClient
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.Runtime.Serialization

<Assembly:System.Data.Objects.DataClasses.EdmSchemaAttribute()>
Namespace GridDemo.Controls

#Region "Contexts"
    Public Partial Class DXGridServerModeDBEntities
        Inherits System.Data.Objects.ObjectContext

#Region "Constructors"
        Public Sub New()
            MyBase.New("name=DXGridServerModeDBEntities", "DXGridServerModeDBEntities")
            Me.ContextOptions.LazyLoadingEnabled = True
            Me.OnContextCreated()
        End Sub

        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString, "DXGridServerModeDBEntities")
            Me.ContextOptions.LazyLoadingEnabled = True
            Me.OnContextCreated()
        End Sub

        Public Sub New(ByVal connection As System.Data.EntityClient.EntityConnection)
            MyBase.New(connection, "DXGridServerModeDBEntities")
            Me.ContextOptions.LazyLoadingEnabled = True
            Me.OnContextCreated()
        End Sub

#End Region
#Region "Partial Methods"
        Partial Private Sub OnContextCreated()
        End Sub

#End Region
#Region "ObjectSet Properties"
        Public ReadOnly Property EFServerModeTestClasses As ObjectSet(Of GridDemo.Controls.EFServerModeTestClass)
            Get
                If(Me._EFServerModeTestClasses Is Nothing) Then
                    Me._EFServerModeTestClasses = Me.CreateObjectSet(Of GridDemo.Controls.EFServerModeTestClass)("EFServerModeTestClasses")
                End If

                Return Me._EFServerModeTestClasses
            End Get
        End Property

        Private _EFServerModeTestClasses As System.Data.Objects.ObjectSet(Of GridDemo.Controls.EFServerModeTestClass)

#End Region
#Region "AddTo Methods"
        Public Sub AddToEFServerModeTestClasses(ByVal eFServerModeTestClass As GridDemo.Controls.EFServerModeTestClass)
            Me.AddObject("EFServerModeTestClasses", eFServerModeTestClass)
        End Sub
#End Region
    End Class

#End Region
#Region "Entities"
    <System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName:="DXGridServerModeDBModel", Name:="EFServerModeTestClass")>
    <System.SerializableAttribute()>
    <System.Runtime.Serialization.DataContractAttribute(IsReference:=True)>
    Public Partial Class EFServerModeTestClass
        Inherits System.Data.Objects.DataClasses.EntityObject

#Region "Factory Method"
        Public Shared Function CreateEFServerModeTestClass(ByVal oID As Global.System.Int32) As EFServerModeTestClass
            Dim eFServerModeTestClass As GridDemo.Controls.EFServerModeTestClass = New GridDemo.Controls.EFServerModeTestClass()
            eFServerModeTestClass.OID = oID
            Return eFServerModeTestClass
        End Function

#End Region
#Region "Primitive Properties"
        <System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=True, IsNullable:=False)>
        <System.Runtime.Serialization.DataMemberAttribute()>
        Public Property OID As Global.System.Int32
            Get
                Return Me._OID
            End Get

            Set(ByVal value As Global.System.Int32)
                If Me._OID <> value Then
                    Me.OnOIDChanging(value)
                    Me.ReportPropertyChanging("OID")
                    Me._OID = System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                    Me.ReportPropertyChanged("OID")
                    Me.OnOIDChanged()
                End If
            End Set
        End Property

        Private _OID As Global.System.Int32

        Partial Private Sub OnOIDChanging(ByVal value As Global.System.Int32)
        End Sub

        Partial Private Sub OnOIDChanged()
        End Sub

        <System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=False, IsNullable:=True)>
        <System.Runtime.Serialization.DataMemberAttribute()>
        Public Property Subject As Global.System.[String]
            Get
                Return Me._Subject
            End Get

            Set(ByVal value As Global.System.[String])
                Me.OnSubjectChanging(value)
                Me.ReportPropertyChanging("Subject")
                Me._Subject = System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, True)
                Me.ReportPropertyChanged("Subject")
                Me.OnSubjectChanged()
            End Set
        End Property

        Private _Subject As Global.System.[String]

        Partial Private Sub OnSubjectChanging(ByVal value As Global.System.[String])
        End Sub

        Partial Private Sub OnSubjectChanged()
        End Sub

        <System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=False, IsNullable:=True)>
        <System.Runtime.Serialization.DataMemberAttribute()>
        Public Property From As Global.System.[String]
            Get
                Return Me._From
            End Get

            Set(ByVal value As Global.System.[String])
                Me.OnFromChanging(value)
                Me.ReportPropertyChanging("From")
                Me._From = System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, True)
                Me.ReportPropertyChanged("From")
                Me.OnFromChanged()
            End Set
        End Property

        Private _From As Global.System.[String]

        Partial Private Sub OnFromChanging(ByVal value As Global.System.[String])
        End Sub

        Partial Private Sub OnFromChanged()
        End Sub

        <System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=False, IsNullable:=True)>
        <System.Runtime.Serialization.DataMemberAttribute()>
        Public Property Sent As Nullable(Of Global.System.DateTime)
            Get
                Return Me._Sent
            End Get

            Set(ByVal value As Nullable(Of Global.System.DateTime))
                Me.OnSentChanging(value)
                Me.ReportPropertyChanging("Sent")
                Me._Sent = System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("Sent")
                Me.OnSentChanged()
            End Set
        End Property

        Private _Sent As System.Nullable(Of Global.System.DateTime)

        Partial Private Sub OnSentChanging(ByVal value As System.Nullable(Of Global.System.DateTime))
        End Sub

        Partial Private Sub OnSentChanged()
        End Sub

        <System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=False, IsNullable:=True)>
        <System.Runtime.Serialization.DataMemberAttribute()>
        Public Property Size As Nullable(Of Global.System.Int64)
            Get
                Return Me._Size
            End Get

            Set(ByVal value As Nullable(Of Global.System.Int64))
                Me.OnSizeChanging(value)
                Me.ReportPropertyChanging("Size")
                Me._Size = System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("Size")
                Me.OnSizeChanged()
            End Set
        End Property

        Private _Size As System.Nullable(Of Global.System.Int64)

        Partial Private Sub OnSizeChanging(ByVal value As System.Nullable(Of Global.System.Int64))
        End Sub

        Partial Private Sub OnSizeChanged()
        End Sub

        <System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=False, IsNullable:=True)>
        <System.Runtime.Serialization.DataMemberAttribute()>
        Public Property HasAttachment As Nullable(Of Global.System.[Boolean])
            Get
                Return Me._HasAttachment
            End Get

            Set(ByVal value As Nullable(Of Global.System.[Boolean]))
                Me.OnHasAttachmentChanging(value)
                Me.ReportPropertyChanging("HasAttachment")
                Me._HasAttachment = System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("HasAttachment")
                Me.OnHasAttachmentChanged()
            End Set
        End Property

        Private _HasAttachment As System.Nullable(Of Global.System.[Boolean])

        Partial Private Sub OnHasAttachmentChanging(ByVal value As System.Nullable(Of Global.System.[Boolean]))
        End Sub

        Partial Private Sub OnHasAttachmentChanged()
        End Sub
#End Region
    End Class
#End Region
End Namespace
