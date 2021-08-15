using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.StudentParents;
using CourseManagementSystem.Models.Students;
using CourseManagementSystem.Services.Addresses.Dtos;
using CourseManagementSystem.Services.Parents.Dtos;
using CourseManagementSystem.Services.Phones.Dtos;
using CourseManagementSystem.Services.StudentParents.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Students.Dtos
{
    public class CreateOrEditStudentDto : EntityDto<int?>
    {
        [Range(StudentConsts.MinIdentityNumberValue, StudentConsts.MaxIdentityNumberValue),]
        public long? IdentityNumber { get; set; }

        [Required]
        [StringLength(StudentConsts.MaxNameLength, MinimumLength = StudentConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(StudentConsts.MaxSurnameLength, MinimumLength = StudentConsts.MinSurnameLength)]
        public string Surname { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(StudentConsts.MaxEmailLength, MinimumLength = StudentConsts.MinEmailLength)]
        public string Email { get; set; }

        [StringLength(StudentConsts.MaxBirthPlaceLength, MinimumLength = StudentConsts.MinBirthPlaceLength)]
        public string BirthPlace { get; set; }

        public int? StudentNumber { get; set; }

        public int? GradeId { get; set; }

        public int? SchoolId { get; set; }

        public int HourlyPaymentPeriod { get; set; }

        public string HourlyPaymentAmount { get; set; }

        public Currency Currency { get; set; }

        public bool IsActive { get; set; }

        public ICollection<CreateOrEditPhoneDto> Phones { get; set; }

        public ICollection<CreateOrEditAddressDto> Addresses { get; set; }

        public ICollection<CreateOrEditStudentParentDto> StudentParents { get; set; }


        public CreateOrEditStudentDto()
        {
            Phones = new HashSet<CreateOrEditPhoneDto>();
            Addresses = new HashSet<CreateOrEditAddressDto>();
            StudentParents = new HashSet<CreateOrEditStudentParentDto>();
        }
    }
}
