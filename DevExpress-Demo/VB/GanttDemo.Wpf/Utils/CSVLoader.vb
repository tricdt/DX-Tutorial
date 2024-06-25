Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO

Namespace GanttDemo

    Public Module CSVLoader

        Private Sub SkipHeader(ByVal reader As StreamReader)
            reader.ReadLine()
        End Sub

        Public Function LoadItems(ByVal stream As Stream) As List(Of DataLoadingItem)
            Using reader = New StreamReader(stream)
                SkipHeader(reader)
                Dim items = New List(Of DataLoadingItem)()
                While Not reader.EndOfStream
                    Dim item = reader.ReadLine()
                    Dim elements = item.Split(","c)
                    items.Add(New DataLoadingItem(Date.Parse(elements(0), CultureInfo.InvariantCulture), TimeSpan.FromMilliseconds(Double.Parse(elements(5), CultureInfo.InvariantCulture)), elements(1), elements(2), elements(3), Integer.Parse(elements(4), CultureInfo.InvariantCulture)))
                End While

                Return items
            End Using
        End Function
    End Module
End Namespace
