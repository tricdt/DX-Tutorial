Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors

Namespace LayoutControlDemo

    <TemplatePart(Name:=ThicknessMemberEdit.EditorElementName, Type:=GetType(ComboBoxEdit))>
    Public Class ThicknessMemberEdit
        Inherits Control

        Private _EditorElement As ComboBoxEdit

#Region "Dependency Properties"
        Public Shared ReadOnly ThicknessProperty As DependencyProperty = DependencyProperty.Register("Thickness", GetType(Thickness), GetType(ThicknessMemberEdit), New PropertyMetadata(New PropertyChangedCallback(AddressOf LayoutControlDemo.ThicknessMemberEdit.OnThicknessChanged)))

        Private Shared Sub OnThicknessChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(o, ThicknessMemberEdit).OnThicknessChanged()
        End Sub

#End Region  ' Dependency Properties
        Private _Side As Side

        Public Sub New()
            DefaultStyleKey = GetType(ThicknessMemberEdit)
        End Sub

        Public Property Side As Side
            Get
                Return _Side
            End Get

            Set(ByVal value As Side)
                If Side.Equals(value) Then Return
                _Side = value
                UpdateSelectedItem()
            End Set
        End Property

        Public Property Thickness As Thickness
            Get
                Return CType(GetValue(ThicknessProperty), Thickness)
            End Get

            Set(ByVal value As Thickness)
                SetValue(ThicknessProperty, value)
            End Set
        End Property

#Region "Template"
        Friend Const EditorElementName As String = "EditorElement"

        Public Overrides Sub OnApplyTemplate()
            If EditorElement IsNot Nothing Then RemoveHandler EditorElement.SelectedIndexChanged, AddressOf OnEditorElementSelectedIndexChanged
            MyBase.OnApplyTemplate()
            EditorElement = TryCast(GetTemplateChild(EditorElementName), ComboBoxEdit)
            If EditorElement IsNot Nothing Then AddHandler EditorElement.SelectedIndexChanged, AddressOf OnEditorElementSelectedIndexChanged
            UpdateSelectedItem()
        End Sub

        Protected Property EditorElement As ComboBoxEdit
            Get
                Return _EditorElement
            End Get

            Private Set(ByVal value As ComboBoxEdit)
                _EditorElement = value
            End Set
        End Property

        Private Sub OnEditorElementSelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            OnSelectionChanged()
        End Sub

#End Region  ' Template
        Protected Overridable Sub OnSelectionChanged()
            UpdateThickness()
        End Sub

        Protected Overridable Sub OnThicknessChanged()
            UpdateSelectedItem()
        End Sub

        Protected Sub UpdateSelectedItem()
            If EditorElement IsNot Nothing Then EditorElement.SelectedItem = Thickness.GetValue(Side)
        End Sub

        Protected Sub UpdateThickness()
            If EditorElement IsNot Nothing Then Thickness = Thickness.ChangeValue(Side, CDbl(EditorElement.SelectedItem))
        End Sub
    End Class

    Public Class ThicknessEdit
        Inherits Control

#Region "Dependency Properties"
        Public Shared ReadOnly ValueProperty As DependencyProperty = DependencyProperty.Register("Value", GetType(Thickness), GetType(ThicknessEdit), Nothing)

#End Region  ' Dependency Properties
        Public Sub New()
            DefaultStyleKey = GetType(ThicknessEdit)
        End Sub

        Public Property Value As Thickness
            Get
                Return CType(GetValue(ValueProperty), Thickness)
            End Get

            Set(ByVal value As Thickness)
                SetValue(ValueProperty, value)
            End Set
        End Property
    End Class
End Namespace
