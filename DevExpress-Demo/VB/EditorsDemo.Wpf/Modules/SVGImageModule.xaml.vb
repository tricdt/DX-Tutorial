Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Editors
Imports System.Windows
Imports System.Windows.Controls

Namespace EditorsDemo

    Public Partial Class SVGImageModule

        Public Sub New()
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata(Of EnumMetadataBuilder)()
            InitializeComponent()
        End Sub

        Private Sub cmbGlyphSizesEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            editor.EditValue = 1
        End Sub
    End Class

    Public Class EnumMetadataBuilder

        Public Shared Sub BuildMetadata(ByVal builder As EnumMetadataBuilder(Of HorizontalAlignment))
            builder.Member(HorizontalAlignment.Center).ImageUri("pack://application:,,,/Images/Svg/CenterAlignment.svg").EndMember().Member(HorizontalAlignment.Left).ImageUri("pack://application:,,,/Images/Svg/LeftAlignment.svg").EndMember().Member(HorizontalAlignment.Right).ImageUri("pack://application:,,,/Images/Svg/RightAlignment.svg").EndMember().Member(HorizontalAlignment.Stretch).ImageUri("pack://application:,,,/Images/Svg/StretchAlignment.svg").EndMember()
        End Sub
    End Class
End Namespace
