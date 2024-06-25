Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.RichEdit
Imports System.Windows
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views

    Public Partial Class MailMergeView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            Me.richEdit.MailMergeOptions = RichEditMailMergeOptions
            If RichEditBehavior IsNot Nothing Then Call Interaction.GetBehaviors(CType(Me.richEdit, DependencyObject)).Add(RichEditBehavior)
        End Sub

        Public Shared ReadOnly RichEditDocumentSourceProperty As DependencyProperty = DependencyProperty.Register("RichEditDocumentSource", GetType(Object), GetType(MailMergeView), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly AdditionalParametersFormProperty As DependencyProperty = DependencyProperty.Register("AdditionalParametersForm", GetType(FrameworkElement), GetType(MailMergeView), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property RichEditDocumentSource As Object
            Get
                Return GetValue(RichEditDocumentSourceProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(RichEditDocumentSourceProperty, value)
            End Set
        End Property

        Public Property AdditionalParametersForm As FrameworkElement
            Get
                Return CType(GetValue(AdditionalParametersFormProperty), FrameworkElement)
            End Get

            Set(ByVal value As FrameworkElement)
                SetValue(AdditionalParametersFormProperty, value)
            End Set
        End Property

        Public Property RichEditBehavior As Behavior

        Public Property RichEditMailMergeOptions As DXRichEditMailMergeOptions
    End Class
End Namespace
