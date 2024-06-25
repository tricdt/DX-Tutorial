using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using System.Collections;
using DevExpress.Xpf.DemoBase.DataClasses;
using System.Data;

namespace EditorsDemo {
    public partial class SearchLookUpEditModule : EditorsDemoModule {
        IDialogOwner dialogOwner;
        Control control;

        CollectionViewViewModel ViewModel { get { return (CollectionViewViewModel)Resources["viewModel"]; } }
        int NewItemRowID { get { return Employees.Count + 1; } }
        IList Employees { get { return ViewModel.Employees; } }
        bool IsRecordValid { get; set; }

        public SearchLookUpEditModule() {
            InitializeComponent();
            this.options.FocusedEditor = searchLookUpEdit;
            searchLookUpEdit.ProcessNewValue += searchLookUpEdit_ProcessNewValue;
            Unloaded += SearchLookUpEditModule_Unloaded;
        }

        void SearchLookUpEditModule_Unloaded(object sender, RoutedEventArgs e) {
            if (dialogOwner == null)
                return;
            dialogOwner.CloseDialog(false);
        }

        void searchLookUpEdit_ProcessNewValue(DependencyObject sender, DevExpress.Xpf.Editors.ProcessNewValueEventArgs e) {
            if (control != null)
                return;
            control = new ContentControl { Template = (ControlTemplate)Resources["addNewRecordTemplate"] };
            Employee row = new Employee();
            row.LastName = e.DisplayText;
            row.Id = NewItemRowID;
            row.BirthDate = DateTime.Now.AddYears(-21).Date;

            control.DataContext = row;
            FrameworkElement owner = sender as FrameworkElement;
            DialogClosedDelegate closeHandler;
            closeHandler = (bool? close) => {
                if (close != null && (bool)close)
                    Employees.Add(control.DataContext);
                control = null;
            };
            dialogOwner = FloatingContainer.ShowDialogContent(control, owner, Size.Empty, new FloatingContainerParameters()
            {
                Title = "Add New Record",
                AllowSizing = false,
                ClosedDelegate = closeHandler,
                ContainerFocusable = false,
                ShowModal = false
            });
            e.PostponedValidation = true;
            e.Handled = true;
            ((FloatingContainer)dialogOwner).Hiding += SearchLookUpEditModule_Closing;
        }
        void SearchLookUpEditModule_Closing(object sender, CancelRoutedEventArgs e) {
            bool? result = ((FloatingContainer)sender).DialogResult;
            if (result == null || !(bool)result)
                return;
            e.Cancel = !GetRecordValid();
        }
        void CloseAddNewRecordHandler(bool? close) {
            if (close != null && (bool)close)
                Employees.Add(control.DataContext);
            control = null;
        }
        void Validate(object sender, ValidationEventArgs e) {
            if (e.Value is string && string.IsNullOrEmpty(e.Value as string)
                || e.Value == null) {
                e.IsValid = false;
                e.ErrorContent = "Please, input the field";
            }
        }
        bool GetRecordValid() {
            if (control == null)
                return false;
            Employee employee = control.DataContext as Employee;
            if (employee == null)
                return false;
            return !object.Equals(employee.BirthDate, null) && !string.IsNullOrEmpty(employee.FirstName) && !string.IsNullOrEmpty(employee.LastName) && !string.IsNullOrEmpty(employee.JobTitle);
        }
    }
}
