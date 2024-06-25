using System;

namespace GridDemo {
    public class IssueData {
        public IssueData(int id, string subject, int userId, DateTime created, int votes, Priority priority) {
            Id = id;
            Subject = subject;
            UserId = userId;
            Created = created;
            Votes = votes;
            Priority = priority;
        }
        public readonly int Id;
        public string Subject { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public int Votes { get; set; }
        public Priority Priority { get; set; }

        public IssueData Clone() {
            return new IssueData(Id, Subject, UserId, Created, Votes, Priority);
        }
    }
}
