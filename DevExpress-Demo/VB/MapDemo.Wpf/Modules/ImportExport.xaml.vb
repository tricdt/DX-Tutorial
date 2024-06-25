Imports System
Imports System.ComponentModel
Imports DevExpress.Xpf.Map
Imports DevExpress.Map

Namespace MapDemo

    Public Partial Class ImportExport
        Inherits MapDemoModule
        Implements INotifyPropertyChanged

        Private shapefileWorldResources As ShapefileWorldResources

        Private fileUriField As Uri

        Private zoomLevelField As Integer

        Private centerPointField As CoordPoint

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property FileUri As Uri
            Get
                Return fileUriField
            End Get

            Set(ByVal value As Uri)
                fileUriField = value
                NotifyPropertyChanged("FileUri")
            End Set
        End Property

        Public Property ZoomLevel As Integer
            Get
                Return zoomLevelField
            End Get

            Set(ByVal value As Integer)
                zoomLevelField = value
                NotifyPropertyChanged("ZoomLevel")
            End Set
        End Property

        Public Property CenterPoint As CoordPoint
            Get
                Return centerPointField
            End Get

            Set(ByVal value As CoordPoint)
                centerPointField = value
                NotifyPropertyChanged("CenterPoint")
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
            DataContext = Me
            shapefileWorldResources = New ShapefileWorldResources()
            ZoomLevel = 1
            CenterPoint = New GeoPoint(0, 0)
            FileUri = shapefileWorldResources.CountriesFileUri
        End Sub

        Private Sub NotifyPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Private Sub lbMapType_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            Select Case lbMapType.SelectedItem.ToString()
                Case "World"
                    FileUri = shapefileWorldResources.CountriesFileUri
                    ZoomLevel = 1
                    CenterPoint = New GeoPoint(0, 0)
                Case "Africa"
                    FileUri = shapefileWorldResources.AfricaFileUri
                    CenterPoint = New GeoPoint(0, 20)
                    ZoomLevel = 3
                Case "South America"
                    FileUri = shapefileWorldResources.SouthAmericaFileUri
                    CenterPoint = New GeoPoint(-26.2538, -61.8752)
                    ZoomLevel = 3
                Case "North America"
                    FileUri = shapefileWorldResources.NorthAmericaFileUri
                    CenterPoint = New GeoPoint(60.572, -100.635)
                    ZoomLevel = 2
                Case "Australia"
                    FileUri = shapefileWorldResources.AustraliaFileUri
                    CenterPoint = New GeoPoint(-25.0856, 141.7675)
                    ZoomLevel = 3
                Case "Eurasia"
                    FileUri = shapefileWorldResources.EurasiaFileUri
                    CenterPoint = New GeoPoint(52.8027, 84.8143)
                    ZoomLevel = 2
            End Select
        End Sub
    End Class
End Namespace
