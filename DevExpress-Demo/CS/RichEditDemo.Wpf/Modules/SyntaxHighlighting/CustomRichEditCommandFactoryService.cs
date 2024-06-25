using DevExpress.Utils;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraRichEdit.Services;

namespace RichEditDemo {
    public class CustomRichEditCommandFactoryService : IRichEditCommandFactoryService {
        readonly IRichEditCommandFactoryService service;

        public CustomRichEditCommandFactoryService(IRichEditCommandFactoryService service) {
            Guard.ArgumentNotNull(service, "service");
            this.service = service;
        }

        RichEditCommand IRichEditCommandFactoryService.CreateCommand(RichEditCommandId id) {
            if (id.Equals(RichEditCommandId.InsertColumnBreak) || id.Equals(RichEditCommandId.InsertLineBreak) || id.Equals(RichEditCommandId.InsertPageBreak))
                return service.CreateCommand(RichEditCommandId.InsertParagraph);
            return service.CreateCommand(id);
        }
    }
}
