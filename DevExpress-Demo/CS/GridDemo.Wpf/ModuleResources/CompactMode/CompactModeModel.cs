using System.Collections.Generic;
using DevExpress.Mvvm.DataAnnotations;


namespace GridDemo {
    [POCOViewModel]
    public class CompactModeModel {   
        public IEnumerable<Message> Data { get { return MailItems.Messages; } }  
    }    
}
