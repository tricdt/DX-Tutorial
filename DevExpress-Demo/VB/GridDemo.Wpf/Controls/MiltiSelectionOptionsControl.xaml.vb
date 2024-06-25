Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Utils
Imports System
Imports System.Collections
Imports System.Windows
Imports System.Windows.Controls

Namespace GridDemo

    Public Partial Class MultiSelectionOptionsControl
        Inherits UserControl

        Public Shared ReadOnly ComboBoxItemsSourceProperty As DependencyProperty

        Shared Sub New()
            ComboBoxItemsSourceProperty = DependencyPropertyManager.Register("ComboBoxItemsSource", GetType(IEnumerable), GetType(MultiSelectionOptionsControl), New PropertyMetadata(Nothing, AddressOf ComboBoxItemsSourceChanged))
        End Sub

        Private Shared Sub ComboBoxItemsSourceChanged(ByVal dObject As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(dObject, MultiSelectionOptionsControl).OnComboBoxItemsSourceChanged()
        End Sub

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Property ComboBoxItemsSource As IEnumerable
            Get
                Return CType(GetValue(ComboBoxItemsSourceProperty), IEnumerable)
            End Get

            Set(ByVal value As IEnumerable)
                SetValue(ComboBoxItemsSourceProperty, value)
            End Set
        End Property

        Private selectButtonClickHandler As EventHandler

        Public Custom Event SelectButtonClick As EventHandler
            AddHandler(ByVal value As EventHandler)
                selectButtonClickHandler = [Delegate].Combine(selectButtonClickHandler, value)
            End AddHandler

            RemoveHandler(ByVal value As EventHandler)
                selectButtonClickHandler = [Delegate].Remove(selectButtonClickHandler, value)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
                selectButtonClickHandler ?(sender, e)
            End RaiseEvent
        End Event

        Private unselectButtonClickHandler As EventHandler

        Public Custom Event UnselectButtonClick As EventHandler
            AddHandler(ByVal value As EventHandler)
                unselectButtonClickHandler = [Delegate].Combine(unselectButtonClickHandler, value)
            End AddHandler

            RemoveHandler(ByVal value As EventHandler)
                unselectButtonClickHandler = [Delegate].Remove(unselectButtonClickHandler, value)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
                unselectButtonClickHandler ?(sender, e)
            End RaiseEvent
        End Event

        Private reselectButtonClickHandler As EventHandler

        Public Custom Event ReselectButtonClick As EventHandler
            AddHandler(ByVal value As EventHandler)
                reselectButtonClickHandler = [Delegate].Combine(reselectButtonClickHandler, value)
            End AddHandler

            RemoveHandler(ByVal value As EventHandler)
                reselectButtonClickHandler = [Delegate].Remove(reselectButtonClickHandler, value)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
                reselectButtonClickHandler ?(sender, e)
            End RaiseEvent
        End Event

        Private Sub OnComboBoxItemsSourceChanged()
            comboBoxControl.ItemsSource = ComboBoxItemsSource
        End Sub

        Protected Sub RaiseButtonClick(ByVal handler As EventHandler)
            Dim e As EventArgs = New EventArgs()
            If handler IsNot Nothing Then handler(Me, e)
        End Sub

        Public Property Header As String
            Get
                Return Convert.ToString(groupBoxControl.Header)
            End Get

            Set(ByVal value As String)
                groupBoxControl.Header = value
            End Set
        End Property

        Public Property ComboBoxDisplayMember As String
            Get
                Return comboBoxControl.DisplayMember
            End Get

            Set(ByVal value As String)
                comboBoxControl.DisplayMember = value
            End Set
        End Property

        Public Property ComboBoxValueMember As String
            Get
                Return comboBoxControl.ValueMember
            End Get

            Set(ByVal value As String)
                comboBoxControl.ValueMember = value
            End Set
        End Property

        Public ReadOnly Property ComboBox As ComboBoxEdit
            Get
                Return comboBoxControl
            End Get
        End Property

        Private Sub SelectButtonClickInClass(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RaiseButtonClick(selectButtonClickHandler)
        End Sub

        Private Sub UnselectButtonClickInClass(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RaiseButtonClick(unselectButtonClickHandler)
        End Sub

        Private Sub ReselectButtonClickInClass(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RaiseButtonClick(reselectButtonClickHandler)
        End Sub
    End Class
End Namespace
