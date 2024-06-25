Imports System
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Documents

Namespace DemoUtils

    Public Class DemoRichControl
        Inherits Windows.Controls.RichTextBox

        Public Sub New()
            DefaultStyleKey = GetType(Windows.Controls.RichTextBox)
        End Sub

        Public Property TextIsBold As Boolean
            Get
                Return IsTextBold()
            End Get

            Set(ByVal value As Boolean)
                ToggleTextFormatBold(value)
            End Set
        End Property

        Public Property TextIsItalic As Boolean
            Get
                Return IsTextItalic()
            End Get

            Set(ByVal value As Boolean)
                ToggleTextFormatItalic(value)
            End Set
        End Property

        Public Property TextIsUnderline As Boolean
            Get
                Return IsTextUnderline()
            End Get

            Set(ByVal value As Boolean)
                ToggleTextFormatUnderline(value)
            End Set
        End Property

        Public Property Text As String
            Get
                Return Selection.Text
            End Get

            Set(ByVal value As String)
                Selection.Text = value
            End Set
        End Property

        Public Property TextFontFamily As Object
            Get
                Dim value As Object = Selection.GetPropertyValue(TextElement.FontFamilyProperty)
                Return If(value Is DependencyProperty.UnsetValue, Nothing, value)
            End Get

            Set(ByVal value As Object)
                If value Is Nothing OrElse value Is TextFontFamily Then Return
                Try
                    If TypeOf value Is String Then
                        Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, New FontFamily(TryCast(value, String)))
                    Else
                        Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, value)
                    End If
                Catch
                End Try
            End Set
        End Property

        Public Property TextFontSize As Object
            Get
                Dim value As Object = Selection.GetPropertyValue(TextElement.FontSizeProperty)
                If value Is DependencyProperty.UnsetValue Then Return Nothing
                Return value
            End Get

            Set(ByVal value As Object)
                If value Is Nothing OrElse value.Equals(TextFontSize) Then Return
                Selection.ApplyPropertyValue(TextElement.FontSizeProperty, Convert.ToDouble(value))
            End Set
        End Property

        Public Property TextColor As Color
            Set(ByVal value As Color)
                If Equals(value, TextColor) Then Return
                Selection.ApplyPropertyValue(TextElement.ForegroundProperty, New SolidColorBrush(value))
            End Set

            Get
                Dim brush As SolidColorBrush = TryCast(Selection.GetPropertyValue(TextElement.ForegroundProperty), SolidColorBrush)
                If brush Is Nothing Then Return Colors.Black
                Return brush.Color
            End Get
        End Property

        Public Sub SetTextColor(ByVal value As Color)
            Selection.ApplyPropertyValue(TextElement.ForegroundProperty, New SolidColorBrush(value))
        End Sub

        Public Property TextBackgroundColor As Color
            Set(ByVal value As Color)
                If value = TextBackgroundColor Then Return
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

        Public Function GetTextAlignment() As TextAlignment
            Dim value As Object = Selection.GetPropertyValue(Block.TextAlignmentProperty)
            If Equals(value, DependencyProperty.UnsetValue) Then Return TextAlignment.Left
            If Equals(value, TextAlignment.Center) Then
                Return TextAlignment.Center
            ElseIf Equals(value, TextAlignment.Right) Then
                Return TextAlignment.Right
            Else
                Return TextAlignment.Left
            End If
        End Function

        Public Sub ToggleTextAlignmentLeft()
            Selection.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Left)
        End Sub

        Public Sub ToggleTextAlignmentCenter()
            Selection.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Center)
        End Sub

        Public Sub ToggleTextAlignmentRight()
            Selection.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Right)
        End Sub

        Public Sub ToggleTextAlignmentJustify()
            Selection.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Justify)
        End Sub

        Public Sub Clear()
            TryCast(Document, FlowDocument).Blocks.Clear()
        End Sub

        Public Sub Print()
            Dim dialog As Windows.Controls.PrintDialog = New Windows.Controls.PrintDialog()
            If dialog.ShowDialog() <> True Then Return
            dialog.PrintVisual(Me, String.Empty)
        End Sub

        Public ReadOnly Property IsEmpty As Boolean
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

        Public ReadOnly Property IsSelectionEmpty As Boolean
            Get
                Return Selection.IsEmpty
            End Get
        End Property

        Protected Function IsTextBold() As Boolean
            Dim value As Object = Selection.GetPropertyValue(TextElement.FontWeightProperty)
            Return If(value Is DependencyProperty.UnsetValue, False, Equals(value, FontWeights.Bold))
        End Function

        Protected Function IsTextItalic() As Boolean
            Dim value As Object = Selection.GetPropertyValue(TextElement.FontStyleProperty)
            Return If(value Is DependencyProperty.UnsetValue, False, Equals(value, FontStyles.Italic))
        End Function

        Protected Function IsTextUnderline() As Boolean
            Dim value As Object = Selection.GetPropertyValue(Inline.TextDecorationsProperty)
            Return If(value Is DependencyProperty.UnsetValue, False, value IsNot Nothing AndAlso TextDecorations.Underline.Equals(value))
        End Function

        Protected Sub ToggleTextFormatBold(ByVal bold As Boolean)
            If bold = IsTextBold() Then Return
            If Not bold Then
                Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal)
            Else
                Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold)
            End If
        End Sub

        Protected Sub ToggleTextFormatItalic(ByVal italic As Boolean)
            If italic = IsTextItalic() Then Return
            If Not italic Then
                Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal)
            Else
                Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic)
            End If
        End Sub

        Protected Sub ToggleTextFormatUnderline(ByVal underline As Boolean)
            If underline = IsTextUnderline() Then Return
            If Not underline Then
                Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, Nothing)
            Else
                Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline)
            End If
        End Sub

        Public Function GetUIElementUnderSelection(Of T As Class)(ByVal blocks As BlockCollection) As T
            For Each block As Block In blocks
                Dim ph As Paragraph = TryCast(block, Paragraph)
                If ph IsNot Nothing Then
                    For Each obj As Object In ph.Inlines
                        If TypeOf obj Is Run Then Continue For
                        Dim cont As InlineUIContainer = TryCast(obj, InlineUIContainer)
                        If cont IsNot Nothing AndAlso cont.ContentStart.CompareTo(Selection.Start) > 0 AndAlso cont.ContentStart.CompareTo(Selection.End) < 0 Then
                            If TypeOf cont.Child Is T Then Return TryCast(cont.Child, T)
                        End If
                    Next
                Else
                    Dim lst As List = TryCast(block, List)
                    If lst IsNot Nothing Then
                        For Each lstItem As ListItem In lst.ListItems
                            Dim retVal As T = GetUIElementUnderSelection(Of T)(lstItem.Blocks)
                            If retVal IsNot Nothing Then Return retVal
                        Next
                    End If
                End If
            Next

            Return Nothing
        End Function

        Public Property ListMarkerStyle As TextMarkerStyle
            Get
                Dim startParagraph As Paragraph = Selection.Start.Paragraph
                Dim endParagraph As Paragraph = Selection.End.Paragraph
                If startParagraph IsNot Nothing AndAlso endParagraph IsNot Nothing AndAlso (TypeOf startParagraph.Parent Is ListItem) AndAlso (TypeOf endParagraph.Parent Is ListItem) AndAlso ReferenceEquals(CType(startParagraph.Parent, ListItem).List, CType(endParagraph.Parent, ListItem).List) Then
                    Return CType(startParagraph.Parent, ListItem).List.MarkerStyle
                End If

                Return TextMarkerStyle.None
            End Get

            Set(ByVal value As TextMarkerStyle)
                If value = ListMarkerStyle Then Return
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

        Public Function GetUIElementUnderSelection(Of T As Class)() As T
            Dim col As BlockCollection = Document.Blocks
            If Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward) Is Nothing OrElse Selection.Start.GetNextInsertionPosition(LogicalDirection.Forward).CompareTo(Selection.End) <> 0 Then Return Nothing
            Return GetUIElementUnderSelection(Of T)(col)
        End Function
    End Class
End Namespace
