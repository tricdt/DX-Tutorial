using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GridDemo {
    public static class IssuesService {
        #region helpers
        static IssuesService() {
            AllIssues = new Lazy<IssueData[]>(() => {
                var date = DateTime.Today;
                var rnd = new Random(0);
                return Enumerable.Range(0, 100000)
                    .Select(i => {
                        date = date.AddSeconds(-rnd.Next(20 * 60));
                        return new IssueData(
                            i,
                            subject: OutlookDataGenerator.GetSubject(),
                            userId: OutlookDataGenerator.GetFromId(),
                            created: date,
                            votes: rnd.Next(100),
                            priority: OutlookDataGenerator.GetPriority());
                    }).ToArray();
            });
        }
        static readonly Lazy<IssueData[]> AllIssues;
        class DefaultSortComparer : IComparer<IssueData> {
            int IComparer<IssueData>.Compare(IssueData x, IssueData y) {
                if(x.Created.Date != y.Created.Date)
                    return Comparer<DateTime>.Default.Compare(x.Created.Date, y.Created.Date);
                return Comparer<int>.Default.Compare(x.Votes, y.Votes);
            }
        }
        #endregion

        public async static Task<IssueData[]> GetIssuesAsync(int skip, int take, IssueSortOrder sortOrder, IssueFilter filter) {
            await System.Threading.Tasks.Task.Delay(300).ConfigureAwait(false);
            var issues = SortIssues(sortOrder, AllIssues.Value);
            if(filter != null)
                issues = FilterIssues(filter, issues);
            return issues.Skip(skip).Take(take).Select(x => x.Clone()).ToArray();
        }

        public static Task<IssueData[]> GetIssuesPageAsync(int page, int pageSize, IssueSortOrder sortOrder, IssueFilter filter) {
            return GetIssuesAsync(page * pageSize, pageSize, sortOrder, filter);
        }

        public async static Task<IssuesSummaries> GetSummariesAsync(IssueFilter filter) {
            await System.Threading.Tasks.Task.Delay(300).ConfigureAwait(false);
            var issues = (IEnumerable<IssueData>)AllIssues.Value;
            if(filter != null)
                issues = FilterIssues(filter, issues);
            DateTime? lastCreated = issues.Any() ? issues.Max(x => x.Created) : (DateTime?)null;
            return new IssuesSummaries(count: issues.Count(), lastCreated: lastCreated);
        }

        public async static System.Threading.Tasks.Task UpdateRowAsync(IssueData row) {
            if(row == null)
                return;
            IssueData data = AllIssues.Value.FirstOrDefault(x => x.Id == row.Id);
            if(data == null)
                return;
            data.Priority = row.Priority;
            data.Subject = row.Subject;
            data.UserId = row.UserId;
            data.Votes = row.Votes;
            data.Created = row.Created;
            await System.Threading.Tasks.Task.Delay(500).ConfigureAwait(false);
        }

        #region filter
        static IEnumerable<IssueData> FilterIssues(IssueFilter filter, IEnumerable<IssueData> issues) {
            if(filter.CreatedFrom.HasValue || filter.CreatedTo.HasValue) {
                if(!filter.CreatedFrom.HasValue || !filter.CreatedTo.HasValue) {
                    throw new InvalidOperationException();
                }
                issues = issues.Where(x => x.Created >= filter.CreatedFrom.Value && x.Created < filter.CreatedTo.Value);
            }
            if(filter.MinVotes.HasValue) {
                issues = issues.Where(x => x.Votes >= filter.MinVotes.Value);
            }
            if(filter.Priority.HasValue) {
                issues = issues.Where(x => x.Priority == filter.Priority.Value);
            }
            return issues;
        }
        #endregion

        #region sort
        static IEnumerable<IssueData> SortIssues(IssueSortOrder sortOrder, IEnumerable<IssueData> issues) {
            switch(sortOrder) {
                case IssueSortOrder.Default:
                    return issues.OrderByDescending(x => x, new DefaultSortComparer()).ThenByDescending(x => x.Created);
                case IssueSortOrder.CreatedDescending:
                    return issues.OrderByDescending(x => x.Created);
                case IssueSortOrder.VotesAscending:
                    return issues.OrderBy(x => x.Votes).ThenByDescending(x => x.Created);
                case IssueSortOrder.VotesDescending:
                    return issues.OrderByDescending(x => x.Votes).ThenByDescending(x => x.Created);
                default:
                    throw new InvalidOperationException();
            }
        }
        #endregion
    }
}
