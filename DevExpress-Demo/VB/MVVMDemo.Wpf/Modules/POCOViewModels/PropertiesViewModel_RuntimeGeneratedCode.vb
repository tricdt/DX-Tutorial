Imports System.ComponentModel

' This class demonstrates the code that will be generated by the POCO mechanism at runtime
Namespace MVVMDemo.POCOViewModels

    Public Class PropertiesViewModel_RuntimeGeneratedCode
        Inherits PropertiesViewModel
        Implements INotifyPropertyChanged

        Public Overrides Property UserName As String
            Get
                Return MyBase.UserName
            End Get

            Set(ByVal value As String)
                If Equals(MyBase.UserName, value) Then Return
                Dim oldValue As String = MyBase.UserName
                MyBase.UserName = value
                OnUserNameChanged(oldValue)
                RaisePropertyChanged("UserName")
            End Set
        End Property

        Public Overrides Property ChangedStatus As String
            Get
                Return MyBase.ChangedStatus
            End Get

            Set(ByVal value As String)
                If Equals(MyBase.ChangedStatus, value) Then Return
                MyBase.UserName = value
                RaisePropertyChanged("ChangedStatus")
            End Set
        End Property

#Region "INotifyPropertyChanged implementation"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim handler = PropertyChangedEvent
            If handler IsNot Nothing Then handler(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
#End Region
    End Class
End Namespace