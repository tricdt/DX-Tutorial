using System;
using System.Collections.Generic;
using DevExpress.XtraRichEdit.Services;

namespace RichEditDemo {
    public class UserService : IUserListService {
        readonly List<string> users = new List<string>();

        public List<string> Users { get { return users; } }

        IList<string> IUserListService.GetUsers() {
            return Users;
        }
        public void Update(List<String> userList) {
            this.users.Clear();
            this.users.AddRange(userList);
        }
    }
}
