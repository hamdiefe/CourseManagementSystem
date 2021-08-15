using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Models.Schools;
using CourseManagementSystem.Services.Schools.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.Schools
{
    [AbpAuthorize(PermissionNames.Pages_Schools)]
    public class SchoolsAppService : CourseManagementSystemAppServiceBase, ISchoolsAppService
    {
        private readonly IRepository<School> _schoolRepository;

        public SchoolsAppService(IRepository<School> schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public async Task<PagedResultDto<GetSchoolForViewDto>> GetAll(GetAllSchoolsInput input)
        {

            var typeFilter = input.TypeFilter.HasValue
                       ? (SchoolType)input.TypeFilter
                       : default;

            var filteredSchools = _schoolRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter))
                        .WhereIf(input.TypeFilter.HasValue && input.TypeFilter > -1, e => e.Type == typeFilter);

            var pagedAndFilteredSchools = filteredSchools
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var schools = from o in pagedAndFilteredSchools
                         select new GetSchoolForViewDto()
                         {
                             School = new SchoolDto
                             {
                                 Id = o.Id,
                                 Name = o.Name,
                                 Type = o.Type
                             }
                         };

            var totalCount = await filteredSchools.CountAsync();

            return new PagedResultDto<GetSchoolForViewDto>(
                totalCount,
                await schools.ToListAsync()
            );
        }

        public async Task<GetSchoolForViewDto> GetSchoolForView(int id)
        {
            var school = await _schoolRepository.GetAsync(id);

            var output = new GetSchoolForViewDto { School = ObjectMapper.Map<SchoolDto>(school) };

            return output;
        }

        public async Task<GetSchoolForEditOutput> GetSchoolForEdit(EntityDto input)
        {
            var school = await _schoolRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetSchoolForEditOutput { School = ObjectMapper.Map<CreateOrEditSchoolDto>(school) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditSchoolDto input)
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

        protected virtual async Task Create(CreateOrEditSchoolDto input)
        {
            var school = ObjectMapper.Map<School>(input);


            if (AbpSession.TenantId != null)
            {
                school.TenantId = (int)AbpSession.TenantId;
            }


            await _schoolRepository.InsertAsync(school);
        }

        protected virtual async Task Update(CreateOrEditSchoolDto input)
        {
            var school = await _schoolRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, school);
        }

        public async Task Delete(EntityDto input)
        {
            await _schoolRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteAll(List<int> input)
        {
            foreach (var item in input)
            {
                await _schoolRepository.DeleteAsync(item);
            }
        }
    }
}
