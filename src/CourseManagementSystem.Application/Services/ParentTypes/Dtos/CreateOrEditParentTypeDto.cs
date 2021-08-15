using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.ParentTypes;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.ParentTypes.Dtos
{
    public class CreateOrEditParentTypeDto : EntityDto<int?>
    {

        [Required]
        [StringLength(ParentTypeConsts.MaxNameLength, MinimumLength = ParentTypeConsts.MinNameLength)]
        public string Name { get; set; }

        public ParentDegree Degree { get; set; }
    }
}
