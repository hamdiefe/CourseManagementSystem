using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Jobs;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Jobs.Dtos
{
    public class CreateOrEditJobDto : EntityDto<int?>
	{

		[Required]
		[StringLength(JobConsts.MaxNameLength, MinimumLength = JobConsts.MinNameLength)]
		public string Name { get; set; }

		[StringLength(JobConsts.MaxDescriptionLength, MinimumLength = JobConsts.MinDescriptionLength)]
		public string Description { get; set; }
	}
}
