Imports System.Collections.Generic

Namespace SchedulingDemo.ViewModels

    Public Class AppointmentCustomizationDemoViewModel

        Private ReadOnly data As CarsData = New CarsData()

        Protected Sub New()
            CustomFlyout = False
            CustomTemplate_AllDay = False
            CustomTemplate = CustomTemplate_AllDay
            ShowStatus_AllDay = True
            ShowStatus = ShowStatus_AllDay
            ShowLocation_AllDay = True
            ShowLocation = ShowLocation_AllDay
            ShowDescription_AllDay = True
            ShowDescription = ShowDescription_AllDay
            ShowInterval = True
            ShowInterval_AllDay = False
            ShowArrows = False
            ShowArrows_AllDay = True
        End Sub

        Public Overridable ReadOnly Property Events As List(Of CarScheduling)
            Get
                Return data.Events
            End Get
        End Property

        Public Overridable ReadOnly Property Cars As List(Of Car)
            Get
                Return data.Cars
            End Get
        End Property

        Public Overridable Property CustomFlyout As Boolean

        Public Overridable Property CustomTemplate As Boolean

        Public Overridable Property ShowStatus As Boolean

        Public Overridable Property ShowLocation As Boolean

        Public Overridable Property ShowDescription As Boolean

        Public Overridable Property ShowInterval As Boolean

        Public Overridable Property ShowArrows As Boolean

        Public Overridable Property CustomTemplate_AllDay As Boolean

        Public Overridable Property ShowStatus_AllDay As Boolean

        Public Overridable Property ShowLocation_AllDay As Boolean

        Public Overridable Property ShowDescription_AllDay As Boolean

        Public Overridable Property ShowInterval_AllDay As Boolean

        Public Overridable Property ShowArrows_AllDay As Boolean
    End Class
End Namespace
