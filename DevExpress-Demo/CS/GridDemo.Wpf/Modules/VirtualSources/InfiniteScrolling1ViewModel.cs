using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Data;
using System.Threading.Tasks;

namespace GridDemo.VirtualSources {
    public class InfiniteScrolling1ViewModel : ViewModelBase {
        [Command]
        public void FetchIssues(FetchRowsAsyncArgs args) {
            args.Result = FetchRowsAsync(args);
        }
        static async Task<FetchRowsResult> FetchRowsAsync(FetchRowsAsyncArgs args) {
            int take = args.Take ?? 50;
            IssueSortOrder sortOrder = IssueSortOrder.Default;
            IssueFilter filter = null;
            var issues = await IssuesService.GetIssuesAsync(
                skip: args.Skip,
                take: take,
                sortOrder: sortOrder,
                filter: filter);

            return new FetchRowsResult(rows: issues, hasMoreRows: issues.Length == take);
        }
    }
}
