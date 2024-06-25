Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.DataModel.DesignTime
Imports System
#If DXCORE3
using DevExpress.Mvvm.DataModel.EFCore;
#Else
Imports DevExpress.Mvvm.DataModel.EF6

#End If
Namespace DevExpress.DevAV.DevAVDbDataModel

    ''' <summary>
    ''' Provides methods to obtain the relevant IUnitOfWorkFactory.
    ''' </summary>
    Public Module UnitOfWorkSource

        ''' <summary>
        ''' Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        ''' </summary>
        Public Function GetUnitOfWorkFactory() As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork)
            Return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode)
        End Function

        ''' <summary>
        ''' Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        ''' </summary>
        ''' <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        Public Function GetUnitOfWorkFactory(ByVal isInDesignTime As Boolean) As IUnitOfWorkFactory(Of IDevAVDbUnitOfWork)
            If isInDesignTime Then Return New DesignTimeUnitOfWorkFactory(Of IDevAVDbUnitOfWork)(Function() New DevAVDbDesignTimeUnitOfWork())
#If DXCORE3
                () => new DevAVDb(string.Format("Data Source={0}", Common.Utils.FilesHelper.GetDatabaseFilePath()));
#Else
            Dim contextFactory As Func(Of DevAVDb) = Function() New DevAVDb()
#End If
            Return New DbUnitOfWorkFactory(Of IDevAVDbUnitOfWork)(Function() New DevAVDbUnitOfWork(contextFactory))
        End Function
    End Module
End Namespace
