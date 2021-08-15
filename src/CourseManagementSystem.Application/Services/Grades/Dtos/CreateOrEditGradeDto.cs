using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Grades;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Grades.Dtos
{
    public class CreateOrEditGradeDto : EntityDto<int?>
	{

		[Required]
		[StringLength(GradeConsts.MaxNameLength, MinimumLength = GradeConsts.MinNameLength)]
		public string Name { get; set; }
	}
}
