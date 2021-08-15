using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Models.Grades;
using CourseManagementSystem.Services.Grades.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Grades
{
    [AbpAuthorize(PermissionNames.Pages_Grades)]
    public class GradesAppService : CourseManagementSystemAppServiceBase, IGradesAppService
    {
        private readonly IRepository<Grade> _gradeRepository;

        public GradesAppService(IRepository<Grade> gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<PagedResultDto<GetGradeForViewDto>> GetAll(GetAllGradesInput input)
        {
            var filteredGrades = _gradeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter));

            var pagedAndFilteredGrades = filteredGrades
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var grades = from o in pagedAndFilteredGrades
                         select new GetGradeForViewDto()
                         {
                             Grade = new GradeDto
                             {
                                 Id = o.Id,
                                 Name = o.Name
                             }
                         };

            var totalCount = await filteredGrades.CountAsync();

            return new PagedResultDto<GetGradeForViewDto>(
                totalCount,
                await grades.ToListAsync()
            );
        }

        public async Task<GetGradeForViewDto> GetGradeForView(int id)
        {
            var grade = await _gradeRepository.GetAsync(id);

            var output = new GetGradeForViewDto { Grade = ObjectMapper.Map<GradeDto>(grade) };

            return output;
        }

        public async Task<GetGradeForEditOutput> GetGradeForEdit(EntityDto input)
        {
            var grade = await _gradeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetGradeForEditOutput { Grade = ObjectMapper.Map<CreateOrEditGradeDto>(grade) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditGradeDto input)
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

        protected virtual async Task Create(CreateOrEditGradeDto input)
        {
            var grade = ObjectMapper.Map<Grade>(input);


            if (AbpSession.TenantId != null)
            {
                grade.TenantId = (int)AbpSession.TenantId;
            }


            await _gradeRepository.InsertAsync(grade);
        }

        protected virtual async Task Update(CreateOrEditGradeDto input)
        {
            var grade = await _gradeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, grade);
        }

        public async Task Delete(EntityDto input)
        {
            await _gradeRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteAll(List<int> input)
        {
            foreach (var item in input)
            {
                await _gradeRepository.DeleteAsync(item);
            }
        }
    }
}
