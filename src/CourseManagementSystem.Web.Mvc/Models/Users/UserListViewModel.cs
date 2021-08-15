using System.Collections.Generic;
using CourseManagementSystem.Roles.Dto;

namespace CourseManagementSystem.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
