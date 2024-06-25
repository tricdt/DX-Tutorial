using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Data;
using System.Threading.Tasks;

namespace GridDemo.VirtualSources {
    public class Paging1ViewModel : ViewModelBase {
        [Command]
        public void FetchIssues(FetchPageAsyncArgs args) {
            args.Result = FetchPageAsync(args);
        }
        static async Task<FetchRowsResult> FetchPageAsync(FetchPageAsyncArgs args) {
            int page = args.Skip / args.Take;
            IssueSortOrder sortOrder = IssueSortOrder.Default;
            IssueFilter filter = null;
            var issues = await IssuesService.GetIssuesPageAsync(
                page: page,
                pageSize: args.Take,
                sortOrder: sortOrder,
                filter: filter);

            return new FetchRowsResult(rows: issues, hasMoreRows: issues.Length == args.Take);
        }
    }
}
