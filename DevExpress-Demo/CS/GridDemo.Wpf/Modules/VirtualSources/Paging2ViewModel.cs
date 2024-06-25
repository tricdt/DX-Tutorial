using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GridDemo.VirtualSources {
    public class Paging2ViewModel : ViewModelBase {
        [Command]
        public void FetchIssues(FetchPageAsyncArgs args) {
            args.Result = FetchPageAsync(args);
        }
        static async Task<FetchRowsResult> FetchPageAsync(FetchPageAsyncArgs args) {
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
            int page = args.Skip / args.Take;
            IssueFilter filter = null;
            var issues = await IssuesService.GetIssuesPageAsync(
                page: page,
                pageSize: args.Take,
                sortOrder: sortOrder,
                filter: filter);

            return new FetchRowsResult(rows: issues, hasMoreRows: issues.Length == args.Take);
            #endregion
        }
    }
}
