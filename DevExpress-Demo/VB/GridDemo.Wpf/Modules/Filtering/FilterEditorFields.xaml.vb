Imports System.Windows.Controls
Imports DevExpress.Xpf.Core.FilteringUI

Namespace GridDemo.Filtering

    Public Partial Class FilterEditorFields
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "!"
        Private Sub OnQueryFields(ByVal sender As Object, ByVal e As QueryFieldsEventArgs)
            Dim shipCountry = e.Fields("ShipCountry")
            Dim shipCity = e.Fields("ShipCity")
            Dim shipAddress = e.Fields("ShipAddress")
            shipCountry.Caption = "Country"
            shipCity.Caption = "City"
            shipAddress.Caption = "Address"
            e.Fields.Remove(shipCountry)
            e.Fields.Remove(shipCity)
            e.Fields.Remove(shipAddress)
            Dim shipGroup = New FieldItem With {.Caption = "Ship"}
            shipGroup.Children.Add(shipCountry)
            shipGroup.Children.Add(shipCity)
            shipGroup.Children.Add(shipAddress)
            e.Fields.Add(shipGroup)
        End Sub
#End Region
    End Class
End Namespace
