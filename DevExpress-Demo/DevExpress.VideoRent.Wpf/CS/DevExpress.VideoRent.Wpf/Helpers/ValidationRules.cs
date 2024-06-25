using System;
using System.Windows;
using DevExpress.VideoRent.Resources;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Collections;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class ValidationRuleBase : FrameworkElement {
        public ValidationRuleBase() {
            Message = string.Empty;
        }
        public string Message { get; set; }
    }
    public class ValidationRuleIsNotBlank : ValidationRuleBase, IValidationRule {
        public void Validate(object sender, ValidationEventArgs e) {
            string title = e.Value as string;
            if(e.Value == null || title != null && title.Length == 0) {
                e.SetError(Message, ErrorType.Critical);
            }
        }
    }
    public class ValidationRuleIsUnique : ValidationRuleBase, IValidationRule {
        #region Dependency Properties
        public static readonly DependencyProperty ValuesProperty;
        static ValidationRuleIsUnique() {
            Type ownerType = typeof(ValidationRuleIsUnique);
            ValuesProperty = DependencyProperty.Register("Values", typeof(IList), ownerType, new PropertyMetadata(null));
        }
        #endregion
        public IList Values { get { return (IList)GetValue(ValuesProperty); } set { SetValue(ValuesProperty, value); } }
        public void Validate(object sender, ValidationEventArgs e) {
           if(Values == null) return;
            string comparingValueString = e.Value == null ? null : e.Value.ToString();
            foreach(object value in Values) {
                string valueString = value == null ? null : value.ToString();
                if(comparingValueString == valueString) {
                    e.SetError(Message, ErrorType.Critical);
                    break;
                }
            }
        }
    }
}
