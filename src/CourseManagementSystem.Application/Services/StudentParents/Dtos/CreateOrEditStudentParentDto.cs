using Abp.Application.Services.Dto;
using CourseManagementSystem.Services.Parents.Dtos;

namespace CourseManagementSystem.Services.StudentParents.Dtos
{
    public class CreateOrEditStudentParentDto : EntityDto<int?>
    {
        public long? CreatorUserId { get; set; }

        public int? TenantId { get; set; }

        public int? StudentId { get; set; }

        public int? ParentId { get; set; }

        public CreateOrEditParentDto Parent { get; set; }
        public object ParentTypeName { get; set; }
    }
}
