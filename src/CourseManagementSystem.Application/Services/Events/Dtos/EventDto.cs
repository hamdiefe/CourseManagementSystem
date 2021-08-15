using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Events;
using System;

namespace CourseManagementSystem.Services.Events.Dtos
{
    public class EventDto : EntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalHours { get; set; }
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
        public EventStatus Status { get; internal set; }
    }
}
