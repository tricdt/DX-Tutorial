namespace MVVMDemo.POCOViewModels {
    public class PropertiesViewModel {
        public virtual string UserName { get; set; }
        protected void OnUserNameChanged(string oldValue) {
            ChangedStatus = string.Format("Old value: '{0}' New value: '{1}'", oldValue, UserName);
        }
        public virtual string ChangedStatus { get; protected set; }
    }
}
