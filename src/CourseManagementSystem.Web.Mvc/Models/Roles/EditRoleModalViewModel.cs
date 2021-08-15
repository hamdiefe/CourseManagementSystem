using Abp.AutoMapper;
using CourseManagementSystem.Roles.Dto;
using CourseManagementSystem.Web.Models.Common;

namespace CourseManagementSystem.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
