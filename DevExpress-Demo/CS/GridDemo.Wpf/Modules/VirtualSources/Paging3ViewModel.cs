using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GridDemo.VirtualSources {
    public class Paging3ViewModel : ViewModelBase {
        [Command]
        public void FetchIssues(FetchPageAsyncArgs args) {
            args.Result = FetchPageAsync(args);
        }
        static async Task<FetchRowsResult> FetchPageAsync(FetchPageAsyncArgs args) {
            #region Sorting
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

            #region !
            IssueFilter filter = (IssueFilter)args.Filter;
            #endregion

            #region Service request
            int page = args.Skip / args.Take;
            var issues = await IssuesService.GetIssuesPageAsync(
                page: page,
                pageSize: args.Take,
                sortOrder: sortOrder,
                filter: filter);

            return new FetchRowsResult(rows: issues, hasMoreRows: issues.Length == args.Take);
            #endregion
        }

        #region !
        [Command]
        public void GetUniqueValues(GetUniqueValuesAsyncArgs args) {
            if(args.PropertyName == "Priority") {
                var values = Enum.GetValues(typeof(Priority)).Cast<object>().ToArray();
                args.Result = System.Threading.Tasks.Task.Factory.StartNew(() => values);
            } else {
                throw new InvalidOperationException();
            }
        }
        #endregion
    }
}
