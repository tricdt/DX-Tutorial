Imports DevExpress.Mvvm.DataAnnotations
Imports System.ComponentModel

Namespace PropertyGridDemo

    Public Class AttributesSupportData

        <Browsable(False)>
        Public Property ID As Integer

        <DefaultValueAttribute(18)>
        <NumericMask(Mask:=PredefinedMasks.Numeric.Decimal & "2")>
        Public Property Age As Integer

        <DateTimeMask(Mask:=PredefinedMasks.DateTime.LongDate)>
        Public Property HireDate As Date

        Const SSNMask As String = "\d{3}-\d{2}-\d{4}"

        <RegExMask(Mask:=SSNMask)>
        Public Property SSN As String

        <DisplayName("First name"), RefreshProperties(RefreshProperties.All)>
        Public Property FirstName As String

        <DisplayName("Last name"), RefreshProperties(RefreshProperties.All)>
        Public Property LastName As String

        <DisplayName("Full name")>
        Public ReadOnly Property FullName As String
            Get
                Dim delimiter As String = If(String.IsNullOrEmpty(FirstName), String.Empty, " ")
                Return FirstName & delimiter & LastName
            End Get
        End Property

        <ReadOnlyAttribute(True)>
        Public Property Gender As Gender?
    End Class
End Namespace
