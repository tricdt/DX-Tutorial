Imports DevExpress.Xpf.DemoBase

Namespace NavigationDemo

    <CodeFile("ViewModels/CustomizedContentViewModel.(cs)")>
    Public Partial Class CustomizedContentDemoModule
        Inherits AccordionDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Enum StockIndicatorMode
        Delta
        DeltaChange
    End Enum
End Namespace
