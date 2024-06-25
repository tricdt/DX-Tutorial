Imports DevExpress.Mvvm.DataModel

Namespace DevExpress.DevAV.DevAVDbDataModel

    ''' <summary>
    ''' IDevAVDbUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    ''' </summary>
    Public Interface IDevAVDbUnitOfWork
        Inherits IUnitOfWork

        ''' <summary>
        ''' The TaskAttachedFile entities repository.
        ''' </summary>
        ReadOnly Property AttachedFiles As IRepository(Of TaskAttachedFile, Long)

        ''' <summary>
        ''' The EmployeeTask entities repository.
        ''' </summary>
        ReadOnly Property Tasks As IRepository(Of EmployeeTask, Long)

        ''' <summary>
        ''' The Employee entities repository.
        ''' </summary>
        ReadOnly Property Employees As IRepository(Of Employee, Long)

        ''' <summary>
        ''' The CustomerCommunication entities repository.
        ''' </summary>
        ReadOnly Property Communications As IRepository(Of CustomerCommunication, Long)

        ''' <summary>
        ''' The CustomerEmployee entities repository.
        ''' </summary>
        ReadOnly Property CustomerEmployees As IRepository(Of CustomerEmployee, Long)

        ''' <summary>
        ''' The Customer entities repository.
        ''' </summary>
        ReadOnly Property Customers As IRepository(Of Customer, Long)

        ''' <summary>
        ''' The CustomerStore entities repository.
        ''' </summary>
        ReadOnly Property CustomerStores As IRepository(Of CustomerStore, Long)

        ''' <summary>
        ''' The Crest entities repository.
        ''' </summary>
        ReadOnly Property Crests As IRepository(Of Crest, Long)

        ''' <summary>
        ''' The Order entities repository.
        ''' </summary>
        ReadOnly Property Orders As IRepository(Of Order, Long)

        ''' <summary>
        ''' The OrderItem entities repository.
        ''' </summary>
        ReadOnly Property OrderItems As IRepository(Of OrderItem, Long)

        ''' <summary>
        ''' The Product entities repository.
        ''' </summary>
        ReadOnly Property Products As IRepository(Of Product, Long)

        ''' <summary>
        ''' The ProductCatalog entities repository.
        ''' </summary>
        ReadOnly Property ProductCatalogs As IRepository(Of ProductCatalog, Long)

        ''' <summary>
        ''' The ProductImage entities repository.
        ''' </summary>
        ReadOnly Property ProductImages As IRepository(Of ProductImage, Long)

        ''' <summary>
        ''' The Picture entities repository.
        ''' </summary>
        ReadOnly Property Pictures As IRepository(Of Picture, Long)

        ''' <summary>
        ''' The QuoteItem entities repository.
        ''' </summary>
        ReadOnly Property QuoteItems As IRepository(Of QuoteItem, Long)

        ''' <summary>
        ''' The Quote entities repository.
        ''' </summary>
        ReadOnly Property Quotes As IRepository(Of Quote, Long)

        ''' <summary>
        ''' The Evaluation entities repository.
        ''' </summary>
        ReadOnly Property Evaluations As IRepository(Of Evaluation, Long)

        ''' <summary>
        ''' The Probation entities repository.
        ''' </summary>
        ReadOnly Property Probations As IRepository(Of Probation, Long)

        ''' <summary>
        ''' The State entities repository.
        ''' </summary>
        ReadOnly Property States As IRepository(Of State, StateEnum)

        ''' <summary>
        ''' The DatabaseVersion entities repository.
        ''' </summary>
        ReadOnly Property Version As IRepository(Of DatabaseVersion, Long)

    End Interface
End Namespace
