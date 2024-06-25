Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace DockingDemo.Helpers

    Public Module ImagesHelper

        Private _NaturePhotos As IEnumerable(Of String)

        Public Property NaturePhotos As IEnumerable(Of String)
            Get
                Return _NaturePhotos
            End Get

            Private Set(ByVal value As IEnumerable(Of String))
                _NaturePhotos = value
            End Set
        End Property

        Public ReadOnly Property NaturePhoto1 As String
            Get
                Return NaturePhotos.ElementAt(0)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto2 As String
            Get
                Return NaturePhotos.ElementAt(1)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto3 As String
            Get
                Return NaturePhotos.ElementAt(2)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto4 As String
            Get
                Return NaturePhotos.ElementAt(3)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto5 As String
            Get
                Return NaturePhotos.ElementAt(4)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto6 As String
            Get
                Return NaturePhotos.ElementAt(5)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto7 As String
            Get
                Return NaturePhotos.ElementAt(6)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto8 As String
            Get
                Return NaturePhotos.ElementAt(7)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto9 As String
            Get
                Return NaturePhotos.ElementAt(8)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto10 As String
            Get
                Return NaturePhotos.ElementAt(9)
            End Get
        End Property

        Public Function GetRandomNaturePhoto() As String
            Return NaturePhotos.ElementAt(rnd.Next(NaturePhotos.Count() - 1))
        End Function

        Sub New()
            Dim natureUriPath As String = "/DockingDemo;component/Images/Photos/Nature/"
            Dim getNatureImage As Func(Of String, String) = Function(x) natureUriPath & x
            Dim naturePhotos = New List(Of String)()
            naturePhotos.Add(getNatureImage("01.JPG"))
            naturePhotos.Add(getNatureImage("02.JPG"))
            naturePhotos.Add(getNatureImage("03.JPG"))
            naturePhotos.Add(getNatureImage("04.JPG"))
            naturePhotos.Add(getNatureImage("05.JPG"))
            naturePhotos.Add(getNatureImage("06.JPG"))
            naturePhotos.Add(getNatureImage("07.JPG"))
            naturePhotos.Add(getNatureImage("08.JPG"))
            naturePhotos.Add(getNatureImage("09.JPG"))
            naturePhotos.Add(getNatureImage("10.JPG"))
            ImagesHelper.NaturePhotos = naturePhotos
        End Sub

        Private ReadOnly rnd As Random = New Random()
    End Module
End Namespace
