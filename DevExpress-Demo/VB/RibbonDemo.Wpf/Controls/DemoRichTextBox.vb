Imports System
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Documents
Imports DevExpress.Mvvm
Imports System.Windows.Input
Imports System.Drawing.Printing

Namespace RibbonDemo

    Public Class DemoRichControl
        Inherits Windows.Controls.RichTextBox

#Region "dependency properties"
        Public Shared ReadOnly IsBoldProperty As DependencyProperty = DependencyProperty.Register("IsBold", GetType(Boolean?), GetType(DemoRichControl), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnIsBoldPropertyChanged)))

        Public Shared ReadOnly IsItalicProperty As DependencyProperty = DependencyProperty.Register("IsItalic", GetType(Boolean?), GetType(DemoRichControl), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnIsItalicPropertyChanged)))

        Public Shared ReadOnly IsUnderlineProperty As DependencyProperty = DependencyProperty.Register("IsUnderline", GetType(Boolean?), GetType(DemoRichControl), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnIsUnderlinePropertyChanged)))

        Public Shared ReadOnly SelectionTextProperty As DependencyProperty = DependencyProperty.Register("SelectionText", GetType(String), GetType(DemoRichControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly SelectionFontFamilyProperty As DependencyProperty = DependencyProperty.Register("SelectionFontFamily", GetType(String), GetType(DemoRichControl), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnSelectionFontFamilyPropertyChanged)))

        Public Shared ReadOnly SelectionFontSizeProperty As DependencyProperty = DependencyProperty.Register("SelectionFontSize", GetType(Double?), GetType(DemoRichControl), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnSelectionFontSizePropertyChanged)))

        Public Shared ReadOnly SelectionTextColorProperty As DependencyProperty = DependencyProperty.Register("SelectionTextColor", GetType(Color), GetType(DemoRichControl), New PropertyMetadata(Colors.Black, New PropertyChangedCallback(AddressOf OnSelectionTextColorPropertyChanged)))

        Public Shared ReadOnly SelectionTextBackgroundColorProperty As DependencyProperty = DependencyProperty.Register("SelectionTextBackgroundColor", GetType(Color), GetType(DemoRichControl), New PropertyMetadata(Colors.Black, New PropertyChangedCallback(AddressOf OnSelectionTextBackgroundColorPropertyChanged)))

        Public Shared ReadOnly IsRightAlignmentProperty As DependencyProperty = DependencyProperty.Register("IsRightAlignment", GetType(Boolean), GetType(DemoRichControl), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnIsRightAlignmentPropertyChanged)))

        Public Shared ReadOnly IsCenterAlignmentProperty As DependencyProperty = DependencyProperty.Register("IsCenterAlignment", GetType(Boolean), GetType(DemoRichControl), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnIsCenterAlignmentPropertyChanged)))

        Public Shared ReadOnly IsLeftAlignmentProperty As DependencyProperty = DependencyProperty.Register("IsLeftAlignment", GetType(Boolean), GetType(DemoRichControl), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnIsLeftAlignmentPropertyChanged)))

        Public Shared ReadOnly IsEmptyProperty As DependencyProperty = DependencyProperty.Register("IsEmpty", GetType(Boolean), GetType(DemoRichControl), New PropertyMetadata(True, New PropertyChangedCallback(AddressOf OnIsEmptyPropertyChanged)))

        Public Shared ReadOnly IsSelectionEmptyProperty As DependencyProperty = DependencyProperty.Register("IsSelectionEmpty", GetType(Boolean), GetType(DemoRichControl), New PropertyMetadata(True))

        Public Shared ReadOnly ListMarkerStyleProperty As DependencyProperty = DependencyProperty.Register("ListMarkerStyle", GetType(TextMarkerStyle), GetType(DemoRichControl), New PropertyMetadata(TextMarkerStyle.None, New PropertyChangedCallback(AddressOf OnListMarkerStylePropertyChanged)))

        Public Shared ReadOnly ContainerProperty As DependencyProperty = DependencyProperty.Register("Container", GetType(InlineUIContainer), GetType(DemoRichControl), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnContainerPropertyChanged)))

        Public Shared ReadOnly IsListProperty As DependencyProperty = DependencyProperty.Register("IsList", GetType(Boolean?), GetType(DemoRichControl), New PropertyMetadata(False, New PropertyChangedCallback(AddressOf OnIsListPropertyChanged)))

        Protected Shared Sub OnIsBoldPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnIsBoldChanged()
        End Sub

        Protected Shared Sub OnIsItalicPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnIsItalicChanged()
        End Sub

        Protected Shared Sub OnIsUnderlinePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnIsUnderlineChanged()
        End Sub

        Protected Shared Sub OnSelectionFontFamilyPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnSelectionFontFamilyChanged()
        End Sub

        Protected Shared Sub OnSelectionFontSizePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnSelectionFontSizeChanged()
        End Sub

        Protected Shared Sub OnSelectionTextColorPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnSelectionTextColorChanged()
        End Sub

        Protected Shared Sub OnSelectionTextBackgroundColorPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnSelectionTextBackgroundColorChanged()
        End Sub

        Protected Shared Sub OnIsRightAlignmentPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnIsRightAlignmentChanged()
        End Sub

        Protected Shared Sub OnIsLeftAlignmentPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnIsLeftAlignmentChanged()
        End Sub

        Protected Shared Sub OnIsCenterAlignmentPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnIsCenterAlignmentChanged()
        End Sub

        Protected Shared Sub OnListMarkerStylePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnListMarkerStyleChanged()
        End Sub

        Protected Shared Sub OnContainerPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnContainerChanged()
        End Sub

        Protected Shared Sub OnIsEmptyPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnIsEmptyChanged()
        End Sub

        Protected Shared Sub OnIsListPropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            CType(d, DemoRichControl).OnIsListChanged()
        End Sub

        Public Property IsBold As Boolean?
            Get
                Return CType(GetValue(IsBoldProperty), Boolean?)
            End Get

            Set(ByVal value As Boolean?)
                SetValue(IsBoldProperty, value)
            End Set
        End Property

        Public Property IsItalic As Boolean?
            Get
                Return CType(GetValue(IsItalicProperty), Boolean?)
            End Get

            Set(ByVal value As Boolean?)
                SetValue(IsItalicProperty, value)
            End Set
        End Property

        Public Property IsUnderline As Boolean?
            Get
                Return CType(GetValue(IsUnderlineProperty), Boolean?)
            End Get

            Set(ByVal value As Boolean?)
                SetValue(IsUnderlineProperty, value)
            End Set
        End Property

        Public Property SelectionText As String
            Get
                Return CStr(GetValue(SelectionTextProperty))
            End Get

            Set(ByVal value As String)
                SetValue(SelectionTextProperty, value)
            End Set
        End Property

        Public Property SelectionFontFamily As String
            Get
                Return CStr(GetValue(SelectionFontFamilyProperty))
            End Get

            Set(ByVal value As String)
                SetValue(SelectionFontFamilyProperty, value)
            End Set
        End Property

        Public Property SelectionFontSize As Double?
            Get
                Return CType(GetValue(SelectionFontSizeProperty), Double?)
            End Get

            Set(ByVal value As Double?)
                SetValue(SelectionFontSizeProperty, value)
            End Set
        End Property

        Public Property SelectionTextColor As Color
            Get
                Return CType(GetValue(SelectionTextColorProperty), Color)
            End Get

            Set(ByVal value As Color)
                SetValue(SelectionTextColorProperty, value)
            End Set
        End Property

        Public Property SelectionTextBackgroundColor As Color
            Get
                Return CType(GetValue(SelectionTextBackgroundColorProperty), Color)
            End Get

            Set(ByVal value As Color)
                SetValue(SelectionTextBackgroundColorProperty, value)
            End Set
        End Property

        Public Property IsRightAlignment As Boolean
            Get
                Return CBool(GetValue(IsRightAlignmentProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsRightAlignmentProperty, value)
            End Set
        End Property

        Public Property IsCenterAlignment As Boolean
            Get
                Return CBool(GetValue(IsCenterAlignmentProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsCenterAlignmentProperty, value)
            End Set
        End Property

        Public Property IsLeftAlignment As Boolean
            Get
                Return CBool(GetValue(IsLeftAlignmentProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsLeftAlignmentProperty, value)
            End Set
        End Property

        Public Property IsEmpty As Boolean
            Get
                Return CBool(GetValue(IsEmptyProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsEmptyProperty, value)
            End Set
        End Property

        Public Property IsSelectionEmpty As Boolean
            Get
                Return CBool(GetValue(IsSelectionEmptyProperty))
            End Get

            Set(ByVal value As Boolean)
                SetValue(IsSelectionEmptyProperty, value)
            End Set
        End Property

        Public Property ListMarkerStyle As TextMarkerStyle
            Get
                Return CType(GetValue(ListMarkerStyleProperty), TextMarkerStyle)
            End Get

            Set(ByVal value As TextMarkerStyle)
                SetValue(ListMarkerStyleProperty, value)
            End Set
        End Property

        Public Property Container As InlineUIContainer
            Get
                Return CType(GetValue(ContainerProperty), InlineUIContainer)
            End Get

            Set(ByVal value As InlineUIContainer)
                SetValue(ContainerProperty, value)
            End Set
        End Property

        Public Property IsList As Boolean?
            Get
                Return CType(GetValue(IsListProperty), Boolean?)
            End Get

            Set(ByVal value As Boolean?)
                SetValue(IsListProperty, value)
            End Set
        End Property

#End Region
#Region "commands"
        Public Property SelectAllCommand As ICommand

        Public Property ClearCommand As ICommand

        Public Property PrintCommand As ICommand

        Public Property CopyCommand As ICommand

        Public Property PasteCommand As ICommand

        Public Property CutCommand As ICommand

#End Region
        Public Sub New()
            ClearCommand = New DelegateCommand(AddressOf ClearCommandExecute, AddressOf CanClearCommandExecute)
            PrintCommand = New DelegateCommand(AddressOf PrintCommandExecute)
            CopyCommand = ApplicationCommands.Copy
            PasteCommand = ApplicationCommands.Paste
            CutCommand = ApplicationCommands.Cut
            SelectAllCommand = ApplicationCommands.SelectAll
            UndoLimit = 0
        End Sub

        Public Event ContainerChanged As EventHandler

        Private Property IsUpdating As Boolean

        Protected Sub OnIsBoldChanged()
            If Not IsUpdating Then IsBoldCore = IsBold
        End Sub

        Protected Sub OnIsItalicChanged()
            If Not IsUpdating Then IsItalicCore = IsItalic
        End Sub

        Protected Sub OnIsUnderlineChanged()
            If Not IsUpdating Then IsUnderlineCore = IsUnderline
        End Sub

        Protected Sub OnSelectionFontFamilyChanged()
            If Not IsUpdating Then SelectionFontFamilyCore = SelectionFontFamily
        End Sub

        Protected Sub OnSelectionFontSizeChanged()
            If Not IsUpdating Then SelectionFontSizeCore = SelectionFontSize
        End Sub

        Protected Sub OnSelectionTextBackgroundColorChanged()
            If Not IsUpdating Then SelectionTextBackgroundColorCore = SelectionTextBackgroundColor
        End Sub

        Protected Sub OnSelectionTextColorChanged()
            If Not IsUpdating Then SelectionTextColorCore = SelectionTextColor
        End Sub

        Protected Sub OnIsRightAlignmentChanged()
            If IsUpdating Then Return
            If IsRightAlignment Then
                IsLeftAlignment = False
                IsCenterAlignment = False
                TextAlignmentCore = TextAlignment.Right
            End If
        End Sub

        Protected Sub OnContainerChanged()
            RaiseEvent ContainerChanged(Me, New EventArgs())
        End Sub

        Protected Sub OnListMarkerStyleChanged()
            If Not IsUpdating Then
                ListMarkerStyleCore = ListMarkerStyle
                IsUpdating = True
                IsList = ListMarkerStyle <> TextMarkerStyle.None
                IsUpdating = False
            Else
                IsList = ListMarkerStyle <> TextMarkerStyle.None
            End If
        End Sub

        Protected Sub OnIsListChanged()
            If IsUpdating Then Return
            ListMarkerStyle = If(IsList.Value, TextMarkerStyle.Disc, TextMarkerStyle.None)
        End Sub

        Protected Sub OnIsEmptyChanged()
            CType(ClearCommand, DelegateCommand).RaiseCanExecuteChanged()
        End Sub

        Protected Sub OnIsLeftAlignmentChanged()
            If IsUpdating Then Return
            If IsLeftAlignment Then
                IsRightAlignment = False
                IsCenterAlignment = False
                TextAlignmentCore = TextAlignment.Left
            End If
        End Sub

        Protected Sub OnIsCenterAlignmentChanged()
            If IsUpdating Then Return
            If IsCenterAlignment Then
                IsLeftAlignment = False
                IsRightAlignment = False
                TextAlignmentCore = TextAlignment.Center
            End If
        End Sub

        Protected Overrides Sub OnKeyUp(ByVal e As Input.KeyEventArgs)
            MyBase.OnKeyUp(e)
            UpdateSelectionProperties()
        End Sub

        Protected Overrides Sub OnSelectionChanged(ByVal e As RoutedEventArgs)
            MyBase.OnSelectionChanged(e)
            UpdateSelectionProperties()
        End Sub

        Public Sub SetFocus()
            Focus()
            UpdateSelectionProperties()
        End Sub

        Protected Sub UpdateSelectionProperties()
            IsUpdating = True
            IsSelectionEmpty = IsSelectionEmptyCore
            IsRightAlignment = Equals(TextAlignmentCore, TextAlignment.Right)
            IsLeftAlignment = Equals(TextAlignmentCore, TextAlignment.Left)
            IsCenterAlignment = Equals(TextAlignmentCore, TextAlignment.Center)
            IsBold = IsBoldCore
            IsItalic = IsItalicCore
            IsUnderline = IsUnderlineCore
            SelectionFontSize = SelectionFontSizeCore
            SelectionFontFamily = SelectionFontFamilyCore
            SelectionTextColor = SelectionTextColorCore
            SelectionTextBackgroundColor = SelectionTextBackgroundColorCore
            Container = GetUIElementUnderSelection()
            ListMarkerStyle = ListMarkerStyleCore
            IsEmpty = IsEmptyCore
            IsSelectionEmpty = IsSelectionEmptyCore
            IsUpdating = False
        End Sub

#Region "commands implementation"
        Protected Function CanClearCommandExecute() As Boolean
            Return Not IsEmpty
        End Function

        Protected Sub ClearCommandExecute()
            TryCast(Document, FlowDocument).Blocks.Clear()
        End Sub

        Protected Sub PrintCommandExecute()
            Dim dialog As Windows.Controls.PrintDialog = New Windows.Controls.PrintDialog()
            If dialog.ShowDialog() <> True Then Return
            dialog.PrintVisual(Me, String.Empty)
        End Sub

#End Region
#Region "core properties"
        Public Property SelectionFontFamilyCore As String
            Get
                Dim value As Object = Selection.GetPropertyValue(TextElement.FontFamilyProperty)
                Return If(value Is DependencyProperty.UnsetValue, String.Empty, value.ToString())
            End Get

            Set(ByVal value As String)
                If Equals(value, Nothing) OrElse Equals(value, SelectionFontFamilyCore) Then Return
                Try
                    If TypeOf value Is String Then
                        Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, New FontFamily(value))
                    Else
                        Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, value)
                    End If
                Catch
                End Try
            End Set
        End Property

        Public Property SelectionFontSizeCore As Double?
            Get
                Dim value As Object = Selection.GetPropertyValue(TextElement.FontSizeProperty)
                If value Is DependencyProperty.UnsetValue Then Return Nothing
                Return CType(value, Double?)
            End Get

            Set(ByVal value As Double?)
                If value Is Nothing OrElse value.Equals(SelectionFontSizeCore) Then Return
                Selection.ApplyPropertyValue(TextElement.FontSizeProperty, Convert.ToDouble(value))
            End Set
        End Property

        Public Property SelectionTextColorCore As Color
            Set(ByVal value As Color)
                If Equals(value, SelectionTextColorCore) Then Return
                Selection.ApplyPropertyValue(TextElement.ForegroundProperty, New SolidColorBrush(value))
            End Set

            Get
                Dim brush As SolidColorBrush = TryCast(Selection.GetPropertyValue(TextElement.ForegroundProperty), SolidColorBrush)
                If brush Is Nothing Then Return Colors.Black
                Return brush.Color
            End Get
        End Property

        Public Property SelectionTextBackgroundColorCore As Color
            Set(ByVal value As Color)
                If value = SelectionTextBackgroundColorCore Then Return
                SetTextBackgroundColor(value)
            End Set

            Get
                Dim brush As SolidColorBrush = TryCast(Selection.GetPropertyValue(TextElement.BackgroundProperty), SolidColorBrush)
                If brush Is Nothing Then Return Colors.Black
                Return brush.Color
            End Get
        End Property

        Public Sub SetTextBackgroundColor(ByVal value As Color)
            Selection.ApplyPropertyValue(TextElement.BackgroundProperty, New SolidColorBrush(value))
        End Sub

        Protected Property TextAlignmentCore As TextAlignment
            Get
                Dim value As Object = Selection.GetPropertyValue(Block.TextAlignmentProperty)
                Return If(value Is DependencyProperty.UnsetValue, TextAlignment.Left, CType(value, TextAlignment))
            End Get

            Set(ByVal value As TextAlignment)
                Selection.ApplyPropertyValue(Block.TextAlignmentProperty, value)
            End Set
        End Property

        Protected ReadOnly Property IsEmptyCore As Boolean
            Get
                For Each b As Block In Document.Blocks
                    If Not(TypeOf b Is Paragraph) Then Return False
                    For Each o As Object In CType(b, Paragraph).Inlines
                        If Not(TypeOf o Is Run) Then Return False
                        Dim r As Run = TryCast(o, Run)
                        If Not String.IsNullOrEmpty(r.Text) Then Return False
                    Next
                Next

                Return True
            End Get
        End Property

        Protected ReadOnly Property IsSelectionEmptyCore As Boolean
            Get
                Return Selection.IsEmpty
            End Get
        End Property

        Protected Property IsBoldCore As Boolean?
            Get
                Dim value As Object = Selection.GetPropertyValue(TextElement.FontWeightProperty)
                Return If(value Is DependencyProperty.UnsetValue, False, Equals(value, FontWeights.Bold))
            End Get

            Set(ByVal value As Boolean?)
                Selection.ApplyPropertyValue(TextElement.FontWeightProperty, If(value.Value, FontWeights.Bold, FontWeights.Normal))
            End Set
        End Property

        Protected Property IsItalicCore As Boolean?
            Get
                Dim value As Object = Selection.GetPropertyValue(TextElement.FontStyleProperty)
                Return If(value Is DependencyProperty.UnsetValue, False, Equals(value, FontStyles.Italic))
            End Get

            Set(ByVal value As Boolean?)
                Selection.ApplyPropertyValue(TextElement.FontStyleProperty, If(value.Value, FontStyles.Italic, FontStyles.Normal))
            End Set
        End Property

        Protected Property IsUnderlineCore As Boolean?
            Get
                Dim value As Object = Selection.GetPropertyValue(Inline.TextDecorationsProperty)
                Return If(value Is DependencyProperty.UnsetValue, False, value IsNot Nothing AndAlso TextDecorations.Underline.Equals(value))
            End Get

            Set(ByVal value As Boolean?)
                Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, If(value.Value, TextDecorations.Underline, Nothing))
            End Set
        End Property

        Protected Property ListMarkerStyleCore As TextMarkerStyle
            Get
                Dim startParagraph As Paragraph = Selection.Start.Paragraph
                Dim endParagraph As Paragraph = Selection.End.Paragraph
                If startParagraph IsNot Nothing AndAlso endParagraph IsNot Nothing AndAlso (TypeOf startParagraph.Parent Is ListItem) AndAlso (TypeOf endParagraph.Parent Is ListItem) AndAlso ReferenceEquals(CType(startParagraph.Parent, ListItem).List, CType(endParagraph.Parent, ListItem).List) Then
                    Return CType(startParagraph.Parent, ListItem).List.MarkerStyle
                End If

                Return TextMarkerStyle.None
            End Get

            Set(ByVal value As TextMarkerStyle)
                Dim p As Paragraph = Selection.Start.Paragraph
                If p Is Nothing Then Return
                If value = TextMarkerStyle.None Then
                    If TypeOf p.Parent Is ListItem Then
                        EditingCommands.ToggleBullets.Execute(Nothing, Me)
                        p = Selection.Start.Paragraph
                        If TypeOf p.Parent Is ListItem Then
                            EditingCommands.ToggleBullets.Execute(Nothing, Me)
                        End If
                    End If

                    Return
                End If

                If Not(TypeOf p.Parent Is ListItem) Then
                    EditingCommands.ToggleBullets.Execute(Nothing, Me)
                    p = Selection.Start.Paragraph
                End If

                If p Is Nothing OrElse Not(TypeOf p.Parent Is ListItem) Then Return
                CType(p.Parent, ListItem).List.MarkerStyle = value
            End Set
        End Property

#End Region
        Public Function GetUIElementUnderSelection(ByVal blocks As BlockCollection) As InlineUIContainer
            For Each block As Block In blocks
                Dim ph As Paragraph = TryCast(block, Paragraph)
                If ph IsNot Nothing Then
                    For Each obj As Object In ph.Inlines
                        If TypeOf obj Is Run Then Continue For
                        Dim cont As InlineUIContainer = TryCast(obj, InlineUIContainer)
                        If cont IsNot Nothing AndAlso cont.ContentStart.CompareTo(Selection.Start) > 0 AndAlso cont.ContentStart.CompareTo(Selection.End) < 0 Then
                            Return cont
                        End If
                    Next
                Else
                    Dim lst As List = TryCast(block, List)
                    If lst IsNot Nothing Then
                        For Each lstItem As ListItem In lst.ListItems
                            Dim retVal As InlineUIContainer = GetUIElementUnderSelection(lstItem.Blocks)
                            If retVal IsNot Nothing Then Return retVal
                        Next
                    End If
                End If
            Next

            Return Nothing
        End Function

        Public Function GetUIElementUnderSelection() As InlineUIContainer
            Dim col As BlockCollection = Document.Blocks
            If Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward) Is Nothing OrElse Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward).CompareTo(Selection.End) <> 0 Then Return Nothing
            Return GetUIElementUnderSelection(col)
        End Function

        Public Sub InsertContainer(ByVal container As InlineUIContainer)
            Selection.Text = ""
            If Selection.End.Paragraph Is Nothing Then
                Selection.End.InsertTextInRun("")
            End If

            If Selection.End.Paragraph IsNot Nothing Then
                If TypeOf Selection.End.Parent Is Run Then
                    Dim text As String = Selection.End.GetTextInRun(LogicalDirection.Forward)
                    Dim newRun As Run = New Run(text)
                    Selection.End.DeleteTextInRun(text.Length)
                    Selection.End.Paragraph.Inlines.InsertAfter(CType(Selection.End.Parent, Run), newRun)
                    Selection.End.Paragraph.Inlines.InsertBefore(newRun, container)
                ElseIf TypeOf Selection.End.Parent Is Paragraph Then
                    Selection.End.Paragraph.Inlines.Add(container)
                End If

                Selection.Select(container.ElementStart, container.ElementEnd)
            End If
        End Sub
    End Class
End Namespace
