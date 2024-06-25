Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Controls

Namespace GanttDemo.Examples

    Public Partial Class CustomizeWorkingTime
        Inherits System.Windows.Controls.UserControl

        Private Shared _Holidays As IEnumerable(Of System.DateTime)

        Shared Sub New()
            GanttDemo.Examples.CustomizeWorkingTime.Holidays = GanttDemo.DateTimeHelper.GetHolidays(System.DateTime.Today.AddYears(-1), System.DateTime.Today.AddYears(1)).ToArray()
        End Sub

        Public Shared Property Holidays As IEnumerable(Of System.DateTime)
            Get
                Return _Holidays
            End Get

            Private Set(ByVal value As IEnumerable(Of System.DateTime))
                _Holidays = value
            End Set
        End Property

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class
End Namespace
