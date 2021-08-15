using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Addresses;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Events;
using CourseManagementSystem.Models.Phones;
using CourseManagementSystem.Models.Schools;
using CourseManagementSystem.Models.TeacherSpecializedFields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Teachers
{
    [Table("Teachers")]
    public class Teacher : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Range(TeacherConsts.MinIdentityNumberValue, TeacherConsts.MaxIdentityNumberValue)]
        public virtual long? IdentityNumber { get; set; }

        [Required]
        [StringLength(TeacherConsts.MaxNameLength, MinimumLength = TeacherConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(TeacherConsts.MaxSurnameLength, MinimumLength = TeacherConsts.MinSurnameLength)]
        public virtual string Surname { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual DateTime? BirthDate { get; set; }

        [StringLength(TeacherConsts.MaxBirthPlaceLength, MinimumLength = TeacherConsts.MinBirthPlaceLength)]
        public virtual string BirthPlace { get; set; }

        [StringLength(TeacherConsts.MaxEmailLength, MinimumLength = TeacherConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        public virtual int? GraduationSchoolId { get; set; }

        [ForeignKey("GraduationSchoolId")]
        public School GraduationSchoolFk { get; set; }

        public virtual bool IsActive { get; set; }


        public virtual EducationalStatus EducationalStatus { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<TeacherSpecializedField> TeacherSpecializedFields { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public Teacher()
        {
            Phones = new HashSet<Phone>();
            Addresses = new HashSet<Address>();
            TeacherSpecializedFields = new HashSet<TeacherSpecializedField>();
            Events = new HashSet<Event>();
        }
    }
}
