using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Parents;
using CourseManagementSystem.Models.Students;
using CourseManagementSystem.Models.Teachers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Phones
{
    [Table("Phones")]
    [Audited]
    public class Phone : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(PhoneConsts.MaxCodeLength, MinimumLength = PhoneConsts.MinCodeLength)]
        public virtual string Code { get; set; }

        [Required]
        [StringLength(PhoneConsts.MaxNumberLength, MinimumLength = PhoneConsts.MinNumberLength)]
        public virtual string Number { get; set; }

        public virtual PhoneType Type { get; set; }

        public virtual int? StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student StudentFk { get; set; }

        public virtual int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Parent ParentFk { get; set; }

        public virtual int? TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher TeacherFk { get; set; }
    }
}
