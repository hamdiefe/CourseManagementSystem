using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Schools;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Schools.Dtos
{
    public class CreateOrEditSchoolDto : EntityDto<int?>
	{

		[Required]
		[StringLength(SchoolConsts.MaxNameLength, MinimumLength = SchoolConsts.MinNameLength)]
		public string Name { get; set; }

		public SchoolType Type { get; set; }
	}
}
