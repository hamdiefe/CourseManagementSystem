using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Students;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Grades
{
    [Table("Grades")]
    [Audited]
    public class Grade : FullAuditedEntity, IMustHaveTenant
    {     
        public int TenantId { get; set; }

        [Required]
        [StringLength(GradeConsts.MaxNameLength, MinimumLength = GradeConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Grade()
        {
            Students = new HashSet<Student>();
        }
    }
}
