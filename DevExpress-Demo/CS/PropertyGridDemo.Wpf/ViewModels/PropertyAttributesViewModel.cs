using System;

namespace PropertyGridDemo {
    public class PropertyAttributesViewModel {
        public virtual AttributesSupportData Data { get; protected set; }
        public PropertyAttributesViewModel() {
            Data = new AttributesSupportData() { 
                ID = 123456, 
                FirstName = "Nancy", 
                LastName = "Davolio", 
                Age = 36, 
                Gender = Gender.Female,
                HireDate = new DateTime(2002, 2, 2), 
                SSN = "123-45-6789"
            };
        }
    }
}
