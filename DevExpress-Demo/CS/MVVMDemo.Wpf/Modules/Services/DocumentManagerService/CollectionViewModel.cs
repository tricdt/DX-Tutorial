using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.Services.DocumentManager {
    public class CollectionViewModel {
        public void ShowDetail(PersonInfo person) {
            if(person == null)
                return;

            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocument(parameter: person, parentViewModel: this);

            if(document == null) { 
                document = service.CreateDocument(documentType: "DocumentDetailView", parameter: person, parentViewModel: this);
                document.Title = person.FullName;
            }

            document.Show();
        }
    }
}
