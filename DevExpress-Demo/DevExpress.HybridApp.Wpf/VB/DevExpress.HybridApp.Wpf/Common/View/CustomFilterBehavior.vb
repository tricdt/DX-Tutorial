Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.Core.FilteringUI
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Editors.Filtering

Namespace DevExpress.DevAV.Common.View

    Public Class CustomFilterBehavior
        Inherits DevExpress.Xpf.Core.FilteringUI.FilterBehavior

        Public Shared ReadOnly HiddenPropertiesProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.Register("HiddenProperties", GetType(System.Collections.Generic.IEnumerable(Of String)), GetType(DevExpress.DevAV.Common.View.CustomFilterBehavior), New System.Windows.PropertyMetadata(Nothing, Sub(d, e) CType(d, DevExpress.DevAV.Common.View.CustomFilterBehavior).UpdateFields()))

        Public Shared ReadOnly AdditionalPropertiesProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.Register("AdditionalProperties", GetType(DevExpress.Xpf.Editors.Filtering.PropertyInfoCollection), GetType(DevExpress.DevAV.Common.View.CustomFilterBehavior), New System.Windows.PropertyMetadata(Nothing, Sub(d, e) CType(d, DevExpress.DevAV.Common.View.CustomFilterBehavior).UpdateFields()))

        Public Shared ReadOnly ObjectTypeProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.Register("ObjectType", GetType(System.Type), GetType(DevExpress.DevAV.Common.View.CustomFilterBehavior), New System.Windows.PropertyMetadata(Nothing, Sub(d, e) CType(d, DevExpress.DevAV.Common.View.CustomFilterBehavior).UpdateFields()))

        Public Property HiddenProperties As IEnumerable(Of String)
            Get
                Return CType(Me.GetValue(DevExpress.DevAV.Common.View.CustomFilterBehavior.HiddenPropertiesProperty), System.Collections.Generic.IEnumerable(Of String))
            End Get

            Set(ByVal value As IEnumerable(Of String))
                Me.SetValue(DevExpress.DevAV.Common.View.CustomFilterBehavior.HiddenPropertiesProperty, value)
            End Set
        End Property

        Public Property AdditionalProperties As PropertyInfoCollection
            Get
                Return CType(Me.GetValue(DevExpress.DevAV.Common.View.CustomFilterBehavior.AdditionalPropertiesProperty), DevExpress.Xpf.Editors.Filtering.PropertyInfoCollection)
            End Get

            Set(ByVal value As PropertyInfoCollection)
                Me.SetValue(DevExpress.DevAV.Common.View.CustomFilterBehavior.AdditionalPropertiesProperty, value)
            End Set
        End Property

        Public Property ObjectType As Type
            Get
                Return CType(Me.GetValue(DevExpress.DevAV.Common.View.CustomFilterBehavior.ObjectTypeProperty), System.Type)
            End Get

            Set(ByVal value As Type)
                Me.SetValue(DevExpress.DevAV.Common.View.CustomFilterBehavior.ObjectTypeProperty, value)
            End Set
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Me.UpdateFields()
        End Sub

        Private Sub UpdateFields()
            If Me.AssociatedObject Is Nothing OrElse Me.ObjectType Is Nothing Then Return
            Me.UpdateContext()
            Me.Fields.Clear()
            Dim fieldsInfo = DevExpress.Xpf.DemoBase.FilterFieldsHelper.GetFields(Me.AssociatedObject, Me.ObjectType, If(Me.HiddenProperties, System.Linq.Enumerable.Empty(Of String)()), If(Me.AdditionalProperties, New DevExpress.Xpf.Editors.Filtering.PropertyInfoCollection())).Where(Function(x) Not x.FieldName.EndsWith("Id"))
            For Each field In fieldsInfo
                Me.Fields.Add(New DevExpress.Xpf.Core.FilteringUI.FilterField() With {.Caption = field.Caption, .EditSettings = field.EditSettings, .FieldName = field.FieldName})
            Next
        End Sub

        Private Sub UpdateContext()
            Dim type = GetType(System.Collections.Generic.List(Of )).MakeGenericType(Me.ObjectType)
            Me.ItemsSource = System.Activator.CreateInstance(type)
        End Sub
    End Class
End Namespace
