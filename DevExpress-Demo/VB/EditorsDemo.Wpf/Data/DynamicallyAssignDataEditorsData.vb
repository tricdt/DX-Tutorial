Imports System.ComponentModel
Imports System.Windows.Media
Imports DevExpress.Xpf.Editors
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Mvvm.DataAnnotations

Namespace EditorsDemo

    <MetadataType(GetType(DynamicallyAssignDataEditorsMetadata))>
    Public Class DynamicallyAssignDataEditorsData

        Const SimpleEditorsGroup As String = "Simple editors"

        Const PopupEditorsGroup As String = "Popup editors"

        Const MiscEditorsGroup As String = "Misc editors"

        Public Sub New()
            Dim list = New PropertyGridMultiEditorsList()
            For Each [property] As PropertyDescriptor In TypeDescriptor.GetProperties(Me)
                [property].SetValue(Me, list.GetValue([property].Name))
            Next
        End Sub

        <Display(Name:="TextEdit", GroupName:=SimpleEditorsGroup)>
        Public Property Name As String

        <Display(Name:="TextEdit (with numeric mask)", Description:="NumericTextEdit", GroupName:=SimpleEditorsGroup)>
        Public Property ID As Long

        <Display(Name:="TextEdit (with RegEx mask)", Description:="RegExTextEdit", GroupName:=SimpleEditorsGroup)>
        Public Property Code As String

        <Display(Name:="PasswordBoxEdit", GroupName:=SimpleEditorsGroup)>
        Public Property Password As String

        <Display(Name:="ButtonEdit", GroupName:=SimpleEditorsGroup)>
        Public Property Countries As String

        <Display(Name:="AutoSuggestEdit", GroupName:=PopupEditorsGroup)>
        Public Property Product As String

        <Display(Name:="ComboBoxEdit (with AutoComplete)", Description:="AutoCompleteComboBoxEdit", GroupName:=PopupEditorsGroup)>
        Public Property Country As String

        <Display(Name:="SearchLookUpEdit", GroupName:=PopupEditorsGroup)>
        Public Property Category1 As Long

        <Display(Name:="LookUpEdit", GroupName:=PopupEditorsGroup)>
        Public Property Category2 As Long

        <Display(Name:="MemoEdit", GroupName:=PopupEditorsGroup)>
        Public Property Notes As String

        <Display(Name:="DateEdit", GroupName:=PopupEditorsGroup)>
        Public Property Date1 As Date

        <Display(Name:="DatePicker", GroupName:=PopupEditorsGroup)>
        Public Property Date2 As Date

        <Display(Name:="FontEdit", GroupName:=PopupEditorsGroup)>
        Public Property Font As FontFamily

        <Display(Name:="PopupCalcEdit", GroupName:=PopupEditorsGroup)>
        Public Property UnitPrice As Decimal

        <Display(Name:="PopupColorEdit", GroupName:=PopupEditorsGroup)>
        Public Property Color As Color

        <Display(Name:="PopupBrushEdit", GroupName:=PopupEditorsGroup)>
        Public Property Brush As Brush

        <Display(Name:="PopupImageEdit", GroupName:=PopupEditorsGroup)>
        Public Property Picture As ImageSource

        <Display(Name:="TrackBarEdit", GroupName:=MiscEditorsGroup)>
        Public Property UnitsInStock As Double

        <Display(Name:="ZoomTrackBarEdit", GroupName:=MiscEditorsGroup)>
        Public Property ReorderLevel As Double

        <Display(Name:="RangeTrackBarEdit", GroupName:=MiscEditorsGroup)>
        Public Property Range As TrackBarEditRange

        <Display(Name:="ProgressBarEdit", GroupName:=MiscEditorsGroup)>
        Public Property UnitsOnOrder As Double

        <Display(Name:="CheckEdit", GroupName:=MiscEditorsGroup)>
        Public Property Discontinued As Boolean

        <Display(Name:="ToggleSwitchEdit", GroupName:=MiscEditorsGroup)>
        Public Property Discontinued2 As Boolean

        <Display(Name:="SpinEdit", GroupName:=MiscEditorsGroup)>
        Public Property Discount As Double

        <Display(Name:="ListBoxEdit", GroupName:=MiscEditorsGroup)>
        Public Property PalleteSize As String

        <Display(Name:="BarCodeEdit", GroupName:=MiscEditorsGroup)>
        Public Property Data As Integer

        <Display(Name:="RatingEdit", GroupName:=MiscEditorsGroup)>
        Public Property Rating As Double

        <Display(Name:="HyperlinkEdit", GroupName:=MiscEditorsGroup)>
        Public Property HyperLink As String
    End Class

    Public Module DynamicallyAssignDataEditorsMetadata

        Public Sub BuildMetadata(ByVal builder As MetadataBuilder(Of DynamicallyAssignDataEditorsData))
            builder.[Property](Function(x) x.Password).PasswordDataType()
            builder.[Property](Function(x) x.Notes).MultilineTextDataType()
        End Sub
    End Module
End Namespace
