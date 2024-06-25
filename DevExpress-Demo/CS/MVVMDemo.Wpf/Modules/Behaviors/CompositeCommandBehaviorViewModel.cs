using System.Windows;

namespace MVVMDemo.Behaviors {
    public class CompositeCommandBehaviorViewModel {
        public void Register(string userName) {
            MessageBox.Show("Registered: " + userName);
        }
        public bool CanRegister(string userName) {
            return !string.IsNullOrEmpty(userName);
        }

        int logEntryIndex;
        public void Log(string log) {
            LogText = string.Format("Log entry {0}: {1}", logEntryIndex++, log);
        }
        public virtual string LogText { get; protected set; }
    }
}
