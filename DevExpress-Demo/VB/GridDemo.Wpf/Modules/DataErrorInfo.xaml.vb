Imports System.Collections.Generic

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DataErrorInfoClasses.(cs)")>
    Public Partial Class DataErrorInfo
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            Dim persons As List(Of PersonInfo) = New List(Of PersonInfo)()
            persons.Add(New PersonInfo("John", "", "123 Home Lane, Homesville", "(555)956-15-47", "none"))
            persons.Add(New PersonInfo("Henry", "McAllister", "436 1st Ave, Cleveland", "(555)941-24-32", "@hotbox.com"))
            persons.Add(New PersonInfo("Frank", "Frankson", "349 Graphic Design L, Newman", "(555)155-05-02", "none"))
            persons.Add(New PersonInfo("Freddy", "Krueger", "Elm Street", "", "none"))
            persons.Add(New PersonInfo("Leticia", "Ford", "93900 Carter Lane, Cartersville", "(555)776-15-66", "none"))
            persons.Add(New PersonInfo("Karen", "Holmes", "", "(555)342-25-74", "none"))
            persons.Add(New PersonInfo("Roger", "Michelson", "3920 Michelson Dr., Bridgeford", "(555)954-51-88", "none"))
            grid.ItemsSource = persons
        End Sub
    End Class
End Namespace
