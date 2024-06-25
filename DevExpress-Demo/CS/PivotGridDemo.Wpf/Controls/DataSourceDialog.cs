using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPivotGrid;
using DevExpress.Xpf.Core;

namespace PivotGridDemo {
    
    public partial class DataSourceDialog : Control {
        const string ConnectionStringName = "ConnectionString",
            CatalogsComboName = "CatalogsCombo",
            CubesComboName = "CubesCombo",
            UserName= "UserCombo",
            PasswordName = "PasswordCombo",
            ConnectButtonName = "Connect";

        public static readonly DependencyProperty IsCatalogsRetrivingProperty;
        public static readonly DependencyProperty IsCubesRetrivingProperty;
        IOLAPMetaGetter metaGetter = OLAPBrowser.UseXmlaConnection ? (IOLAPMetaGetter)new XmlaMetaGetter() : new DevExpress.XtraPivotGrid.Data.OLAPMetaGetter();

        static DataSourceDialog() {
            Type ownerType = typeof(DataSourceDialog);
            IsCatalogsRetrivingProperty = DependencyProperty.Register("IsCatalogsRetriving", typeof(bool), ownerType, new PropertyMetadata(false));
            IsCubesRetrivingProperty = DependencyProperty.Register("IsCubesRetriving", typeof(bool), ownerType, new PropertyMetadata(false));
        }

        public bool IsCatalogsRetriving {
            get { return (bool)GetValue(IsCatalogsRetrivingProperty); }
            set { SetValue(IsCatalogsRetrivingProperty, value); }
        }

        public bool IsCubesRetriving {
            get { return (bool)GetValue(IsCubesRetrivingProperty); }
            set { SetValue(IsCubesRetrivingProperty, value); }
        }

        ComboBoxEdit CatalogsCombo { get; set; }
        ComboBoxEdit CubesCombo { get; set; }
        TextEdit ConnectionString { get; set; }
        TextEdit User { get; set; }
        PasswordBoxEdit Password { get; set; }
        Button ConnectButton { get; set; }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            CatalogsCombo = GetTemplateChild(CatalogsComboName) as ComboBoxEdit;
            CatalogsCombo.EditValueChanged += OnCatalogsComboEditValueChanged;
            CubesCombo = GetTemplateChild(CubesComboName) as ComboBoxEdit;
            ConnectionString = GetTemplateChild(ConnectionStringName) as TextEdit;
            User = GetTemplateChild(UserName) as TextEdit;
            Password = GetTemplateChild(PasswordName) as PasswordBoxEdit;
            ConnectButton = GetTemplateChild(ConnectButtonName) as Button;
            ConnectButton.Click += Connect;
            ApplyPlatformTemplate();
        }

        void Connect(object sender, RoutedEventArgs e) {
            ClearCombo(CatalogsCombo);
            ClearCombo(CubesCombo);
            IsCatalogsRetriving = true;
            IsCubesRetriving = false;
            metaGetter.ConnectionString = GetConnectionStringCore();
            if(!metaGetter.Connected) {
                ShowMessage("Invalid cube.");
                IsCatalogsRetriving = false;
                return;
            }
            RetriveCatalogsAndCubes();
        }

        void OnCatalogsComboEditValueChanged(object sender, EditValueChangedEventArgs e) {
            IsCatalogsRetriving = false;
            if(IsCatalogEmpty()) {
                IsCubesRetriving = false;
                return;
            }
            IsCubesRetriving = true;
            CubesCombo.Clear();
            metaGetter.ConnectionString = GetConnectionStringCore();
            RetriveCubes();
        }

        bool CatalogOrCubeEmpty() {
            if(IsCatalogEmpty())
                return true;
            return CubesCombo.SelectedIndex < 0 || string.IsNullOrEmpty(CubesCombo.Items[CubesCombo.SelectedIndex] as string);
        }
        bool IsCatalogEmpty() {
            return CatalogsCombo.SelectedIndex < 0 || string.IsNullOrEmpty(CatalogsCombo.Items[CatalogsCombo.SelectedIndex] as string);
        }
        void ClearCombo(ComboBoxEdit edit) {
            edit.Items.Clear();
            edit.EditValue = string.Empty;
        }

        const string CubeEditName = "CubeEdit",
                    ServerRadioName = "AnalysisServerRadio",
                    TextCubeFileName = "TextCubeFile",
                    CubeRadioName = "CubeRadio";

        bool OpenCubeFile(string fileName) {
            try {
                CubeEdit.Text = fileName;
                metaGetter.ConnectionString = "Provider=msolap;Data Source=" + CubeEdit.Text;
                RetriveCatalogsAndCubes();
                return true;
            } finally {
                metaGetter.Connected = false;
            }
        }

        void RetriveCatalogsAndCubes() {
            if(!InitComboBox(metaGetter.GetCatalogs(), CatalogsCombo)) {
                ShowMessage("There is no catalogs in the cube file.");
            }
            IsCatalogsRetriving = false;
        }

        void UpdateControls() {
            bool isServer = AnalysisServerRadio.IsChecked.Value;
            if(CubeEdit.Text.Length > 0)
                ConnectButton.IsEnabled = true;
            CatalogsCombo.IsEnabled = isServer;
            CubesCombo.IsEnabled = isServer;
            CubeEdit.Text = string.Empty;
            CubeEdit.AllowDefaultButton = !isServer;
            ClearCombo(CatalogsCombo);
            ClearCombo(CubesCombo);
            TextCubeFile.Text = isServer ? "Server" : "Cube File";
        }

        public string GetConnectionString() {
            if(CatalogOrCubeEmpty())
                return null;
            return GetConnectionStringCore() +
                ";Initial Catalog=" + (string)CatalogsCombo.EditValue +
                ";Cube Name=" + (string)CubesCombo.EditValue;
        }

        string GetConnectionStringCore() {
            return "Provider=msolap;Data Source=" + CubeEdit.Text;
        }

        void ShowMessage(string message) {
            ThemedMessageBox.Show(Application.Current.MainWindow.Title, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        bool InitComboBox(List<string> items, ComboBoxEdit edit) {
            ClearCombo(edit);
            if(items != null && items.Count > 0) {
                edit.Items.AddRange(items.ToArray());
                edit.SelectedItem = edit.Items[0];
                return true;
            }
            return false;
        }

        ButtonEdit CubeEdit { get; set; }
        RadioButton AnalysisServerRadio { get; set; }
        RadioButton CubeRadio { get; set; }
        TextBlock TextCubeFile { get; set; }

        void ApplyPlatformTemplate() {
            CubeEdit = GetTemplateChild(CubeEditName) as ButtonEdit;
            CubeEdit.EditValueChanged += new EditValueChangedEventHandler(CubeEdit_EditValueChanged);
            ConnectButton.IsEnabled = false;
            AnalysisServerRadio = GetTemplateChild(ServerRadioName) as RadioButton;
            CubeRadio = GetTemplateChild(CubeRadioName) as RadioButton;
            TextCubeFile = GetTemplateChild(TextCubeFileName) as TextBlock;
            AnalysisServerRadio.Checked += new RoutedEventHandler(AnalysisServerRadio_Checked);
            CubeRadio.Checked += new RoutedEventHandler(CubeRadio_Checked);
            CubeEdit.DefaultButtonClick += new RoutedEventHandler(CubeEdit_DefaultButtonClick);

        }

        void CubeEdit_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            if(CubeEdit.Text.Length > 0)
                ConnectButton.IsEnabled = true;
            else
                ConnectButton.IsEnabled = false;
        }

        void CubeEdit_DefaultButtonClick(object sender, RoutedEventArgs e) {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Cube files (*.cub)|*.cub";
            if(dialog.ShowDialog() == true) {
                OpenCubeFile(dialog.FileName);
            }
        }

        void CubeRadio_Checked(object sender, RoutedEventArgs e) {
            UpdateControls();
        }

        void AnalysisServerRadio_Checked(object sender, RoutedEventArgs e) {
            UpdateControls();
        }

        void RetriveCubes() {
            if(!InitComboBox(metaGetter.GetCubes(CatalogsCombo.SelectedItem.ToString()), CubesCombo))
                ShowMessage("There is no catalogs in the cube file.");
            IsCubesRetriving = false;
        }

    }
}
