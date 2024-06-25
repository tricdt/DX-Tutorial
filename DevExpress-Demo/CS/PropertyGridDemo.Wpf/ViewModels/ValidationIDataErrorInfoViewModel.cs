namespace PropertyGridDemo {
    public class ValidationIDataErrorInfoViewModel  {
        public virtual DataErrorInfoValidationData Data { get; protected set; }
        public ValidationIDataErrorInfoViewModel() {
            Data = new DataErrorInfoValidationData();
        }
    }
}
