using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using DevExpress.Data.Filtering;

namespace EditorsDemo {
    public partial class SearchLookUpEditOptions : UserControl {
        public BaseEdit FocusedEditor {
            get { return (BaseEdit)GetValue(FocusedEditorProperty); }
            set { SetValue(FocusedEditorProperty, value); }
        }

        public static readonly DependencyProperty FocusedEditorProperty =
            DependencyProperty.Register("FocusedEditor", typeof(BaseEdit), typeof(SearchLookUpEditOptions), null);

        
        public SearchLookUpEditOptions() {
            InitializeComponent();
            ComboBoxEdit.SetupComboBoxEnumItemSource<FilterCondition, FilterCondition>(filterConditionComboBox);
            ComboBoxEdit.SetupComboBoxEnumItemSource<FindMode, FindMode>(findModeComboBox);
            
            ComboBoxEdit.SetupComboBoxEnumItemSource<EditorPlacement, EditorPlacement>(addNewComboBox);
            
        }
    }
}
