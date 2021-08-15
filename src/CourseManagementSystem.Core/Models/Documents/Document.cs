using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Students;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementSystem.Models.Documents
{
    [Table("Documents")]
    [Audited]
    public class Document : FullAuditedEntity
    {
        public int TenantId { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual DocumentType Type { get; set; }

        public virtual DocumentSerie Serie { get; set; }

        public virtual int No { get; set; }

        [StringLength(DocumentConsts.MaxDescrptionLength, MinimumLength = DocumentConsts.MinDescrptionLength)]
        public virtual string Description { get; set; }

        public virtual decimal? Debt { get; set; }

        public virtual decimal? Credit { get; set; }

        public virtual decimal? Remaining { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual int? StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student StudentFk { get; set; }
    }
}
