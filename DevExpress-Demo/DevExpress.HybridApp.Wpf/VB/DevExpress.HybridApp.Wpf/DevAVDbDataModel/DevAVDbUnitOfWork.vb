Imports DevExpress.Mvvm.DataModel
Imports System
#If DXCORE3
using DevExpress.Mvvm.DataModel.EFCore;
#Else
Imports DevExpress.Mvvm.DataModel.EF6

#End If
Namespace DevExpress.DevAV.DevAVDbDataModel

    ''' <summary>
    ''' A DevAVDbUnitOfWork instance that represents the run-time implementation of the IDevAVDbUnitOfWork interface.
    ''' </summary>
    Public Class DevAVDbUnitOfWork
        Inherits DbUnitOfWork(Of DevAVDb)
        Implements IDevAVDbUnitOfWork

        Public Sub New(ByVal contextFactory As Func(Of DevAVDb))
            MyBase.New(contextFactory)
        End Sub

        Private ReadOnly Property AttachedFiles As IRepository(Of TaskAttachedFile, Long) Implements IDevAVDbUnitOfWork.AttachedFiles
            Get
                Return GetRepository(Function(x) x.[Set](Of TaskAttachedFile)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Tasks As IRepository(Of EmployeeTask, Long) Implements IDevAVDbUnitOfWork.Tasks
            Get
                Return GetRepository(Function(x) x.[Set](Of EmployeeTask)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Employees As IRepository(Of Employee, Long) Implements IDevAVDbUnitOfWork.Employees
            Get
                Return GetRepository(Function(x) x.[Set](Of Employee)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Communications As IRepository(Of CustomerCommunication, Long) Implements IDevAVDbUnitOfWork.Communications
            Get
                Return GetRepository(Function(x) x.[Set](Of CustomerCommunication)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property CustomerEmployees As IRepository(Of CustomerEmployee, Long) Implements IDevAVDbUnitOfWork.CustomerEmployees
            Get
                Return GetRepository(Function(x) x.[Set](Of CustomerEmployee)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Customers As IRepository(Of Customer, Long) Implements IDevAVDbUnitOfWork.Customers
            Get
                Return GetRepository(Function(x) x.[Set](Of Customer)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property CustomerStores As IRepository(Of CustomerStore, Long) Implements IDevAVDbUnitOfWork.CustomerStores
            Get
                Return GetRepository(Function(x) x.[Set](Of CustomerStore)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Crests As IRepository(Of Crest, Long) Implements IDevAVDbUnitOfWork.Crests
            Get
                Return GetRepository(Function(x) x.[Set](Of Crest)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Orders As IRepository(Of Order, Long) Implements IDevAVDbUnitOfWork.Orders
            Get
                Return GetRepository(Function(x) x.[Set](Of Order)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property OrderItems As IRepository(Of OrderItem, Long) Implements IDevAVDbUnitOfWork.OrderItems
            Get
                Return GetRepository(Function(x) x.[Set](Of OrderItem)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Products As IRepository(Of Product, Long) Implements IDevAVDbUnitOfWork.Products
            Get
                Return GetRepository(Function(x) x.[Set](Of Product)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property ProductCatalogs As IRepository(Of ProductCatalog, Long) Implements IDevAVDbUnitOfWork.ProductCatalogs
            Get
                Return GetRepository(Function(x) x.[Set](Of ProductCatalog)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property ProductImages As IRepository(Of ProductImage, Long) Implements IDevAVDbUnitOfWork.ProductImages
            Get
                Return GetRepository(Function(x) x.[Set](Of ProductImage)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Pictures As IRepository(Of Picture, Long) Implements IDevAVDbUnitOfWork.Pictures
            Get
                Return GetRepository(Function(x) x.[Set](Of Picture)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property QuoteItems As IRepository(Of QuoteItem, Long) Implements IDevAVDbUnitOfWork.QuoteItems
            Get
                Return GetRepository(Function(x) x.[Set](Of QuoteItem)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Quotes As IRepository(Of Quote, Long) Implements IDevAVDbUnitOfWork.Quotes
            Get
                Return GetRepository(Function(x) x.[Set](Of Quote)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Evaluations As IRepository(Of Evaluation, Long) Implements IDevAVDbUnitOfWork.Evaluations
            Get
                Return GetRepository(Function(x) x.[Set](Of Evaluation)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property Probations As IRepository(Of Probation, Long) Implements IDevAVDbUnitOfWork.Probations
            Get
                Return GetRepository(Function(x) x.[Set](Of Probation)(), Function(ByVal x) x.Id)
            End Get
        End Property

        Private ReadOnly Property States As IRepository(Of State, StateEnum) Implements IDevAVDbUnitOfWork.States
            Get
                Return GetRepository(Function(x) x.[Set](Of State)(), Function(ByVal x) x.ShortName)
            End Get
        End Property

        Private ReadOnly Property Version As IRepository(Of DatabaseVersion, Long) Implements IDevAVDbUnitOfWork.Version
            Get
                Return GetRepository(Function(x) x.[Set](Of DatabaseVersion)(), Function(ByVal x) x.Id)
            End Get
        End Property
    End Class
End Namespace
