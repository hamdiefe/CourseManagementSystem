using Abp.Application.Services.Dto;
using CourseManagementSystem.Models.Common;
using CourseManagementSystem.Models.Documents;
using System;

namespace CourseManagementSystem.Services.Documents.Dtos
{
    public class DocumentDto : EntityDto
    {
        public DateTime Date { get; set; }

        public DocumentType Type { get; set; }

        public DocumentSerie Serie { get; set; }

        public int No { get; set; }

        public string Description { get; set; }

        public decimal? Debt { get; set; }

        public decimal? Credit { get; set; }

        public decimal? Remaining { get; set; }

        public Currency Currency { get; set; }

        public int? StudentId { get; set; }
    }
}
