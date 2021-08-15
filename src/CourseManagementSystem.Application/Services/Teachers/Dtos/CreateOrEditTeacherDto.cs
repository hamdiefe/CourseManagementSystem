using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Teachers;
using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using CourseManagementSystem.Services.TeacherSpecializedFields.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Teachers.Dtos
{
    public class CreateOrEditTeacherDto : EntityDto<int?>
    {

        [Range(TeacherConsts.MinIdentityNumberValue, TeacherConsts.MaxIdentityNumberValue)]
        public long? IdentityNumber { get; set; }

        [Required]
        [StringLength(TeacherConsts.MaxNameLength, MinimumLength = TeacherConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(TeacherConsts.MaxSurnameLength, MinimumLength = TeacherConsts.MinSurnameLength)]
        public string Surname { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(TeacherConsts.MaxBirthPlaceLength, MinimumLength = TeacherConsts.MinBirthPlaceLength)]
        public string BirthPlace { get; set; }

        [StringLength(TeacherConsts.MaxEmailLength, MinimumLength = TeacherConsts.MinEmailLength)]
        public string Email { get; set; }

        public int? GraduationSchoolId { get; set; }

        public bool IsActive { get; set; }

        public EducationalStatus EducationalStatus { get; set; }

        public ICollection<CreateOrEditPhoneDto> Phones { get; set; }

        public ICollection<CreateOrEditAddressDto> Addresses { get; set; }

        public ICollection<CreateOrEditTeacherSpecializedFieldDto> TeacherSpecializedFields { get; set; }

        public CreateOrEditTeacherDto()
        {
            Phones = new HashSet<CreateOrEditPhoneDto>();
            Addresses = new HashSet<CreateOrEditAddressDto>();
            TeacherSpecializedFields = new HashSet<CreateOrEditTeacherSpecializedFieldDto>();
        }
    }
}
