Imports System.Collections.Generic

Namespace SankeyDemo

    Friend Module LargestExportPartners

        Public Function GetData() As List(Of Export)
            Return New List(Of Export)() From {New Export("United States", "Canada", 1.553 * 0.18), New Export("United States", "Mexico", 1.553 * 0.157), New Export("United States", "China", 1.553 * 0.084), New Export("United States", "Japan", 1.553 * 0.044), New Export("Germany", "United States", 1.434 * 0.088), New Export("Germany", "France", 1.434 * 0.082), New Export("Germany", "China", 1.434 * 0.068), New Export("Germany", "Netherlands", 1.434 * 0.067), New Export("Germany", "United Kingdom", 1.434 * 0.066), New Export("Germany", "Italy", 1.434 * 0.051), New Export("France", "Germany", 0.549 * 0.148), New Export("France", "Spain", 0.549 * 0.077), New Export("France", "Italy", 0.549 * 0.075), New Export("France", "United States", 0.549 * 0.072), New Export("Italy", "Germany", 0.496 * 0.125), New Export("Italy", "France", 0.496 * 0.103), New Export("Italy", "United States", 0.496 * 0.09), New Export("United Kingdom", "United States", 0.441 * 0.132), New Export("United Kingdom", "Germany", 0.441 * 0.105), New Export("United Kingdom", "France", 0.441 * 0.074), New Export("United Kingdom", "Netherlands", 0.441 * 0.062), New Export("Canada", "United States", 0.4235 * 0.764), New Export("Canada", "China", 0.4235 * 0.043), New Export("China", "United States", 2.216 * 0.192), New Export("China", "Japan", 2.216 * 0.059), New Export("China", "South Korea", 2.216 * 0.044), New Export("Australia", "China", 0.2172 * 0.218), New Export("Australia", "United States", 0.2172 * 0.125), New Export("Australia", "Argentina", 0.2172 * 0.081), New Export("Australia", "Netherlands", 0.2172 * 0.043), New Export("Mexico", "United States", 0.409 * 0.799), New Export("Japan", "United States", 0.6889 * 0.194), New Export("Japan", "China", 0.6889 * 0.19), New Export("Japan", "South Korea", 0.6889 * 0.076), New Export("Brazil", "China", 0.2172 * 0.218), New Export("Brazil", "United States", 0.2172 * 0.125), New Export("Brazil", "Argentina", 0.2172 * 0.081)}
        End Function
    End Module
End Namespace
