using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Students;
using CourseManagementSystem.Models.Teachers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Events
{
    [Table("Events")]
    [Audited]
    public class Event : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(EventConsts.MaxTitleLength, MinimumLength = EventConsts.MinTitleLength)]
        public virtual string Title { get; set; }

        [StringLength(EventConsts.MaxDescriptionLength, MinimumLength = EventConsts.MinDescriptionLength)]
        public virtual string Description { get; set; }

        public EventType Type { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual int TotalHours { get; set; }

        public virtual int? StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student StudentFk { get; set; }

        public virtual int? TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher TeacherFk { get; set; }

        public EventStatus Status { get; set; }
    }
}
