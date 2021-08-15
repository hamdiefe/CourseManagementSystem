using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Cities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Towns
{
    [Table("Towns")]
    [Audited]
    public class Town : FullAuditedEntity
    {
        [Required]
        [StringLength(TownConsts.MaxNameLength, MinimumLength = TownConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual int? CityId { get; set; }

        [ForeignKey("CityId")]
        public City CityFk { get; set; }     
    }
}
