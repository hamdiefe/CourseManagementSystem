using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Cities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Countries
{
    [Table("Countries")]
    [Audited]
    public class Country : FullAuditedEntity
    {
        [Required]
        [StringLength(CountryConsts.MaxNameLength, MinimumLength = CountryConsts.MinNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxDualCodeLength, MinimumLength = CountryConsts.MinDualCodeLength)]
        public virtual string DualCode { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxTernaryCodeLength, MinimumLength = CountryConsts.MinTernaryCodeLength)]
        public virtual string TernaryCode { get; set; }

        [Required]
        [StringLength(CountryConsts.MaxPhoneCodeLength, MinimumLength = CountryConsts.MinPhoneCodeLength)]
        public virtual string PhoneCode { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public Country()
        {
            Cities = new HashSet<City>();
        }
    }
}
