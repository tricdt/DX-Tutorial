Imports System.Globalization
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.Xpf.SpellChecker
Imports DevExpress.XtraRichEdit.SpellChecker
Imports DevExpress.XtraSpellChecker
Imports DevExpress.XtraSpellChecker.Native

Namespace DevExpress.DevAV

    Public Class SpellCheckerRichEditBehavior
        Inherits Behavior(Of RichEditControl)

        Private spellChecker As SpellChecker

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AssociatedObject.SpellChecker = spellChecker
        End Sub

        Public Sub New()
            spellChecker = CreateSpellChecker()
        End Sub

        Private Function CreateSpellChecker() As SpellChecker
            Dim spellChecker = New SpellChecker()
            spellChecker.Culture = New CultureInfo("en-US")
            spellChecker.SpellCheckMode = SpellCheckMode.AsYouType
            SpellCheckTextControllersManager.Default.RegisterClass(GetType(RichEditControl), GetType(RichEditSpellCheckController))
            Return spellChecker
        End Function
    End Class
End Namespace
