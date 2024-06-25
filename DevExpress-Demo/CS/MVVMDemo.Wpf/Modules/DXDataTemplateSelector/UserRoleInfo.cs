using System;
using System.Linq;

namespace MVVMDemo.DXDataTemplateSelector {
    public class UserRoleInfo {
        #region UserRoles
        public static UserRoleInfo[] UserRoles
        {
            get
            {
                UserRole[] roles = Enum.GetValues(typeof(UserRole))
                    .Cast<UserRole>()
                    .ToArray();
                return PersonInfo.Persons
                    .Select((person, i) => new UserRoleInfo() { FullName = person.FullName, UserRole = roles[i] })
                    .ToArray();
            }
        } 
        #endregion
        public string FullName { get; set; }
        public UserRole UserRole { get; set; }
        public bool IsAdmin() {
            return UserRole == UserRole.Admin;
        }
    }
    public enum UserRole {
        Admin,
        Moderator,
        User
    }
}
