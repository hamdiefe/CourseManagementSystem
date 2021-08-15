using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Common;
using System;

namespace CourseManagementSystem.Services.Students.Dtos
{
    public class StudentDto : EntityDto
    {
        public long? IdentityNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Email { get; set; }

        public string BirthPlace { get; set; }

        public int? StudentNumber { get; set; }

        public int? GradeId { get; set; }

        public int? SchoolId { get; set; }

        public int HourlyPaymentPeriod { get; set; }

        public decimal HourlyPaymentAmount { get; set; }

        public string HourlyPaymentAmountString { get; set; }

        public Currency Currency { get; set; }

        public bool IsActive { get; set; }
    }
}
