Namespace DevExpress.DevAV.Properties

    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.1.0.0")>
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
    <FilterCriteria />
    <ImageUri />
  </FilterInfo>
  <FilterInfo>
    <Name>Overdue Tasks</Name>
    <FilterCriteria>(Not IsNull([DueDate]))AND([DueDate] &lt; LocalDateTimeNow())AND(Not([Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#))</FilterCriteria>
    <ImageUri />
  </FilterInfo>
  <FilterInfo>
    <Name>Incomplete Tasks</Name>
    <FilterCriteria>Not([Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#)</FilterCriteria>
    <ImageUri>Resources/Tasks/Deferred.svg</ImageUri>
  </FilterInfo>
  <FilterInfo>
    <Name>High Priority Tasks</Name>
    <FilterCriteria>[Priority] = ##Enum#DevExpress.DevAV.EmployeeTaskPriority,High#</FilterCriteria>
    <ImageUri />
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property TasksStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("TasksStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("TasksStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>On probation </Name>
    <FilterCriteria>Not IsNull([ProbationReason])</FilterCriteria>
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property EmployeesCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("EmployeesCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("EmployeesCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>All</Name>
  </FilterInfo>
  <FilterInfo>
    <Name>Salaried</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Salaried#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Commission</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Commission#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Contract</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Contract#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Terminated</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Terminated#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>On Leave</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,OnLeave#</FilterCriteria>
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
    <Name>Active</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.CustomerStatus,Active#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Suspended</Name>
    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.CustomerStatus,Suspended#</FilterCriteria>
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
    <Name>All</Name>
  </FilterInfo>
  <FilterInfo>
    <Name>Stores &gt; 10 Locations</Name>
    <FilterCriteria>[TotalStores] &gt; 10</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Revenues &gt; 100 Billion</Name>
    <FilterCriteria>[AnnualRevenue] &gt; 100000000000.0m</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Employees &gt; 10000</Name>
    <FilterCriteria>[TotalEmployees] &gt; 10000</FilterCriteria>
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property CustomersStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("CustomersStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("CustomersStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>Discontinued</Name>
    <FilterCriteria>[Available] = False</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Out of Stock</Name>
    <FilterCriteria>[CurrentInventory] = 0</FilterCriteria>
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
  </FilterInfo>
  <FilterInfo>
    <Name>Video Players</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,VideoPlayers#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Automation</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Automation#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Monitors</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Monitors#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Projectors</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Projectors#</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Televisions</Name>
    <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Televisions#</FilterCriteria>
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
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>Sales &gt; $5000</Name>
    <FilterCriteria>[TotalAmount] &gt; 5000.0m</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Sales &lt; $5000</Name>
    <FilterCriteria>[TotalAmount] &lt; 5000.0m</FilterCriteria>
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property OrdersCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("OrdersCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("OrdersCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>All</Name>
  </FilterInfo>
  <FilterInfo>
    <Name>Today</Name>
    <FilterCriteria>IsOutlookIntervalToday([OrderDate])</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Yesterday</Name>
    <FilterCriteria>IsOutlookIntervalYesterday([OrderDate])</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>This Month</Name>
    <FilterCriteria>IsOutlookIntervalEarlierThisMonth([OrderDate])</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>This Year</Name>
    <FilterCriteria>IsOutlookIntervalEarlierThisYear([OrderDate])</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Unpaid Orders</Name>
    <FilterCriteria>[PaymentTotal] = 0.0m</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>Refunds</Name>
    <FilterCriteria>[PaymentTotal] &gt; 0.0m AND [RefundTotal]  = [PaymentTotal]</FilterCriteria>
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property OrdersStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("OrdersStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("OrdersStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>All</Name>
  </FilterInfo>
  <FilterInfo>
    <Name>Last Year</Name>
    <FilterCriteria>GetYear([Date])=(GetYear(LocalDateTimeNow())-1)</FilterCriteria>
  </FilterInfo>
  <FilterInfo>
    <Name>This Year</Name>
    <FilterCriteria>IsOutlookIntervalEarlierThisYear([Date])</FilterCriteria>
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property QuotesStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("QuotesStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("QuotesStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>All</Name>
    <FilterCriteria />
    <ImageUri />
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property TasksCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("TasksCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("TasksCustomFilters") = value
            End Set
        End Property
    End Class
End Namespace
