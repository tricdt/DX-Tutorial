using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.DemoData;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm.DataAnnotations;

namespace PivotGridDemo {
    public class FilterViewModel {
        bool inFilterSync;

        public FilterViewModel() {
            var dates = SalesPersons.Select(x => x.OrderDate.Value.Date).OrderBy(x => x);
            StartDate = SelectionStart = dates.FirstOrDefault();
            EndDate = SelectionEnd = dates.LastOrDefault();
        }

        public virtual DateTime EndDate { get; set; }
        [BindableProperty(true, OnPropertyChangedMethodName = "UpdateSelection")]
        public virtual CriteriaOperator FilterCriteria { get; set; }
        public IList<SalesPerson> SalesPersons { get { return NWindDataProvider.SalesPersons; } }
        [BindableProperty(true, OnPropertyChangedMethodName = "UpdateFilter")]
        public virtual DateTime SelectionEnd { get; set; }
        [BindableProperty(true, OnPropertyChangedMethodName = "UpdateFilter")]
        public virtual DateTime SelectionStart { get; set; }
        public virtual DateTime StartDate { get; set; }

        protected void UpdateFilter() {
            if(inFilterSync)
                return;
            string str1 = GetCriteria(SelectionStart, true);
            string str2 = GetCriteria(SelectionEnd, false);
            inFilterSync = true;
            if(!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
                FilterCriteria = CriteriaOperator.Parse(str1 + " And " + str2);
            else
                FilterCriteria = CriteriaOperator.Parse(str1 + str2);
            inFilterSync = false;
        }

        protected void UpdateSelection() {
            if (inFilterSync)
                return;
            inFilterSync = true;
            SelectionStart = GetDateTimeValue(GetBinaryOperatorAgrument(FilterCriteria, BinaryOperatorType.GreaterOrEqual), StartDate);
            SelectionEnd = GetDateTimeValue(GetBinaryOperatorAgrument(FilterCriteria, BinaryOperatorType.LessOrEqual), EndDate);
            inFilterSync = false;
        }

        OperandValue GetBinaryOperatorAgrument(CriteriaOperator criteria, BinaryOperatorType type) {
            GroupOperator group = criteria as GroupOperator;
            if(ReferenceEquals(null, group))
                return null;
            BinaryOperator op = group.Operands.OfType<BinaryOperator>()
                .Where(bin => CheckBinaryOperator(bin, type)).FirstOrDefault();
            return ReferenceEquals(null, op) ? null : op.RightOperand as OperandValue;
        }
        bool CheckBinaryOperator(BinaryOperator binaryOperator, BinaryOperatorType type) {
            return object.Equals(binaryOperator.OperatorType, type) && IsOperandProperty(binaryOperator.LeftOperand, "fieldOrderDate");
        }

        DateTime GetDateTimeValue(OperandValue criteria, DateTime defaultValue) {
            DateTime? dateTime = ReferenceEquals(null, criteria) ? null : criteria.Value as DateTime?;
            return dateTime.HasValue ? dateTime.Value : defaultValue;
        }

        bool IsOperandProperty(CriteriaOperator criteria, string fieldName){
            OperandProperty property = criteria as OperandProperty;
            return !ReferenceEquals(null, property) && property.PropertyName == fieldName;
        }

        string GetCriteria(DateTime date, bool isGreater) {
            return string.Format("{0} {1} #{2}#", "fieldOrderDate", isGreater ? ">=" : "<",
                Convert.ToString(date, CultureInfo.InvariantCulture));
        }
    }
}
