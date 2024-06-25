Namespace EditorsDemo

    Public Partial Class TimePickerModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
            DateEditWithNavigatorAndTimePicker.DateTime = Date.Now
            DateEditWithTimePicker.DateTime = Date.Now
        End Sub
    End Class
End Namespace
