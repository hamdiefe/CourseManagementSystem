using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Countries;
using CourseManagementSystem.Models.Towns;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Cities
{
    [Table("Cities")]
    [Audited]
    public class City : FullAuditedEntity
    {
        [Required]
        [StringLength(CityConsts.MaxNameLength, MinimumLength = CityConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(CityConsts.MaxLicensePlateLength, MinimumLength = CityConsts.MinLicensePlateLength)]
        public virtual string LicensePlate { get; set; }

        [Required]
        public virtual int PhoneCode { get; set; }

        public virtual int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; }

        public virtual ICollection<Town> Towns { get; set; }

        public City()
        {
            Towns = new HashSet<Town>();
        }
    }
}
