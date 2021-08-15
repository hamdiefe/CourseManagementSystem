using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Models.ParentTypes;
using CourseManagementSystem.Services.ParentTypes.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseManagementSystem.Services.ParentTypes
{
    [AbpAuthorize(PermissionNames.Pages_ParentTypes)]
    public class ParentTypesAppService : CourseManagementSystemAppServiceBase, IParentTypesAppService
    {
        private readonly IRepository<ParentType> _parentTypeRepository;

        public ParentTypesAppService(IRepository<ParentType> parentTypeRepository)
        {
            _parentTypeRepository = parentTypeRepository;
        }

        public async Task<PagedResultDto<GetParentTypeForViewDto>> GetAll(GetAllParentTypesInput input)
        {
            var filteredParentTypes = _parentTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter));

            var pagedAndFilteredParentTypes = filteredParentTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var parentTypes = from o in pagedAndFilteredParentTypes
                         select new GetParentTypeForViewDto()
                         {
                             ParentType = new ParentTypeDto
                             {
                                 Id = o.Id,
                                 Name = o.Name,
                                 Degree = o.Degree
                             }
                         };

            var totalCount = await filteredParentTypes.CountAsync();

            return new PagedResultDto<GetParentTypeForViewDto>(
                totalCount,
                await parentTypes.ToListAsync()
            );
        }

        public async Task<GetParentTypeForViewDto> GetParentTypeForView(int id)
        {
            var parentType = await _parentTypeRepository.GetAsync(id);

            var output = new GetParentTypeForViewDto { ParentType = ObjectMapper.Map<ParentTypeDto>(parentType) };

            return output;
        }

        public async Task<GetParentTypeForEditOutput> GetParentTypeForEdit(EntityDto input)
        {
            var parentType = await _parentTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetParentTypeForEditOutput { ParentType = ObjectMapper.Map<CreateOrEditParentTypeDto>(parentType) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditParentTypeDto input)
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

        protected virtual async Task Create(CreateOrEditParentTypeDto input)
        {
            var parentType = ObjectMapper.Map<ParentType>(input);


            if (AbpSession.TenantId != null)
            {
                parentType.TenantId = (int)AbpSession.TenantId;
            }


            await _parentTypeRepository.InsertAsync(parentType);
        }

        protected virtual async Task Update(CreateOrEditParentTypeDto input)
        {
            var parentType = await _parentTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, parentType);
        }

        public async Task Delete(EntityDto input)
        {
            await _parentTypeRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteAll(List<int> input)
        {
            foreach (var item in input)
            {
                await _parentTypeRepository.DeleteAsync(item);
            }
        }
    }
}
