using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GridDemo {
    public abstract class LargeDataSourceObjectBase {
        protected class ValuesContainer<T> {
            Dictionary<int, T> modifiedValues;
            readonly Func<int, T> getDefaultValue;
            public ValuesContainer(Func<int, T> getDefaultValue) {
                this.getDefaultValue = getDefaultValue;
            }
            public T GetValue(int index) {
                if(modifiedValues == null)
                    return getDefaultValue(index);
                T value;
                if(modifiedValues.TryGetValue(index, out value))
                    return value;
                return getDefaultValue(index);
            }
            public void SetValue(int index, T value) {
                if(modifiedValues == null)
                    modifiedValues = new Dictionary<int, T>();
                modifiedValues[index] = value;
            }
        } 
        static readonly Priority[] Priorities = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToArray();
        static LargeDataSourceObjectBase() {
        }

        const int BaseColumnCount = 7;
        protected readonly ValuesContainer<string> fromValues;
        protected readonly ValuesContainer<string> toValues;
        protected readonly ValuesContainer<DateTime> sentValues;
        protected readonly ValuesContainer<bool> hasAttachmentValues;
        protected readonly ValuesContainer<int> sizeValues;
        protected readonly ValuesContainer<Priority> priorityValues;
        protected readonly ValuesContainer<string> subjectValues;
        public LargeDataSourceObjectBase(int id) {
            Id = id;
            fromValues = new ValuesContainer<string>(index => OutlookData.Users[GetPseudoRandomValue(Id, index, OutlookData.Users.Length)].Name);
            toValues = new ValuesContainer<string>(index => OutlookData.Users[GetPseudoRandomValue(Id, index + 5, OutlookData.Users.Length)].Name);
            sentValues = new ValuesContainer<DateTime>(index => DateTime.Today.AddDays(GetPseudoRandomValue(Id, index, 30)));
            hasAttachmentValues = new ValuesContainer<bool>(index => GetPseudoRandomValue(Id, index, 2) == 0 ? true : false);
            sizeValues = new ValuesContainer<int>(index => GetPseudoRandomValue(Id, index, 10000));
            priorityValues = new ValuesContainer<Priority>(index => Priorities[GetPseudoRandomValue(Id, index, Priorities.Length)]);
            subjectValues = new ValuesContainer<string>(index => OutlookDataGenerator.Subjects[GetPseudoRandomValue(Id, index, OutlookDataGenerator.Subjects.Length)]);
        }
        [Display(Name = "Id (0)", Order = 0)]
        public int Id { get; private set; }

        int GetPseudoRandomValue(int rowIndex, int columnIndex, int maxValue) {
            return (rowIndex + columnIndex) % maxValue;
        }
    }
}
