using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Students;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Schools
{
    [Table("Schools")]
    [Audited]
    public class School : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(SchoolConsts.MaxNameLength, MinimumLength = SchoolConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual SchoolType Type { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public School()
        {
            Students = new HashSet<Student>();
        }
    }
}
