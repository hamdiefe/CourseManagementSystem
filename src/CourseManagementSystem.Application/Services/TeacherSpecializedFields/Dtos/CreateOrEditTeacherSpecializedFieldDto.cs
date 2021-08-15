using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Common;

namespace CourseManagementSystem.Services.TeacherSpecializedFields.Dtos
{
    public class CreateOrEditTeacherSpecializedFieldDto : EntityDto<int?>
    {
        public long? CreatorUserId { get; set; }

        public int? TenantId { get; set; }

        public int? TeacherId { get; set; }

        public Lesson SpecializedField { get; set; }
    }
}
