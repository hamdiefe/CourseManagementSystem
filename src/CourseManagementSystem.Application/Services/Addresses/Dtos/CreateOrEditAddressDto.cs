using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Addresses;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Addresses.Dtos
{
    public class CreateOrEditAddressDto : EntityDto<int?>
    {
        public long? CreatorUserId { get; set; }

        public int? TenantId { get; set; }

        [Required]
        [StringLength(AddressConsts.MaxDetailLength, MinimumLength = AddressConsts.MinDetailLength)]
        public string Detail { get; set; }

        [StringLength(AddressConsts.MaxDistrictLength, MinimumLength = AddressConsts.MinDistrictLength)]
        public string District { get; set; }

        public int? TownId { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public AddressType Type { get; set; }

        public int? StudentId { get; set; }

        public int? ParentId { get; set; }

        public int? TeacherId { get; set; }
    }
}
