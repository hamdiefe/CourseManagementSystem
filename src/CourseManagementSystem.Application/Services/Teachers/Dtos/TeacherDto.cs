using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Common;
using System;

namespace CourseManagementSystem.Services.Teachers.Dtos
{
    public class TeacherDto : EntityDto
    {
        public long? IdentityNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string Email { get; set; }

        public int? GraduationSchoolId { get; set; }

        public EducationalStatus EducationalStatus { get; set; }

        public bool IsActive { get; set; }
    }
}
