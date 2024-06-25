Imports DevExpress.Xpf.DemoBase.Helpers
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Xml.Serialization

Namespace GridDemo

    <XmlRoot("Countries")>
    Public Class CountriesData
        Inherits List(Of Country)

        Private Shared dataSourceField As List(Of Country) = Nothing

        Public Shared ReadOnly Property DataSource As List(Of Country)
            Get
                If dataSourceField IsNot Nothing Then Return dataSourceField
                Dim s As XmlSerializer = New XmlSerializer(GetType(CountriesData))
                Dim assembly As Assembly = GetType(CountriesData).Assembly
                dataSourceField = CType(s.Deserialize(assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) & "Countries.xml")), List(Of Country))
                Return dataSourceField
            End Get
        End Property
    End Class

    Public Class Country

        Public ReadOnly Property ActualNWindName As String
            Get
                Return If(NWindName, Name)
            End Get
        End Property

        Public ReadOnly Property ActualName As String
            Get
                Return If(Name, NWindName)
            End Get
        End Property

        Public Property Name As String

        Public Property NWindName As String

        Public Property Flag As Byte()
    End Class
End Namespace
