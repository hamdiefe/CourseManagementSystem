using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Addresses;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Jobs;
using CourseManagementSystem.Models.ParentTypes;
using CourseManagementSystem.Models.Phones;
using CourseManagementSystem.Models.StudentParents;
using CourseManagementSystem.Models.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Parents
{
    [Table("Parents")]
    [Audited]
    public class Parent : FullAuditedEntity, IMustHaveTenant
    {     
        public int TenantId { get; set; }

        [Range(ParentConsts.MinIdentityNumberValue, ParentConsts.MaxIdentityNumberValue)]
        public virtual long? IdentityNumber { get; set; }

        [Required]
        [StringLength(ParentConsts.MaxNameLength, MinimumLength = ParentConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(ParentConsts.MaxSurnameLength, MinimumLength = ParentConsts.MinSurnameLength)]
        public virtual string Surname { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual DateTime? BirthDate { get; set; }

        [StringLength(ParentConsts.MaxBirthPlaceLength, MinimumLength = ParentConsts.MinBirthPlaceLength)]
        public virtual string BirthPlace { get; set; }

        [StringLength(ParentConsts.MaxEmailLength, MinimumLength = ParentConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        public virtual EducationalStatus EducationalStatus { get; set; }

        public virtual int? JobId { get; set; }

        [ForeignKey("JobId")]
        public Job JobFk { get; set; }

        public virtual int? ParentTypeId { get; set; }

        [ForeignKey("ParentTypeId")]
        public ParentType ParentTypeFk { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<StudentParent> StudentParents { get; set; }

        public Parent()
        {
            Phones = new HashSet<Phone>();
            Addresses = new HashSet<Address>();
            StudentParents = new HashSet<StudentParent>();
        }
    }
}
