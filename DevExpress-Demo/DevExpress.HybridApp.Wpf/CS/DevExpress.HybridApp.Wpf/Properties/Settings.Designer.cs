









namespace DevExpress.DevAV.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
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
</ArrayOfFilterInfo>")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList EmployeesStaticFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["EmployeesStaticFilters"]));
            }
            set {
                this["EmployeesStaticFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
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
</ArrayOfFilterInfo>")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList CustomersCustomFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["CustomersCustomFilters"]));
            }
            set {
                this["CustomersCustomFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
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
</ArrayOfFilterInfo>")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList ProductsCustomFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["ProductsCustomFilters"]));
            }
            set {
                this["ProductsCustomFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
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
</ArrayOfFilterInfo>")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList ProductsStaticFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["ProductsStaticFilters"]));
            }
            set {
                this["ProductsStaticFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfFilterInfo xmlns:xsi=\"http://www" +
            ".w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n" +
            "  <FilterInfo>\r\n    <Name>All Tasks</Name>\r\n    <FilterCriteria />\r\n    <ImageUr" +
            "i>Resources/Tasks/InProgress.svg</ImageUri>\r\n  </FilterInfo>\r\n  <FilterInfo>\r\n  " +
            "  <Name>In Progress</Name>\r\n    <FilterCriteria>[Status] = ##Enum#DevExpress.Dev" +
            "AV.EmployeeTaskStatus,InProgress#</FilterCriteria>\r\n    <ImageUri>Resources/Task" +
            "s/InProgress.svg</ImageUri>\r\n  </FilterInfo>\r\n  <FilterInfo>\r\n    <Name>Not Star" +
            "ted</Name>\r\n    <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeTaskS" +
            "tatus,NotStarted#</FilterCriteria>\r\n    <ImageUri>Resources/Tasks/NotStarted.svg" +
            "</ImageUri>\r\n  </FilterInfo>\r\n  <FilterInfo>\r\n    <Name>Deferred</Name>\r\n    <Fi" +
            "lterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Deferred#</Fi" +
            "lterCriteria>\r\n    <ImageUri>Resources/Tasks/Deferred.svg</ImageUri>\r\n  </Filter" +
            "Info>\r\n  <FilterInfo>\r\n    <Name>Completed</Name>\r\n    <FilterCriteria>[Status] " +
            "= ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#</FilterCriteria>\r\n    <I" +
            "mageUri>Resources/Tasks/Completed.svg</ImageUri>\r\n  </FilterInfo>\r\n  <FilterInfo" +
            ">\r\n    <Name>High Priority</Name>\r\n    <FilterCriteria>[Priority] = ##Enum#DevEx" +
            "press.DevAV.EmployeeTaskPriority,High#</FilterCriteria>\r\n    <ImageUri>Resources" +
            "/Tasks/HighPriority.svg</ImageUri>\r\n  </FilterInfo>\r\n  <FilterInfo>\r\n    <Name>U" +
            "rgent</Name>\r\n    <FilterCriteria>[Priority] = ##Enum#DevExpress.DevAV.EmployeeT" +
            "askPriority,Urgent#</FilterCriteria>\r\n    <ImageUri>Resources/Tasks/Urgent.svg</" +
            "ImageUri>\r\n  </FilterInfo>\r\n</ArrayOfFilterInfo>")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList TasksStaticFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["TasksStaticFilters"]));
            }
            set {
                this["TasksStaticFilters"] = value;
            }
        }
    }
}
