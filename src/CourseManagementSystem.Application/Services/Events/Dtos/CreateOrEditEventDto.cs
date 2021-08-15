using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Events;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Events.Dtos
{
    public class CreateOrEditEventDto : EntityDto<int?>
    {

        [Required]
        [StringLength(EventConsts.MaxTitleLength, MinimumLength = EventConsts.MinTitleLength)]
        public string Title { get; set; }

        [StringLength(EventConsts.MaxDescriptionLength, MinimumLength = EventConsts.MinDescriptionLength)]
        public string Description { get; set; }

        public EventType Type { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int TotalHours { get; set; }

        public int? StudentId { get; set; }

        public int? TeacherId { get; set; }

        public int WeekRepeat { get; set; }

        public EventStatus Status { get; set; }
    }
}
