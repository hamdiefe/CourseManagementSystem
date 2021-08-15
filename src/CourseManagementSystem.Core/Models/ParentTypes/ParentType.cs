using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Parents;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.ParentTypes
{
    [Table("ParentTypes")]
    [Audited]
    public class ParentType : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(ParentTypeConsts.MaxNameLength, MinimumLength = ParentTypeConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual ParentDegree Degree { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }

        public ParentType()
        {
            Parents = new HashSet<Parent>();
        }
    }
}
