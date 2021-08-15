using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Addresses;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Documents;
using CourseManagementSystem.Models.Events;
using CourseManagementSystem.Models.Grades;
using CourseManagementSystem.Models.Phones;
using CourseManagementSystem.Models.Schools;
using CourseManagementSystem.Models.StudentParents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Students
{
    [Table("Students")]
    public class Student : FullAuditedEntity, IMustHaveTenant
    {   
        public int TenantId { get; set; }

        [Range(StudentConsts.MinIdentityNumberValue, StudentConsts.MaxIdentityNumberValue)]
        public virtual long? IdentityNumber { get; set; }

        [Required]
        [StringLength(StudentConsts.MaxNameLength, MinimumLength = StudentConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(StudentConsts.MaxSurnameLength, MinimumLength = StudentConsts.MinSurnameLength)]
        public virtual string Surname { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual DateTime? BirthDate { get; set; }

        [StringLength(StudentConsts.MaxBirthPlaceLength, MinimumLength = StudentConsts.MinBirthPlaceLength)]
        public virtual string BirthPlace { get; set; }

        [StringLength(StudentConsts.MaxEmailLength, MinimumLength = StudentConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        public virtual int? StudentNumber { get; set; }

        public virtual int? SchoolId { get; set; }

        [ForeignKey("SchoolId")]
        public School SchoolFk { get; set; }

        public virtual int? GradeId { get; set; }

        [ForeignKey("GradeId")]
        public Grade GradeFk { get; set; }       

        public virtual ICollection<Phone> Phones { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<StudentParent> StudentParents { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual int HourlyPaymentPeriod { get; set; }

        public virtual decimal HourlyPaymentAmount { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public Student()
        {
            Phones = new HashSet<Phone>();
            Addresses = new HashSet<Address>();
            StudentParents = new HashSet<StudentParent>();
            Events = new HashSet<Event>();
            Documents = new HashSet<Document>();
        }
    }
}
