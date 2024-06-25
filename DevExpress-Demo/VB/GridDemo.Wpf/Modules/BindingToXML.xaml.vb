Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System.Xml

Namespace GridDemo

    Public Partial Class BindingToXML
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            Dim document As XmlDocument = New XmlDocument()
            document.Load(EmployeesWithPhotoData.GetDataStream())
            grid.ItemsSource = document.SelectNodes("/Employees/Employee")
        End Sub
    End Class
End Namespace
