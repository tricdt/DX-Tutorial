using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GridDemo.VirtualSources {
    public class InfiniteScrolling5ViewModel : ViewModelBase {
        #region fetch rows
        [Command]
        public void FetchIssues(FetchRowsAsyncArgs args) {
            args.Result = FetchRowsAsync(args);
        }
        static async Task<FetchRowsResult> FetchRowsAsync(FetchRowsAsyncArgs args) {
            #region sorting
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

            #region filtering
            IssueFilter filter = (IssueFilter)args.Filter;
            #endregion

            #region service request
            int take = args.Take ?? 50;
            var issues = await IssuesService.GetIssuesAsync(
                skip: args.Skip,
                take: take,
                sortOrder: sortOrder,
                filter: filter);

            return new FetchRowsResult(rows: issues, hasMoreRows: issues.Length == take);
            #endregion
        }
        #endregion

        #region unique filter values
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

        #region total summary
        [Command]
        public void GetTotalSummaries(GetSummariesAsyncArgs args) {
            args.Result = GetTotalSummariesAsync(args);
        }
        static async Task<object[]> GetTotalSummariesAsync(GetSummariesAsyncArgs args) {
            IssueFilter filter = (IssueFilter)args.Filter;
            var summaryValues = await IssuesService.GetSummariesAsync(filter);
            return args.Summaries.Select(x => {
                if(x.SummaryType == SummaryType.Count)
                    return (object)summaryValues.Count;
                if(x.SummaryType == SummaryType.Max && x.PropertyName == "Created")
                    return summaryValues.LastCreated;
                throw new InvalidOperationException();
            }).ToArray();
        }
        #endregion

        #region !
        [Command]
        public void UpdateIssue(RowValidationArgs args) {
            args.ResultAsync = UpdateIssueAsync(args.Item as IssueData);
        }
        static async Task<ValidationErrorInfo> UpdateIssueAsync(IssueData issue) {
            await IssuesService.UpdateRowAsync(issue);
            return null;
        }
        #endregion
    }
}
