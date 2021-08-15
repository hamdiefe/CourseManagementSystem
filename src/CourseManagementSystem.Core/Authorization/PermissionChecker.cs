using Abp.Authorization;
using CourseManagementSystem.Authorization.Roles;
using CourseManagementSystem.Authorization.Users;

namespace CourseManagementSystem.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
