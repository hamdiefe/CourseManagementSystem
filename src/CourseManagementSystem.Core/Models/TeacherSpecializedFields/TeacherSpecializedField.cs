using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Teachers;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.TeacherSpecializedFields
{
    [Table("TeacherSpecializedFields")]
    [Audited]
    public class TeacherSpecializedField : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual int? TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher TeacherFk { get; set; }

        public virtual Lesson SpecializedField { get; set; }
    }
}
