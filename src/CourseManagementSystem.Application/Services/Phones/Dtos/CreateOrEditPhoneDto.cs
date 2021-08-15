using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Phones;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Phones.Dtos
{
    public class CreateOrEditPhoneDto : EntityDto<int?>
    {
        public long? CreatorUserId { get; set; }

        public int? TenantId { get; set; }

        [Required]
        [StringLength(PhoneConsts.MaxCodeLength, MinimumLength = PhoneConsts.MinCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(PhoneConsts.MaxNumberLength, MinimumLength = PhoneConsts.MinNumberLength)]
        public string Number { get; set; }

        public PhoneType Type { get; set; }

        public int? StudentId { get; set; }

        public int? ParentId { get; set; }

        public int? TeacherId { get; set; }
    }
}
