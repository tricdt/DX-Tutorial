using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using MyDX_Demo.Module.MVVM_Demo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo.DocumentManagerService
{
    public class CollectionViewModel
    {
        public void ShowDetail(PersonInfo person)
        {
            if (person == null)
                return;

            IDocumentManagerService service = this.GetRequiredService<IDocumentManagerService>();
            IDocument document = service.FindDocument(parameter: person, parentViewModel: this);

            if (document == null)
            {
                document = service.CreateDocument(documentType: "DocumentDetailView", parameter: person, parentViewModel: this);
                document.Title = person.FullName;
            }

            document.Show();
        }
    }
}
