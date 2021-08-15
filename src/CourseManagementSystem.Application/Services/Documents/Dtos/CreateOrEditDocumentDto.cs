using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Documents;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementSystem.Services.Documents.Dtos
{
    public class CreateOrEditDocumentDto : EntityDto<int?>
    {
        public DateTime Date { get; set; }

        public DocumentType Type { get; set; }

        public DocumentSerie Serie { get; set; }

        public int No { get; set; }

        [StringLength(DocumentConsts.MaxDescrptionLength, MinimumLength = DocumentConsts.MinDescrptionLength)]
        public string Description { get; set; }

        public string Debt { get; set; }

        public string Credit { get; set; }

        public string Remaining { get; set; }

        public Currency Currency { get; set; }

        public int? StudentId { get; set; }
    }
}
