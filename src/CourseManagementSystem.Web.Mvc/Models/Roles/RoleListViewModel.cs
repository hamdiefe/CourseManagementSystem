using System.Collections.Generic;
using CourseManagementSystem.Roles.Dto;

namespace CourseManagementSystem.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
