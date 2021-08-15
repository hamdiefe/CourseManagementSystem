using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Common;
using System;

namespace CourseManagementSystem.Services.Parents.Dtos
{
    public class ParentDto : EntityDto
    {
        public long? IdentityNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string Email { get; set; }

        public EducationalStatus EducationalStatus { get; set; }

        public int? JobId { get; set; }

        public int? ParentTypeId { get; set; }
    }
}
