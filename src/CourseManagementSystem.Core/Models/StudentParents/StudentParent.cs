using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Parents;
using CourseManagementSystem.Models.Students;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.StudentParents
{
    [Table("StudentParents")]
    [Audited]
    public class StudentParent : FullAuditedEntity, IMustHaveTenant
    {     
        public int TenantId { get; set; }        

        public virtual int? StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student StudentFk { get; set; }

        public virtual int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Parent ParentFk { get; set; }       
    }
}
