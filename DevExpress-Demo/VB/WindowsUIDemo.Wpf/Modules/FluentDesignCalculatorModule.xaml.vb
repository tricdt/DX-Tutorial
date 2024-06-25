Imports DevExpress.Xpf.DemoBase
Imports System.Windows

Namespace WindowsUIDemo

    <CodeFile("Modules/FluentDesignCalculatorModule/FluentDesignCalculatorWindow.xaml")>
    <CodeFile("Modules/FluentDesignCalculatorModule/FluentDesignCalculatorWindow.xaml.(cs)")>
    Public Partial Class FluentDesignCalculatorModule
        Inherits DemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim newWindow As Window = Window.GetWindow(CType(sender, DependencyObject))
            Dim fluentDesignCalculator As FluentDesignCalculatorWindow = New FluentDesignCalculatorWindow()
            fluentDesignCalculator.Owner = newWindow
            fluentDesignCalculator.ShowDialog()
        End Sub
    End Class
End Namespace
