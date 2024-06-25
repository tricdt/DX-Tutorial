using System.Collections.Generic;
using System.Linq;

namespace WindowsUIDemo {
    public class CompactModeModel {
        public IEnumerable<Message> Data { get { return MailItems.Messages; } }    

        public Message FocusedRow { get; set; }

        public CompactModeModel() {
            FocusedRow = Data.First();
        }
    } 
}
