using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Parents;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Jobs
{
    [Table("Jobs")]
    [Audited]
    public class Job : FullAuditedEntity, IMustHaveTenant
    {     
        public int TenantId { get; set; }

        [Required]
        [StringLength(JobConsts.MaxNameLength, MinimumLength = JobConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [StringLength(JobConsts.MaxDescriptionLength, MinimumLength = JobConsts.MinDescriptionLength)]
        public virtual string Description { get; set; }

        public virtual ICollection<Parent> Parent { get; set; }

        public Job()
        {
            Parent = new HashSet<Parent>();
        }
    }
}
