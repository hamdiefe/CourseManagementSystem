using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Parents;
using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Parents.Dtos
{
    public class CreateOrEditParentDto : EntityDto<int?>
    {
        [Range(ParentConsts.MinIdentityNumberValue, ParentConsts.MaxIdentityNumberValue),]
        public long? IdentityNumber { get; set; }

        [Required]
        [StringLength(ParentConsts.MaxNameLength, MinimumLength = ParentConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ParentConsts.MaxSurnameLength, MinimumLength = ParentConsts.MinSurnameLength)]
        public string Surname { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(ParentConsts.MaxBirthPlaceLength, MinimumLength = ParentConsts.MinBirthPlaceLength)]
        public string BirthPlace { get; set; }

        [StringLength(ParentConsts.MaxEmailLength, MinimumLength = ParentConsts.MinEmailLength)]
        public string Email { get; set; }

        public EducationalStatus EducationalStatus { get; set; }

        public int? JobId { get; set; }

        public int? ParentTypeId { get; set; }

        public ICollection<CreateOrEditPhoneDto> Phones { get; set; }

        public ICollection<CreateOrEditAddressDto> Addresses { get; set; }

        public CreateOrEditParentDto()
        {
            Phones = new HashSet<CreateOrEditPhoneDto>();
            Addresses = new HashSet<CreateOrEditAddressDto>();
        }
    }
}
