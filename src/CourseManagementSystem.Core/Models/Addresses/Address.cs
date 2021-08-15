using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Cities;
using CourseManagementSystem.Models.Countries;
using CourseManagementSystem.Models.Parents;
using CourseManagementSystem.Models.Students;
using CourseManagementSystem.Models.Teachers;
using CourseManagementSystem.Models.Towns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Addresses
{
    [Table("Addresses")]
    [Audited]
    public class Address : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(AddressConsts.MaxDetailLength, MinimumLength = AddressConsts.MinDetailLength)]
        public virtual string Detail { get; set; }

        [StringLength(AddressConsts.MaxDistrictLength, MinimumLength = AddressConsts.MinDistrictLength)]
        public virtual string District { get; set; }

        public virtual int? TownId { get; set; }

        [ForeignKey("CityId")]
        public Town TownFk { get; set; }

        public virtual int? CityId { get; set; }

        [ForeignKey("CityId")]
        public City CityFk { get; set; }

        public virtual int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }

        public virtual AddressType Type { get; set; }

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
