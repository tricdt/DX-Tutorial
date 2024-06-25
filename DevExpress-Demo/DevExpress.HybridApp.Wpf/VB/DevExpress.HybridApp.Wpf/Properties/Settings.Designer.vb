Namespace DevExpress.DevAV.Properties

    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")>
    Friend NotInheritable Partial Class Settings
        Inherits Global.System.Configuration.ApplicationSettingsBase

        Private Shared defaultInstance As DevExpress.DevAV.Properties.Settings = CType((Global.System.Configuration.ApplicationSettingsBase.Synchronized(New DevExpress.DevAV.Properties.Settings())), DevExpress.DevAV.Properties.Settings)

        Public Shared ReadOnly Property [Default] As Settings
            Get
                Return DevExpress.DevAV.Properties.Settings.defaultInstance
            End Get
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>All</Name>
    <ImageUri>Resources/Employees/All.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>Salaried</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Salaried#</FilterCriteria>
    <ImageUri>Resources/Employees/Salaried.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>Commission</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Commission#</FilterCriteria>
    <ImageUri>Resources/Employees/Commission.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>Contract</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Contract#</FilterCriteria>
    <ImageUri>Resources/Employees/Probation.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>Terminated</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Terminated#</FilterCriteria>
    <ImageUri>Resources/Employees/Terminated.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>On Leave</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,OnLeave#</FilterCriteria>
    <ImageUri>Resources/Employees/OnLeave.svg</ImageUri>
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property EmployeesStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("EmployeesStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("EmployeesStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>All Customers</Name>
    <FilterCriteria />
  </FilterInfo>
  <FilterInfo>
    <Name>My Account</Name>
    <FilterCriteria>[HomeOffice.State] = ##Enum#DevExpress.DevAV.StateEnum,CA#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>John's Account</Name>
    <FilterCriteria>[HomeOffice.State] = ##Enum#DevExpress.DevAV.StateEnum,WA#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Top Stores</Name>
    <FilterCriteria>[AnnualRevenue] &gt;= 90000000000.0m</FilterCriteria>
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property CustomersCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("CustomersCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("CustomersCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>HD Video player</Name>
    <FilterCriteria>Contains([Name], 'HD') And Category = 'VideoPlayers'</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>50inch Plasma</Name>
    <FilterCriteria>Contains([Name], '50') And Category = 'Televisions'</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>21inch Monitor</Name>
    <FilterCriteria>Contains([Name], '21') And Category = 'Monitors'</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Remote Control</Name>
    <FilterCriteria>Contains([Name], 'Remote') And Category = 'Automation'</FilterCriteria>
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property ProductsCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("ProductsCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("ProductsCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>All</Name>
    <ImageUri>Resources/Products/All.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>Video Players</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,VideoPlayers#</FilterCriteria>
    <ImageUri>Resources/Products/VideoPlayers.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>Automation</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Automation#</FilterCriteria>
    <ImageUri>Resources/Products/Automation.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>Monitors</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Monitors#</FilterCriteria>
    <ImageUri>Resources/Products/Monitors.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>Projectors</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Projectors#</FilterCriteria>
    <ImageUri>Resources/Products/Projectors.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>Televisions</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Televisions#</FilterCriteria>
    <ImageUri>Resources/Products/TVs.svg</ImageUri>
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property ProductsStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("ProductsStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("ProductsStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "<ArrayOfFilterInfo xmlns:xsi=""http://www" & ".w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <Name>All Tasks</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <FilterCriteria />" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <ImageUr" & "i>Resources/Tasks/InProgress.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  " & "  <Name>In Progress</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.Dev" & "AV.EmployeeTaskStatus,InProgress#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <ImageUri>Resources/Task" & "s/InProgress.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <Name>Not Star" & "ted</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeTaskS" & "tatus,NotStarted#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <ImageUri>Resources/Tasks/NotStarted.svg" & "</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <Name>Deferred</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <Fi" & "lterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Deferred#</Fi" & "lterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <ImageUri>Resources/Tasks/Deferred.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  </Filter" & "Info>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <Name>Completed</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <FilterCriteria>[Status] " & "= ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <I" & "mageUri>Resources/Tasks/Completed.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  <FilterInfo" & ">" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <Name>High Priority</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <FilterCriteria>[Priority] = ##Enum#DevEx" & "press.DevAV.EmployeeTaskPriority,High#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <ImageUri>Resources" & "/Tasks/HighPriority.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <Name>U" & "rgent</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <FilterCriteria>[Priority] = ##Enum#DevExpress.DevAV.EmployeeT" & "askPriority,Urgent#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "    <ImageUri>Resources/Tasks/Urgent.svg</" & "ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "</ArrayOfFilterInfo>")>
        Public Property TasksStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("TasksStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("TasksStaticFilters") = value
            End Set
        End Property
    End Class
End Namespace
