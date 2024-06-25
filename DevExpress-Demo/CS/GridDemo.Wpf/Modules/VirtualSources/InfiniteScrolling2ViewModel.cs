using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GridDemo.VirtualSources {
    public class InfiniteScrolling2ViewModel : ViewModelBase {
        [Command]
        public void FetchIssues(FetchRowsAsyncArgs args) {
            args.Result = FetchRowsAsync(args);
        }
        static async Task<FetchRowsResult> FetchRowsAsync(FetchRowsAsyncArgs args) {
            #region !
            IssueSortOrder sortOrder = IssueSortOrder.Default;
            if(args.SortOrder.Length > 0) {
                var sort = args.SortOrder.Single();
                if(sort.PropertyName == "Created") {
                    if(sort.Direction != ListSortDirection.Descending)
                        throw new InvalidOperationException();
                    sortOrder = IssueSortOrder.CreatedDescending;
                }
                if(sort.PropertyName == "Votes") {
                    sortOrder = sort.Direction == ListSortDirection.Ascending
                        ? IssueSortOrder.VotesAscending
                        : IssueSortOrder.VotesDescending;
                }
            }
            #endregion

            #region Service request
            int take = args.Take ?? 50;
            IssueFilter filter = null;
            var issues = await IssuesService.GetIssuesAsync(
                skip: args.Skip,
                take: take,
                sortOrder: sortOrder,
                filter: filter);

            return new FetchRowsResult(rows: issues, hasMoreRows: issues.Length == take);
            #endregion
        }
    }
}
