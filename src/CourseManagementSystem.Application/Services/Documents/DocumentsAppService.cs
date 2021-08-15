using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Models.Documents;
using CourseManagementSystem.Models.Students;
using CourseManagementSystem.Services.Documents.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Documents
{
    [AbpAuthorize(PermissionNames.Pages_Documents)]
    public class DocumentsAppService : CourseManagementSystemAppServiceBase, IDocumentsAppService
    {
        private readonly IRepository<Document> _documentRepository;
        private readonly IRepository<Student> _studentRepository;

        public DocumentsAppService(IRepository<Document> documentRepository,
                                   IRepository<Student> studentRepository)
        {
            _documentRepository = documentRepository;
            _studentRepository = studentRepository;
        }

        public async Task<PagedResultDto<GetDocumentForViewDto>> GetAll(GetAllDocumentsInput input)
        {
            var filteredDocuments = _documentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Description.Contains(input.Filter));

            var pagedAndFilteredDocuments = filteredDocuments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var documents = from o in pagedAndFilteredDocuments
                            join o1 in _studentRepository.GetAll() on o.StudentId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()
                            select new GetDocumentForViewDto()
                            {
                                Document = new DocumentDto
                                {
                                    Id = o.Id,
                                    Date = o.Date,
                                    Type = o.Type,
                                    Serie = o.Serie,
                                    No = o.No,
                                    Description = o.Description,
                                    Debt = o.Debt,
                                    Credit = o.Credit,
                                    Remaining = o.Remaining,
                                    Currency = o.Currency,
                                    StudentId = o.StudentId
                                },
                                StudentName = s1 == null || s1.Name == null ? "" : s1.Name + " " + s1.Surname
                            };

            var totalCount = await filteredDocuments.CountAsync();

            return new PagedResultDto<GetDocumentForViewDto>(
                totalCount,
                await documents.ToListAsync()
            );
        }

        public async Task<GetDocumentForViewDto> GetDocumentForView(int id)
        {
            var document = await _documentRepository.GetAsync(id);

            var output = new GetDocumentForViewDto { Document = ObjectMapper.Map<DocumentDto>(document) };

            if (output.Document.StudentId != null)
            {
                var student = await _studentRepository.FirstOrDefaultAsync((int)output.Document.StudentId);
                output.StudentName = student?.Name?.ToString() + " " + student?.Name?.ToString();
            }

            return output;
        }

        public async Task<GetDocumentForEditOutput> GetDocumentForEdit(EntityDto input)
        {
            var document = await _documentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDocumentForEditOutput { Document = ObjectMapper.Map<CreateOrEditDocumentDto>(document) };

            if (output.Document.StudentId != null)
            {
                var student = await _studentRepository.FirstOrDefaultAsync((int)output.Document.StudentId);
                output.StudentName = student?.Name?.ToString() + " " + student?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDocumentDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        protected virtual async Task Create(CreateOrEditDocumentDto input)
        {
            var document = ObjectMapper.Map<Document>(input);


            if (AbpSession.TenantId != null)
            {
                document.TenantId = (int)AbpSession.TenantId;
            }


            await _documentRepository.InsertAsync(document);
        }

        protected virtual async Task Update(CreateOrEditDocumentDto input)
        {
            var document = await _documentRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, document);
        }

        public async Task Delete(EntityDto input)
        {
            await _documentRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteAll(List<int> input)
        {
            foreach (var item in input)
            {
                await _documentRepository.DeleteAsync(item);
            }
        }

        public async Task<List<DocumentStudentLookupTableDto>> GetStudentsForTableDropdown()
        {
            return await _studentRepository.GetAll().Where(x => x.IsActive == true)
               .Select(o => new DocumentStudentLookupTableDto
               {
                   Id = o.Id,
                   DisplayName = o == null || o.Name == null ? "" : o.Name.ToString()
               }).ToListAsync();
        }
    }
}
