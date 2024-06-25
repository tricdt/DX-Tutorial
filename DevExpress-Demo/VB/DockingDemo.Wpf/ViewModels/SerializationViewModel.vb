Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Reflection
Imports System.Xml.Serialization
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace DockingDemo.ViewModels

    Public Class SerializableViewModel

        Protected Sub New()
        End Sub

        Public Property Content As Object

        Public Property DisplayName As String

        Public Property Name As String

        Public Shared Function Create() As SerializableViewModel
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New DockingDemo.ViewModels.SerializableViewModel())
        End Function

        Public Shared Function Create(ByVal id As Integer) As SerializableViewModel
            Dim vm = DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New DockingDemo.ViewModels.SerializableViewModel())
            vm.Name = "Model" & id
            vm.DisplayName = "Model " & id
            vm.Content = id
            Return vm
        End Function

        Public Shared Function Create(ByVal info As DockingDemo.ViewModels.SerializableViewModel.SerializationInfo) As SerializableViewModel
            Dim vm = DockingDemo.ViewModels.SerializableViewModel.Create()
            vm.ApplySerializationInfo(info)
            Return vm
        End Function

        Public Function GetSerializationInfo() As SerializationInfo
            Return New DockingDemo.ViewModels.SerializableViewModel.SerializationInfo() With {.Content = Me.Content, .DisplayName = Me.DisplayName, .Name = Me.Name}
        End Function

        Private Sub ApplySerializationInfo(ByVal info As DockingDemo.ViewModels.SerializableViewModel.SerializationInfo)
            Me.Content = info.Content
            Me.DisplayName = info.DisplayName
            Me.Name = info.Name
        End Sub

        Public Class SerializationInfo

            Public Sub New()
            End Sub

            Public Property Content As Object

            Public Property DisplayName As String

            Public Property Name As String
        End Class
    End Class

    Public Class MVVMSerializationViewModel
        Inherits DevExpress.Mvvm.ViewModelBase

        Private _Items As System.Collections.Generic.IList(Of Object)

        Private layoutStream As System.IO.Stream

        Private vmStream As System.IO.Stream

        Public Sub New()
            Me._Items = Me.GenerateItems(5)
        End Sub

        Public ReadOnly Property Items As IList(Of Object)
            Get
                Return Me._Items
            End Get
        End Property

        Public Overridable ReadOnly Property SerializationService As IDockLayoutManagerSerializationService
            Get
                Return Nothing
            End Get
        End Property

        Public Function CanRestore() As Boolean
            Return Me.vmStream IsNot Nothing AndAlso Me.layoutStream IsNot Nothing
        End Function

        Public Sub Restore()
            Me.vmStream.Position = 0
            Me.layoutStream.Position = 0
            Me.RestoreFromStream(Me.vmStream)
            Me.SerializationService.Deserialize(Me.layoutStream)
        End Sub

        Public Sub RestoreSample(ByVal index As Integer)
            Dim assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim vmName As String = String.Format("views{0}.xml", index + 1)
            Using resourceStream As System.IO.Stream = DevExpress.Utils.AssemblyHelper.GetEmbeddedResourceStream(assembly, DevExpress.Xpf.DemoBase.Helpers.DemoHelper.GetPath("Layouts/MVVMSerialization/", assembly) & vmName, True)
                Me.RestoreFromStream(resourceStream)
            End Using

            Dim name As String = String.Format("savedworkspace{0}.xml", index + 1)
            Using resourceStream As System.IO.Stream = DevExpress.Utils.AssemblyHelper.GetEmbeddedResourceStream(assembly, DevExpress.Xpf.DemoBase.Helpers.DemoHelper.GetPath("Layouts/MVVMSerialization/", assembly) & name, True)
                Me.SerializationService.Deserialize(resourceStream)
            End Using
        End Sub

        Public Sub Store()
            If Me.vmStream Is Nothing Then Me.vmStream = New System.IO.MemoryStream()
            If Me.layoutStream Is Nothing Then Me.layoutStream = New System.IO.MemoryStream()
            Me.vmStream.SetLength(0)
            Me.layoutStream.SetLength(0)
            Me.StoreToStream(Me.vmStream)
            Me.SerializationService.Serialize(Me.layoutStream)
        End Sub

        Private Function GenerateItems(ByVal count As Integer) As IList(Of Object)
            Dim items As System.Collections.ObjectModel.ObservableCollection(Of Object) = New System.Collections.ObjectModel.ObservableCollection(Of Object)()
            For i As Integer = 0 To count - 1
                items.Add(DockingDemo.ViewModels.SerializableViewModel.Create(i))
            Next

            Return items
        End Function

        Private Sub RestoreFromStream(ByVal stream As System.IO.Stream)
            Dim serializer As DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer = DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer.Deserialize(stream)
            Me.Items.Clear()
            For Each info In serializer.Infos
                Me.Items.Add(DockingDemo.ViewModels.SerializableViewModel.Create(info))
            Next
        End Sub

        Private Sub StoreToStream(ByVal stream As System.IO.Stream)
            Dim serializer As DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer = New DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer()
            For Each item As DockingDemo.ViewModels.SerializableViewModel In Me.Items
                serializer.Infos.Add(item.GetSerializationInfo())
            Next

            Call DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer.Serialize(stream, serializer)
        End Sub

        <System.Xml.Serialization.XmlRootAttribute("ViewModels")>
        Public Class ViewModelSerializer

            Public Sub New()
                Me.Infos = New System.Collections.Generic.List(Of DockingDemo.ViewModels.SerializableViewModel.SerializationInfo)()
            End Sub

            Public Property Infos As List(Of DockingDemo.ViewModels.SerializableViewModel.SerializationInfo)

            Public Property Name As String

            Public Shared Function Deserialize(ByVal stream As System.IO.Stream) As ViewModelSerializer
                Dim res As DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer
                Dim s As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer), New System.Type() {GetType(DockingDemo.ViewModels.SerializableViewModel.SerializationInfo)})
                res = CType(s.Deserialize(stream), DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer)
                Return res
            End Function

            Public Shared Sub Serialize(ByVal stream As System.IO.Stream, ByVal obj As DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer)
                Dim s As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer), New System.Type() {GetType(DockingDemo.ViewModels.SerializableViewModel.SerializationInfo)})
                s.Serialize(stream, obj)
            End Sub

            Public Shared Sub Serialize(ByVal path As String, ByVal obj As DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer)
                Dim s As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(DockingDemo.ViewModels.MVVMSerializationViewModel.ViewModelSerializer), New System.Type() {GetType(DockingDemo.ViewModels.SerializableViewModel.SerializationInfo)})
                Using st As System.IO.Stream = New System.IO.FileStream(path, System.IO.FileMode.Create)
                    s.Serialize(st, obj)
                End Using
            End Sub
        End Class
    End Class
End Namespace
