using System.Collections.Generic;
using CourseManagementSystem.Roles.Dto;

namespace CourseManagementSystem.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}